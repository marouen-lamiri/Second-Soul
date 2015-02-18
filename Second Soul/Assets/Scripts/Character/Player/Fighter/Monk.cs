using UnityEngine;
using System.Collections;

public class Monk : Fighter {

	// Use this for initialization
	void Start () {
		fighterStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fighterUpdate ();
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 1;
		dexterityPerLvl = 3;
		endurancePerLvl = 2;
		
		strength = 10;
		dexterity = 20;
		endurance = 15;
	}

	public override void initializeSecondaryStats(){
		armor = 50;
		fireResistance = 12;
		coldResistance = 12;
		lightningtResistance = 12;
		
		accuracy = 0.9f;
		attackSpeed = 1.6f;
		
		criticalChance = 0.6f;
		criticalDamage = 1.5f;
		
		attackPower = 10f;
		
		maxHealth = 250;
		
		healthRegen = 0.75f;
	}
	
	public override void initializeSecondaryStatsBase(){		
		armorBase = 5;
		fireResBase = 2;
		coldResBase = 2;
		lightResBase = 2;
		
		accurBase = 0.001f;
		attSpeedBase = 0.01f;
		
		attPowerBase = 0.01f;
		
		critChanBase = 0.02f;
		critDmgBase = 0.01f;
		
		attPowerBase = 0.01f;
		
		hpBase = 7;
		
		hpRegBase = 0.02f;
	}
}
