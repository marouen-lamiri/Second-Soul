using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTreeNode : SkillNode {

	bool unlocked;
	bool available;
	
	List<SkillTreeNode> parents;
	List<SkillTreeNode> children;
	
	public SkillTreeNode(System.Type s, string name, string desc, Rect pos, Texture2D icon) 
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
				child.makeAvailable();
			}
		}
	}
	
	public void addParent(SkillTreeNode s){
		parents.Add(s);
	}
	
	public void addChild(SkillTreeNode s){
		children.Add(s);
	}

	public bool isUnlocked(){
		return unlocked;
	}
	
	public bool isAvailable(){
		return available;
	}
	
	public void makeUnlocked(){
		unlocked = true;
		makeChildrenAvailable();
	}
	
	public void makeAvailable(){
		available = true;
	}
}
