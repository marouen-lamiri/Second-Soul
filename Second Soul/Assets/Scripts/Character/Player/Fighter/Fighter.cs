using UnityEngine;
using System.Collections;

public class Fighter : Player {
	//Variable declaration
	
	// primary stats... these compute secondary stats defined in Character
	protected int strength; // base damage, armor, critt damage
	protected int dexterity; // attack speed, crit chance, accuracy
	protected int endurance; // health, resistances, health regen
	
	protected int strengthPerLvl;
	protected int dexterityPerLvl;
	protected int endurancePerLvl;

	public DatabaseFighter database;
	
	//FighterNetworkScript fighterNetworkScript; // is "already serialized" in parent class Player.cs.

	// Use this for initialization
	void Start () {
		fighterStart ();
		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));

	}
	protected void fighterStart(){
		playerStart ();
		initializePlayer();
		initializeLevel();
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		playerEnabled=true;
		if (Network.isClient) {
			playerEnabled = false;
		}
		health = maxHealth;
		energy = maxEnergy;
		
		target = null;
		startPosition = transform.position;
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();
		activeSkill2 = (Charge)controller.GetComponent<Charge>();
		activeSkill3 = (KnightsHonour)controller.GetComponent<KnightsHonour>();
		activeSkill4 = (BerserkMode)controller.GetComponent<BerserkMode>();
		activeSkill5 = (Focus)controller.GetComponent<Focus>();
		activeSkill1.setCaster (this);
		activeSkill2.setCaster (this);
		activeSkill3.setCaster (this);
		activeSkill4.setCaster (this);
		activeSkill5.setCaster (this);
		//networking:
		fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
		database.readPrimaryStats();
	}
	// Update is called once per frame
	void Update () {
		fighterUpdate ();
	}

	protected void fighterUpdate(){
		playerUpdate ();
		if(Input.GetKeyDown ("e")){
			playerEnabled = !playerEnabled;
		}
		playerLogic();
	}
	
	protected virtual void initializePrimaryStats(){
		strengthPerLvl = 1;
		dexterityPerLvl = 1;
		endurancePerLvl = 1;
		
		strength = 10;
		dexterity = 10;
		endurance = 10;
	}
	
	public override void levelUp(){
		Debug.Log("leveled up");
		
		calculateNewPrimaryStats();
		
		initializeSecondaryStats(); // reset base so that new and old primary stat don't combine
		calculateSecondaryStats();
	}
	
	public void calculateNewPrimaryStats(){
		strength += strengthPerLvl;
		dexterity += dexterityPerLvl;
		endurance += endurancePerLvl;
	}
	
	public void calculateSecondaryStats(){
		armor += strength * armorBase;
		fireResistance += endurance * fireResBase;
		coldResistance += endurance * coldResBase;
		lightningtResistance += endurance * lightResBase;
		
		accuracy += dexterity * accurBase;
		attackSpeed += dexterity * attSpeedBase;
		
		criticalChance += dexterity * critChanBase;
		criticalDamage += strength * critDmgBase;
		
		attackPower += strength * attPowerBase;
		
		maxHealth += endurance * hpBase;
		healthRegen += endurance * hpRegBase;
		
		health = maxHealth;
	}

	public override bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= criticalChance * 100) {
			return true;
		}
		else {
			return false;
		}
	}

	public override double getDamage(){
		if (criticalHitCheck ()) {
			return attackPower*criticalDamage;
		}
		return attackPower;
	}

	public override double getDamageCanMiss(){
		if(!hitCheck()){
			return 0;
		}
		if (criticalHitCheck ()) {
			return attackPower*criticalDamage;
		}
		return attackPower;
	}

	public override bool loseEnergy(float energy){
		if (energy > this.energy) {
			return false;
		}
		this.energy -= energy;

		// networking event listener:
		fighterNetworkScript.onEnergyLost (this.energy);

		return true;
	}

	// Getters and Setters for Primary Stats
	public int getStrength () {
		return strength;
	}
	
	public void setStrength (int iStrength) {
		this.strength = iStrength;
	}

	public int getDexterity () {
		return dexterity;
	}
	
	public void setDexterity (int iDexterity) {
		this.dexterity = iDexterity;
	}
	
	public int getEndurance () {
		return endurance;
	}
	
	public void setEndurance (int iEndurance) {
		this.endurance = iEndurance;
	}

}
