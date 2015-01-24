using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillNode {

	ISkill skill;
	
	string skillName;
	string skillDesc;
	
	Rect position;
	Texture2D icon;
	
	public SkillNode(ISkill s, string name, string desc){
		this.skill = s;
		this.skillName = name;
		this.skillDesc = desc;
	}

}
