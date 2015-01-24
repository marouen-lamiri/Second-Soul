using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTreeNode : SkillNode {

	bool unlocked;
	bool available;
	
	List<SkillTreeNode> parents;
	List<SkillTreeNode> children;
	
	public SkillTreeNode(ISkill s, string name, string desc, Rect pos, Texture2D icon) 
	: base(s, name, desc, pos, icon){
		this.unlocked = false;
		this.available = false;
	}
	
	public void makeChildrenAvailable(){	
		foreach(SkillTreeNode child in children){
			bool allParentsUnlocked = true; // turns false if any are not
			foreach(SkillTreeNode parent in parents){
				if(!parent.unlocked){
					allParentsUnlocked = false;
				}
			}
			if(allParentsUnlocked){
				child.available = true;
			}
		}
	}

}
