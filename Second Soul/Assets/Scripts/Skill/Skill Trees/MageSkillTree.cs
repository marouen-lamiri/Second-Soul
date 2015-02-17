using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MageSkillTree : SorcererSkillTree {
	
	protected SkillTreeNode iceShard;
	
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
			equipSkill();
		}
	}
	
	public override void createSkillTree(){
		base.createSkillTree();
		iceShard = addSkillTreeNode(typeof(IceShardSkill), "Ice Shard", "...", 
		                               nodePositions[3], IceShardModel.getImage());
		// order: (parent, child)
		setSkillTreeNodeLinks(lightStorm, iceShard);
	}
}