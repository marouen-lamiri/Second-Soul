using UnityEngine;
using System.Collections;

public class Berserker : Fighter {

	// Use this for initialization
	void Start () {
		fighterStart ();
		initializeSkillTree();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fighterUpdate ();
	}
	
	protected override void initializeSkillTree(){
		if(playerEnabled){
			base.initializeSkillTree();
			skillTreeGameObject.AddComponent(typeof(BerserkerSkillTree));
			skillTree = (BerserkerSkillTree) GameObject.FindObjectOfType (typeof (BerserkerSkillTree));
			skillTree.setPlayer(this);
			//skillTree.findPlayer(this.GetType());
		}
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 5;
		dexterityPerLvl = 3;
		endurancePerLvl = 2;
		
		strength = 30;
		dexterity = 25;
		endurance = 40;
	}

	public override void initializeSecondaryStats(){
		armor = 200;
		fireResistance = 30;
		coldResistance = 10;
		lightningtResistance = 10;
		
		accuracy = 0.8f;
		attackSpeed = 1.3f;

		criticalChance = 0.3f;
		criticalDamage = 2.5f;
		
		attackPower = 60f;

		maxHealth = 1400;
		
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
