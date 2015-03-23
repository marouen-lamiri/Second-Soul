﻿using UnityEngine;
using System.Collections;

public class DungeonToBoss : SceneManager {

	public GUIStyle buttons;
	protected Fighter fPosition;
	protected Sorcerer sPosition;
	protected int minPossibility = 0;
	protected int maxPossibility = 2;
	protected string playerTag = "Player";
	protected string sceneName = "SkillTree";
	protected string sceneNameBoss = "BossScene";
	protected string teleporter = "Teleporter2";
	
	
	// Use this for initialization
	void Start () {
		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
	}
	
	// Update is called once per frame
	void OnGUI () {
		clicked ();
		if(showMenu){
			menu ();
		}
	}
	
	void clicked(){
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distance)){
				if (hit.transform.name == teleporter){
					showMenu = true;
				}
			}
		}
	}
	
	void menu(){
		GUIStyle centeredStyle = GUI.skin.GetStyle("textarea");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = sizeFont;
		GUI.Box (new Rect (Screen.width/3 - Screen.width/36, Screen.height/3 + Screen.height/32, Screen.width/3  + Screen.width/22, Screen.height/3), greeting, centeredStyle);
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + Screen.height/12, Screen.width/3, Screen.height/12), okMessage, buttons)){
			int randomTech = Random.Range (minPossibility, maxPossibility);
			if(randomTech == minPossibility){
				NetworkLevelLoader.Instance.LoadLevel(sceneNameBoss,1);
			}
			else{
				NetworkLevelLoader.Instance.LoadLevel(sceneName,1);
			}
		}
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 2 * Screen.height/12, Screen.width/3, Screen.height/12), noMessage, buttons)){
			showMenu = !showMenu;
		}
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 3 * Screen.height/12, Screen.width/3, Screen.height/12), closeMessage, buttons)){
			showMenu = !showMenu;
		}
	}
}
