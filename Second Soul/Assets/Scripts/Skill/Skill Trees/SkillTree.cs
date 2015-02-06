using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTree : MonoBehaviour {

	protected Player player;

	protected bool isSkillOpen;
	
	public Rect position;
	public Texture2D image;
	
	// list of skill nodes
	protected List<SkillTreeNode> skillTree;
	
	// offset positions relative to same skilltree node
	public List<Rect> nodePositions;
	
	// current selected skill (on hover)
	public SkillTreeNode target;
	
	
	void Awake(){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player.skillTree = this;
	}
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
		skillTree = new List<SkillTreeNode>();
		setNodePositionOffsets();
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
			onSkillNodeHover();
			checkTargetAction();
		}
	}
	
	protected void toggleTab(){
		isSkillOpen = !isSkillOpen;
	}
	
	protected void drawSkillTree(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}
	
	protected void drawSkillTreeNodes(){
		for(int i = 0; i < skillTree.Count; i++){
			Rect nodePos = nodePositions[i];
			nodePos.x = nodePositions[i].x + position.x;
			nodePos.y = nodePositions[i].y + position.y;
			skillTree[i].position = nodePos;
			GUI.DrawTexture(skillTree[i].position, skillTree[i].icon);
		}
	}
	
	protected void setNodePositionOffsets(){
		nodePositions = new List<Rect>();
		nodePositions.Add(new Rect(79, 18, 43, 48));
		nodePositions.Add(new Rect(19, 106, 43, 48));
		nodePositions.Add(new Rect(138, 107, 43, 48));
		nodePositions.Add(new Rect(19, 194, 43, 48));
	}
	
	//public abstract void createSkillTree();
	
	public virtual void createSkillTree(){
		SkillTreeNode fb = addSkillTreeNode(typeof(FireballSkill), "Fireball", "Throw a fireball", 
		                                    nodePositions[0], FireballModel.getImage());
		SkillTreeNode ls = addSkillTreeNode(typeof(LightningStormSkill), "Lightning Storm", "...", 
		                                    nodePositions[1], LightningModel.getImage());
		SkillTreeNode cy = addSkillTreeNode(typeof(CycloneSkill), "Cyclone", "...", 
		                                    nodePositions[2], HealModel.getImage());
		// order: (parent, child)
		fb.makeAvailable();
		setSkillTreeNodeLinks(fb, ls);
		setSkillTreeNodeLinks(fb, cy);	
	}
	
	protected SkillTreeNode addSkillTreeNode(System.Type skill, string name, string desc, Rect pos, Texture2D img){
		SkillTreeNode skillTreeNode = new SkillTreeNode(skill, name, desc, pos, img);
		skillTree.Add(skillTreeNode);
		return skillTreeNode;
	}
	
	protected void setSkillTreeNodeLinks(SkillTreeNode parent, SkillTreeNode child){
		parent.addChild(child);
		child.addParent(parent);
	}
	
	protected void onSkillNodeHover(){
		foreach(SkillTreeNode node in skillTree){
			if(node.position.Contains(mousePositionInSkillTree())){
				target = node;
				Debug.Log (target.skillName + " is av: " + target.isAvailable() + " is un: " + target.isUnlocked());
			}
		}
	}
	
	protected void checkTargetAction(){
		//make sure target is set, and mouse is still in target position (since target doesn't go back null)
		if(target != null && target.position.Contains(mousePositionInSkillTree())){
			//on mouse click, if target skill is avalable BUT not unlocked
			if(Input.GetMouseButtonDown(0) && target.isAvailable() && !target.isUnlocked()){
				//if player has usable skill points
				if(player.usableSkillPoints > 0){
					target.makeUnlocked();
					unlockSkill(target);
				}
			}
		}
	}
	
	protected void unlockSkill(SkillTreeNode s){
		SkillNode newSkill = new SkillNode(s.GetType(), s.skillName, s.skillDesc, s.position, s.icon);
		player.unlockedSkills.Add(newSkill);
	}
	
	protected Vector2 mousePositionInSkillTree(){
		return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}
}
