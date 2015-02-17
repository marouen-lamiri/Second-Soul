using UnityEngine;
using System.Collections;

public class Berserker : Fighter {

	// Use this for initialization
	void Start () {
		fighterStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fighterUpdate ();
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 3;
		dexterityPerLvl = 2;
		endurancePerLvl = 1;
		
		strength = 20;
		dexterity = 15;
		endurance = 10;
	}

	public override void initializeSecondaryStats(){
		armor = 70;
		fireResistance = 10;
		coldResistance = 10;
		lightningtResistance = 10;
		
		accuracy = 0.8f;
		attackSpeed = 1.3f;

		criticalChance = 0.3f;
		criticalDamage = 2.5f;
		
		attackPower = 20f;

		maxHealth = 200;
		
		healthRegen = 0.5f;
	}
	
	public override void initializeSecondaryStatsBase(){		
		armorBase = 7;
		fireResBase = 1;
		coldResBase = 1;
		lightResBase = 1;
		
		accurBase = 0.001f;
		attSpeedBase = 0.02f;
		
		attPowerBase = 0.01f;
		
		critChanBase = 0.01f;
		critDmgBase = 0.02f;
		
		attPowerBase = 0.025f;

		hpBase = 5;

		hpRegBase = 0.01f;
	}
}
