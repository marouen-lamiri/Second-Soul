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
	
	public List<Rect> nodePositions;
	
	
	void Awake(){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player.skillTree = this;
	}
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
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
	
	private void drawSkillTreeNodes(){
		foreach(SkillTreeNode node in skillTree){
			node.position.x += position.x;
			node.position.y += position.y;
			Debug.Log(node.position);
			Debug.Log(node.icon);
			GUI.DrawTexture(node.position, node.icon);
		}
	}
	
	public void createSkillTree(){
		SkillTreeNode fb = addSkillTreeNode(new FireballSkill(), "Fireball", "Throw a fireball", 
		                                    nodePositions[0], FireballModel.getImage());
		SkillTreeNode ls = addSkillTreeNode(new LightningStormSkill(), "Lighting Storm", "...", 
		                                    nodePositions[1], LightingModel.getImage());
		SkillTreeNode he = addSkillTreeNode(new HealSkill(), "Heal", "...", 
		                                    nodePositions[2], HealModel.getImage());
		// order: (parent, child)
		setSkillTreeNodeLinks(fb, ls);
		setSkillTreeNodeLinks(fb, he);	
	}
	
	private SkillTreeNode addSkillTreeNode(ISkill skill, string name, string desc, Rect pos, Texture2D img){
		SkillTreeNode skillTreeNode = new SkillTreeNode(skill, name, desc, pos, img);
		skillTree.Add(skillTreeNode);
		return skillTreeNode;
	}
	
	private void setSkillTreeNodeLinks(SkillTreeNode parent, SkillTreeNode child){
		parent.addChild(child);
		child.addParent(parent);
	}
	
	private void unlockSkill(SkillTreeNode skillTreeNode){
		ISkill skill = skillTreeNode.getSkill();
		player.unlockedSkills.Add(skill);
	}
}
