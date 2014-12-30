using UnityEngine;
using System.Collections;

public class Druid : Sorcerer {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void initializePrimaryStats(){
		intelligencePerLvl = 1;
		wisdomPerLvl = 3;
		spiritPerLvl = 2;
		
		intelligence = 10;
		wisdom = 20;
		spirit = 15;
	}

	public override void initializeSecondaryStats(){
		
		castSpeed = 3f;
		cdr = 0.3f;
		
		spellCriticalChance = 0.3f;
		spellCriticalDamage = 1.5f;
		
		spellPower = 10f;
		
		maxEnergy = 200;
		
		energyRegen = 0.75f;
	}
	
	public override void initializeSecondaryStatsBase(){		
		
		castSpeedBase = 0.03f;
		cdrBase = 0.03f;
		
		spCritChanBase = 0.02f;
		spCritDmgBase = 0.01f;
		
		spPowerBase = 0.01f;
		
		enBase = 3;
		
		enRegBase = 0.02f;
	}
}
