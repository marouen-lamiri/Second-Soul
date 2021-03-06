﻿using UnityEngine;
using System.Collections;

public class Sorcerer : Player {

	//Variable declaration
	protected int intelligence; // spell power, spell crit damage
	protected int wisdom; // cast speed/cooldown, spell crit chance
	protected int spirit; // total energy/regen

	protected int intelligencePerLvl;
	protected int wisdomPerLvl;
	protected int spiritPerLvl;

	public static int soulShards; //Money for the sorcerer;

	protected Fighter fighter;

	public DatabaseSorcerer database;

	// Use this for initialization
	void Start () {
		sorcererStart (); //initialized in base classes now why still needed?
		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));
	}

	protected void sorcererStart(){
		//important that this happens first, other initializations depend on it
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		//logically the sorcerer shouldn't be an enabled player unless it's the client in a game
		playerEnabled = false;
		if (Network.isClient) {
			playerEnabled = true;
			fighter.playerEnabled = false;
		}
		
		//inititalize skills before Player start
		activeSkill1 = null;
		activeSkill2 = null;
		activeSkill3 = null;
		activeSkill4 = null;
		activeSkill5 = null;
		activeSkill6 = null;
		
		playerStart ();
		initializePlayer();
		initializeLevel();
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		health = maxHealth;
		fighter.energy = fighter.maxEnergy;
		
		target = null;
		startPosition = transform.position;
	
		//networking:
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
		//database.readPrimaryStats();
	}
	// Update is called once per frame
	void FixedUpdate () {
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

	protected void initializeMoney(){
		if(level <= 1){
			soulShards = 300;
		}
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
		if (criticalHitCheck ()) {
			return spellPower*spellCriticalDamage;
		}
		return spellPower;
	}

	public override double getDamageCanMiss(){
		if(!hitCheck()){
			return 0;
		}
		if (criticalHitCheck ()) {
			return spellPower*spellCriticalDamage;
		}
		return spellPower;
	}

	public Fighter getFighter(){
		return fighter;
	}

	public void gainSouls(int souls){
		soulShards =+ souls;
	}

	public override void levelUp(){
		Debug.Log("leveled up");
		usableSkillPoints++;
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
		if(fighter == null){
			fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		}
		castSpeed += wisdom * castSpeedBase;
		cdr += wisdom * cdrBase;
		spellCriticalChance += wisdom * spCritChanBase;;
		spellCriticalDamage += intelligence * spCritDmgBase;
		
		spellPower += intelligence * spPowerBase;
		fighter.maxEnergy += spirit * enBase;//not sure of this. we may ned to adjust who holds energy because fighter holds it atm
		
		energyRegen += spirit * enRegBase;
		
		fighter.energy = fighter.maxEnergy;
	}

	public override bool isDead(){
		return fighter.isDead ();
	}

	public override bool loseEnergy(float energy){

		if (energy > fighter.energy) {
			return false;
		}
		fighter.loseEnergy (energy);
		// networking event listener:
		sorcererNetworkScript.onEnergyLost (fighter.energy);// (this.energy);

		return true;
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
