﻿using UnityEngine;
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

	public Database database;
	
	//FighterNetworkScript fighterNetworkScript; // is "already serialized" in parent class Player.cs.

	// Use this for initialization
	void Start () {
		fighterStart ();
		database.readPrimaryStats();
	}
	protected void fighterStart(){
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
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();
		
		//networking:
		fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
	}
	// Update is called once per frame
	void Update () {
		fighterUpdate ();
	}

	protected void fighterUpdate(){
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

	public virtual bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= criticalChance * 100) {
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
			return damage*attackPower*criticalDamage;
		}
		return damage * attackPower;
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
}
