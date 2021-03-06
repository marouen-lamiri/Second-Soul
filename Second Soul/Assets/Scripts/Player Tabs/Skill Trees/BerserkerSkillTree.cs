﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BerserkerSkillTree : FighterSkillTree {

	protected SkillTreeNode berserkRage;

	void Awake(){
		actionBar = (ActionBar) GameObject.FindObjectOfType (typeof (ActionBar));
		//player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		//player.skillTree = this;
	}
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
		skillTree = new List<SkillTreeNode>();
		initializeGUI();
		setNodePositionOffsets();
		createSkillTree();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("t")) {
			toggleTab();
		}
	}
	
	void OnGUI(){
		if(isSkillOpen){
			drawSkillTree();
			drawSkillTreeNodes();
			onSkillNodeHover();
			checkTargetAction();
			equipSkill();
		}
	}
	
	public override void createSkillTree(){
		base.createSkillTree();
		berserkRage = addSkillTreeNode(typeof(BerserkMode), "Berserker Rage", "...", 
		                                    nodePositions[3], BerserkerRageModel.getImage());
		// order: (parent, child)
		setSkillTreeNodeLinks(cleave, berserkRage);
	}
}
