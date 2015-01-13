using UnityEngine;
using System.Collections;

public class Sorcerer : Player {

	//Variable declaration
	protected int intelligence; // spell power, spell crit damage
	protected int wisdom; // cast speed/cooldown, spell crit chance
	protected int spirit; // total energy/regen

	protected int intelligencePerLvl;
	protected int wisdomPerLvl;
	protected int spiritPerLvl;

	private Fighter fighter;

	public DatabaseSorcerer database;

	// Use this for initialization
	void Start () {
		sorcererStart (); //initialized in base classes now why still needed?
		database.readPrimaryStats();
	}
	// is this needed since called in sorcereStart??
	protected void initFighter(){
		//fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}

	protected void sorcererStart(){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		initializePlayer();
		initializeLevel();
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		playerEnabled=true;
		if (Network.isClient) {
			fighter.playerEnabled=false;
		}
		health = maxHealth;
		energy = maxEnergy;
		
		target = null;
		startPosition = transform.position;
		activeSkill1 = (BasicRanged)controller.GetComponent<BasicRanged>();
		activeSkill2 = (FireballSkill)controller.GetComponent<FireballSkill>();
		activeSkill2.setCaster (this);
		startPosition = transform.position;
		//networking:
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
	}
	// Update is called once per frame
	void Update () {
		sorcererUpdate ();
	}

	protected void sorcererUpdate(){
		playerUpdate ();
		playerEnabled = !fighter.playerEnabled;
		playerLogic();
	}

	protected virtual void initializePrimaryStats(){
		intelligencePerLvl = 1;
		wisdomPerLvl = 1;
		spiritPerLvl = 1;
		
		intelligence = 10;
		wisdom = 10;
		spirit = 10;
	}

	public override bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= spellCriticalChance * 100) {
			return true;
		}
		else {
			return false;
		}
	}
	public override double getDamage(){
		if(!hitCheck()){
			return 0;
		}
		if (criticalHitCheck ()) {
			return damage*attackPower*spellCriticalDamage;
		}
		return damage * spellPower;
	}
	public override void levelUp(){
		Debug.Log("leveled up");
		
		calculateNewPrimaryStats();
		
		initializeSecondaryStats(); // reset base so that new and old primary stat don't combine
		calculateSecondaryStats();
	}

	public void calculateNewPrimaryStats(){
		intelligence += intelligencePerLvl;
		wisdom += wisdomPerLvl;
		spirit += spiritPerLvl;
	}
	
	public void calculateSecondaryStats(){
		castSpeed += wisdom * castSpeedBase;
		cdr += wisdom * cdrBase;
		spellCriticalChance += wisdom * spCritChanBase;;
		spellCriticalDamage += intelligence * spCritDmgBase;
		
		spellPower += intelligence * spPowerBase;

		fighter.maxEnergy += spirit * enBase;//not sure of this. we may ned to adjust who holds energy because fighter holds it atm
		energyRegen += spirit * enRegBase;
		
		fighter.energy = maxEnergy;
	}

	public override bool isDead(){
		return fighter.isDead ();
	}

	public override bool loseEnergy(float energy){

		var newEnergyValue = fighter.loseEnergy (energy);

		// networking event listener:
		sorcererNetworkScript.onEnergyLost (newEnergyValue);// (this.energy);

		return newEnergyValue;
	}

	// Getters and Setters for Primary Stats
	public int getIntelligence () {
		return intelligence;
	}
	
	public void setIntelligence (int iIntelligence) {
		this.intelligence = iIntelligence;
	}
	
	public int getWisdom () {
		return wisdom;
	}
	
	public void setWisdom (int iWisdom) {
		this.wisdom = iWisdom;
	}
	
	public int getSpirit () {
		return spirit;
	}
	
	public void setSpirit (int iSpirit) {
		this.spirit = iSpirit;
	}
}
