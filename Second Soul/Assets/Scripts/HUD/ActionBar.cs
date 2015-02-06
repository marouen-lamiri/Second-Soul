using UnityEngine;
using System.Collections;

public class ActionBar : MonoBehaviour {
	
	protected Player player;

	public Rect position;
	public Texture2D image;

	SkillNode activeSkill1;
	SkillNode activeSkill2;
	SkillNode activeSkill3;
	SkillNode activeSkill4;
	SkillNode activeSkill5;
	SkillNode activeSkill6;

	// Use this for initialization
	void Start () {
		activeSkill1 = null;
		activeSkill2 = null;
		activeSkill3 = null;
		activeSkill4 = null;
		activeSkill5 = null;
		activeSkill6 = null;
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player.actionBar = this;
		//for testing only
		initFakeSkills();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		drawActionBar();
		drawSkillNodes();
	}
	
	//for testing only
	private void initFakeSkills(){
		setActiveSkill1(player.unlockedSkills[0]);
		setActiveSkill2(player.unlockedSkills[1]);
		setActiveSkill3(player.unlockedSkills[2]);
		activeSkill1.icon = FireballModel.getImage();
		activeSkill1.position.x = Screen.width * 0.5f - position.width * 0.5f + 60;
		activeSkill1.position.y = Screen.height - 53;
		activeSkill2.icon = FireballModel.getImage();
		activeSkill2.position.x = Screen.width * 0.5f - position.width * 0.5f + 104;
		activeSkill2.position.y = Screen.height - 53;
		activeSkill3.icon = FireballModel.getImage();
		activeSkill3.position.x = Screen.width * 0.5f - position.width * 0.5f + 148;
		activeSkill3.position.y = Screen.height - 53;
	}
	
	private void drawActionBar(){
		position.x = Screen.width * 0.5f - position.width * 0.5f;
		position.y = Screen.height - position.height;
		GUI.DrawTexture(position, image);
	}
	
	private void drawSkillNodes(){
		if(activeSkill1 != null){
			GUI.DrawTexture(activeSkill1.position, activeSkill1.icon);
		}
		if(activeSkill2 != null){
			GUI.DrawTexture(activeSkill2.position, activeSkill2.icon);
		}
		if(activeSkill3 != null){
			GUI.DrawTexture(activeSkill3.position, activeSkill3.icon);
		}
		if(activeSkill4 != null){
			GUI.DrawTexture(activeSkill4.position, activeSkill4.icon);
		}
		if(activeSkill5 != null){
			GUI.DrawTexture(activeSkill5.position, activeSkill5.icon);
		}
		if(activeSkill6 != null){
			GUI.DrawTexture(activeSkill6.position, activeSkill6.icon);
		}
	}
	
	public void setActiveSkill1(SkillNode skillNode){
		activeSkill1 = skillNode;
		activeSkill1.position = new Rect(position.x + 20,position.y + 3,38,38);
	}
	
	public void setActiveSkill2(SkillNode skillNode){
		activeSkill2 = skillNode;
		activeSkill2.position = new Rect(position.x + 62,position.y + 3,38,38);
	}
	
	public void setActiveSkill3(SkillNode skillNode){
		activeSkill3 = skillNode;
		activeSkill3.position = new Rect(position.x + 104,position.y + 3,38,38);
	}
	
	public void setActiveSkill4(SkillNode skillNode){
		activeSkill4 = skillNode;
		activeSkill4.position = new Rect(position.x + 146,position.y + 3,38,38);
	}
	
	public void setActiveSkill5(SkillNode skillNode){
		activeSkill5 = skillNode;
		activeSkill5.position = new Rect(position.x + 188,position.y + 3,38,38);
	}
	
	public void setActiveSkill6(SkillNode skillNode){
		activeSkill6 = skillNode;
		activeSkill6.position = new Rect(position.x + 230,position.y + 3,38,38);
	}

}
