using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTreeNode : SkillNode {

	bool unlocked;
	bool available;
	
	public SkillTreeNode(ISkill s, string name, string desc) : base(s, name, desc){
		this.unlocked = false;
		this.available = false;
	}

}
