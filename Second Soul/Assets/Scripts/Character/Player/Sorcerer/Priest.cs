using UnityEngine;
using System.Collections;

public class Priest : Sorcerer {

	// Use this for initialization
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
			skillTreeGameObject.AddComponent(typeof(PriestSkillTree));
			skillTree = (PriestSkillTree) GameObject.FindObjectOfType (typeof (PriestSkillTree));
			skillTree.setPlayer(this);
			//skillTree.findPlayer(this.GetType());
		}
	}

	protected override void initializePrimaryStats(){
		intelligencePerLvl = 2;
		wisdomPerLvl = 1;
		spiritPerLvl = 3;
		
		intelligence = 15;
		wisdom = 10;
		spirit = 20;
	}

	public override void initializeSecondaryStats(){
		
		castSpeed = 1f;
		cdr = 0f;
		
		spellCriticalChance = 0.1f;
		spellCriticalDamage = 1.5f;
		
		spellPower = 15.0f;
		
		fighter.maxEnergy = 300;
		
		energyRegen = 1.0f;
	}
	
	public override void initializeSecondaryStatsBase(){		
		
		castSpeedBase = 0.01f;
		cdrBase = 0.01f;
		
		spCritChanBase = 0.005f;
		spCritDmgBase = 0.02f;
		
		spPowerBase = 0.02f;
		
		enBase = 4;
		
		enRegBase = 0.03f;
	}
}
