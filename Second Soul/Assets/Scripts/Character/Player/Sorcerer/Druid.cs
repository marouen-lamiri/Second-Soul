using UnityEngine;
using System.Collections;

public class Druid : Sorcerer {

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
			skillTreeGameObject.AddComponent(typeof(DruidSkillTree));
			skillTree = (DruidSkillTree) GameObject.FindObjectOfType (typeof (DruidSkillTree));
			skillTree.setPlayer(this);
			//skillTree.findPlayer(this.GetType());
		}
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
		
		castSpeed = 1f;
		cdr = 0.3f;

		spellCriticalChance = 0.3f;
		spellCriticalDamage = 1.5f;
		
		spellPower = 10f;
		
		fighter.maxEnergy = 200;
		
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
