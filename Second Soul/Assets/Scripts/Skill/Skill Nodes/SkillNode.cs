using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillNode {

	//public ISkill skill;
	//public string skillClassName;
	public System.Type skillType;
	
	public string skillName;
	public string skillDesc;
	
	public Rect position;
	public Texture2D icon;
	
	public SkillNode(System.Type s, string name, string desc, Rect pos, Texture2D icon){
		this.skillType = s;
		this.skillName = name;
		this.skillDesc = desc;
		this.position = pos;
		this.icon = icon;
	}
	
	
	public System.Type getSkillType(){
		return skillType;
	}
}
