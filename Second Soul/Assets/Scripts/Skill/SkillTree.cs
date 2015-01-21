using UnityEngine;
using System.Collections;

public class SkillTree : MonoBehaviour {

	bool isSkillOpen;
	
	public Rect position;
	public Texture2D image;
	
	// Use this for initialization
	void Start () {
		isSkillOpen = false;
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
}
