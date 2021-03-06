﻿using UnityEngine;
using System.Collections;

public class Mage : Sorcerer {

	// Use this for initialization
	void Start () {
		sorcererStart ();
		initializeSkillTree();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		sorcererUpdate ();
	}
	
	protected override void initializeSkillTree(){
		if(playerEnabled){
			base.initializeSkillTree();
			skillTreeGameObject.AddComponent(typeof(MageSkillTree));
			skillTree = (MageSkillTree) GameObject.FindObjectOfType (typeof (MageSkillTree));
			skillTree.setPlayer(this);
			//skillTree.findPlayer(this.GetType());
		}
	}

	protected override void initializePrimaryStats(){
		intelligencePerLvl = 3;
		wisdomPerLvl = 2;
		spiritPerLvl = 1;
		
		intelligence = 20;
		wisdom = 15;
		spirit = 10;
	}

	public override void initializeSecondaryStats(){
	
		castSpeed = 2.8f;
		cdr = 0.15f;
		
		spellCriticalChance = 0.2f;
		spellCriticalDamage = 2.0f;
		
		spellPower = 20f;

		fighter.maxEnergy = 100;

		energyRegen = 0.5f;
	}
	
	public override void initializeSecondaryStatsBase(){		

		castSpeedBase = 0.02f;
		cdrBase = 0.02f;

		spCritChanBase = 0.01f;
		spCritDmgBase = 0.03f;
		
		spPowerBase = 0.03f;

		enBase = 2;

		enRegBase = 0.01f;
	}
}
