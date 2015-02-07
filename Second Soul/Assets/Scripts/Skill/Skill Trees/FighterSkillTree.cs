﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FighterSkillTree : SkillTree {

	protected SkillTreeNode cleave;
	protected SkillTreeNode charge;
	protected SkillTreeNode cyclone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void createSkillTree(){
		cleave = addSkillTreeNode(typeof(Cleave), "Cleave", "...", 
		                                    nodePositions[0], CleaveModel.getImage());
		charge = addSkillTreeNode(typeof(Charge), "Charge", "...", 
		                                    nodePositions[1], ChargeModel.getImage());
		cyclone = addSkillTreeNode(typeof(SpinAttack), "Cyclone", "...", 
		                                    nodePositions[2], HealModel.getImage());
		                                    
		cleave.makeAvailable();
		// order: (parent, child)
		setSkillTreeNodeLinks(cleave, charge);
		setSkillTreeNodeLinks(cleave, cyclone);	
	}
}