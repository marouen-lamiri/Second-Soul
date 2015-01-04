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
	
	// Use this for initialization
	protected void Awake (){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	void Start () {
		sorcererStart ();
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

	public virtual bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= spellCriticalChance * 100) {
			return true;
		}
		else {
			return false;
		}
	}
	public virtual double getDamage(){
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
		return fighter.loseEnergy (energy);
	}
}
