using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTree : MonoBehaviour {

	private Player player;

	bool isSkillOpen;
	
	public Rect position;
	public Texture2D image;
	
	List<SkillTreeNode> skillTree;
	
	
	void Awake(){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player.skillTree = this;
	}
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
		skillTree.Add(new SkillTreeNode(new FireballSkill(), "Fireball", "Throw a fireball", 
										new Rect(0,0,0,0), FireballModel.getImage()));
		skillTree.Add(new SkillTreeNode(new LightningStormSkill(), "Lighting Storm", "...", 
		                                new Rect(0,0,0,0), LightingModel.getImage()));
		skillTree.Add(new SkillTreeNode(new HealSkill(), "Heal", "...", 
		                                new Rect(0,0,0,0), HealModel.getImage()));
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
		}
	}
	
	private void toggleTab(){
		isSkillOpen = !isSkillOpen;
	}
	
	private void drawSkillTree(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}
	
	private void addSkillTreeNode(ISkill skill, string name, string desc, Rect pos, Texture2D img){
		skillTree.Add(new SkillTreeNode(skill, name, desc, pos, img));
	}
	
	private void unlockSkill(SkillTreeNode skillTreeNode){
		ISkill skill = skillTreeNode.getSkill();
		player.unlockedSkills.Add(skill);
	}
}
