using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DruidSkillTree : SorcererSkillTree {

	protected SkillTreeNode windLance;
	
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
		windLance = addSkillTreeNode(typeof(CycloneSkill), "Wind Lance", "...", 
		                               nodePositions[3], WindLanceModel.getImage());
		// order: (parent, child)
		setSkillTreeNodeLinks(lightStorm, windLance);
	}
}
