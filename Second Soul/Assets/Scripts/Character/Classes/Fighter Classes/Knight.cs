using UnityEngine;
using System.Collections;

public class Knight : Fighter {

	// Use this for initialization
	void Start () {
		fighterStart ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 2;
		dexterityPerLvl = 1;
		endurancePerLvl = 3;
		
		strength = 15;
		dexterity = 10;
		endurance = 20;
	}

	public override void initializeSecondaryStats(){
		armor = 60;
		fireResistance = 15;
		coldResistance = 15;
		lightningtResistance = 15;
		
		accuracy = 0.6f;
		attackSpeed = 0.8f;
		
		criticalChance = 0.1f;
		criticalDamage = 2.0f;
		
		attackPower = 1.5f;
		
		maxHealth = 300;
		
		healthRegen = 1.0f;
	}
	
	public override void initializeSecondaryStatsBase(){		
		armorBase = 6;
		fireResBase = 3;
		coldResBase = 3;
		lightResBase = 3;
		
		accurBase = 0.0008f;
		attSpeedBase = 0.008f;
		
		attPowerBase = 0.01f;
		
		critChanBase = 0.005f;
		critDmgBase = 0.015f;
		
		attPowerBase = 0.018f;
		
		hpBase = 10;
		
		hpRegBase = 0.03f;
	}
}
