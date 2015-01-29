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
		this.parents = new List<SkillTreeNode>();
		this.children = new List<SkillTreeNode>();
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
	
	public void addParent(SkillTreeNode s){
		parents.Add(s);
	}
	
	public void addChild(SkillTreeNode s){
		children.Add(s);
	}
	
	public ISkill getSkill(){
		return skill;
	}

}
