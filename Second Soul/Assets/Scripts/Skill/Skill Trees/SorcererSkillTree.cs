using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SorcererSkillTree : SkillTree {
	
	protected SkillTreeNode fireball;
	protected SkillTreeNode lightStorm;
	protected SkillTreeNode cyclone;
	
	void Awake(){

	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void createSkillTree(){
		fireball = addSkillTreeNode(typeof(FireballSkill), "Fireball", "Throw a fireball", 
		                                    nodePositions[0], FireballModel.getImage());
		lightStorm = addSkillTreeNode(typeof(LightningStormSkill), "Lightning Storm", "...", 
		                                    nodePositions[1], LightningModel.getImage());
		cyclone = addSkillTreeNode(typeof(CycloneSkill), "Cyclone", "...", 
		                                    nodePositions[2], HealModel.getImage());
		
		fireball.makeAvailable();
		// order: (parent, child)
		setSkillTreeNodeLinks(fireball, lightStorm);
		setSkillTreeNodeLinks(fireball, cyclone);	
	}
}
