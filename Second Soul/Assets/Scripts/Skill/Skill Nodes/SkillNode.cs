using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillNode {

	ISkill skill;
	
	string skillName;
	string skillDesc;
	
	public Rect position;
	public Texture2D icon;
	
	public SkillNode(ISkill s, string name, string desc, Rect pos, Texture2D icon){
		this.skill = s;
		this.skillName = name;
		this.skillDesc = desc;
		this.position = pos;
		this.icon = icon;
	}
}
