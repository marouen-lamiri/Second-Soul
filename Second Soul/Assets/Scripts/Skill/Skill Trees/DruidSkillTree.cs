﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DruidSkillTree : SorcererSkillTree {

	protected SkillTreeNode windLance;
	
	void Awake(){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player.skillTree = this;
	}
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
		skillTree = new List<SkillTreeNode>();
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
		}
	}
	
	public override void createSkillTree(){
		base.createSkillTree();
		windLance = addSkillTreeNode(typeof(CycloneSkill), "Wind Lance", "...", 
		                               nodePositions[3], WindLanceModel.getImage());
		// order: (parent, child)
		setSkillTreeNodeLinks(lightStorm, windLance);
	}
}
