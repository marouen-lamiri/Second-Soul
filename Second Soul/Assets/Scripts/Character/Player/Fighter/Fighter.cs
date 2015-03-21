using UnityEngine;
using System.Collections;

public class Fighter : Player{
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
		//important that this happens first, other initializations depend on it
		playerEnabled = true;
		if (Network.isClient) {
			playerEnabled = false;
		}	
		
		//inititalize skills before Player start
		activeSkill1 = null;
		activeSkill2 = null;
		activeSkill3 = null;
		activeSkill4 = null;
		activeSkill5 = null;
		activeSkill6 = null;
		
		initializePlayer();
		playerStart ();
		initializeLevel();
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		initializeMoney();
		
		health = maxHealth;
		energy = maxEnergy;
		
		target = null;
		startPosition = transform.position;

		//networking:
		fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
		//database.readPrimaryStats();
	}
	// Update is called once per frame
	void FixedUpdate () {
		fighterUpdate ();
	}

	protected void fighterUpdate(){
		playerUpdate ();
		if(Input.GetKeyDown ("e")){
			playerEnabled = !playerEnabled;
		}
		playerLogic();
	}

	protected void initializeMoney(){
		if(level <= 1){
			gold = 300; 
		}
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
		UnityNotificationBar.UNotify ("Level Up to level: " + level); //although this might appear false in Mono-Develop, it actually works as an external asset
		usableSkillPoints++;
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

	public int getLevelXP () {
		return totalXP;
	}

	public float getLevelBaseXP(){
		return baseFactorXP;
	}

	public int getLevel () {
		return level;
	}
	
	public int getNextLevel(){
		return nextLevelXP;
	}

	public void setEndurance (int iEndurance) {
		this.endurance = iEndurance;
	}

	public void setLevel (int iLevel) {
		this.level = iLevel;
	}
	
	public void setNextLevel(int iBaseFactor){
		this.nextLevelXP = iBaseFactor;
	}

	public void setLevelXP (int iLevel) {
		this.totalXP = iLevel;
	}

	public void setLevelBaseXP(float iBaseFactor){
		this.baseFactorXP = iBaseFactor;
	}
}	
