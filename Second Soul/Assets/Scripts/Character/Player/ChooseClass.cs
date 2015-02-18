using UnityEngine;
using System.Collections;

public class ChooseClass : MonoBehaviour {
	//Prefabs
	public Berserker berserker;
	public Knight knight;
	public Monk monk;
	public Sorcerer mage;
	public Druid druid;
	public Priest priest;

	// Positioning.
	public int windowX;
	public int windowY;
	public int windowButtonWidth;
	public int windowButtonHeight;

	// Represents the Sub-Classes for both Main-Classes.
	public int fighterSelection = 0;
	public string[] fighterSelectionStrings = {"Berserker", "Knight", "Monk"};
	public int sorcererSelection = 0;
	public string[] sorcererSelectionStrings = {"Mage", "Priest", "Druid"};

	// Scene to load when pressing the Start-button.
	public string sceneName;
	
	// Positioning initialization.
	public void Awake() {
//		windowX = 10;
//		windowY = 10;
		UnityNotificationBar.UNotify("Welcome to the Game!"); //although this might appear false in Mono-Develop, it actually works as an external asset
		windowX = 10;
		windowY = 40; // to put it below chat window
		windowButtonWidth = 100;
		windowButtonHeight = 25;
		initializeGameTabs();
		
	}
	
	protected void initializeGameTabs(){
		GameObject gameTabs = GameObject.Find("Game Tabs");
		DontDestroyOnLoad(gameTabs);
	}

	void OnGUI () {
		// Editing the style.
		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 14;

		// Screen percentage: 25%, 50%, 75% of the screen.
		var screenWidth25 = Screen.width / 4;
		var screenWidth50 = Screen.width / 2;
		var screenWidth75 = Screen.width - Screen.width / 4;

		// Label percentage: 50% of the label.
		var labelWidth50 = windowButtonWidth / 2;

		// Header.
//		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY + windowButtonHeight * 1, windowButtonWidth, windowButtonHeight), "Second Soul", centeredStyle);
//		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY + windowButtonHeight * 2, windowButtonWidth, windowButtonHeight), "Class Selection", centeredStyle);
		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY +150+ windowButtonHeight * 1, windowButtonWidth, windowButtonHeight), "Second Soul", centeredStyle);
		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY +150+ windowButtonHeight * 2, windowButtonWidth, windowButtonHeight), "Class Selection", centeredStyle);

		// Main-Classes.
		/*group-1*/GUI.Label(new Rect(screenWidth25 - labelWidth50, windowY + windowButtonHeight * 4, windowButtonWidth, windowButtonHeight), "Fighter:", centeredStyle);
		/*group-2*/GUI.Label(new Rect(screenWidth75 - labelWidth50, windowY + windowButtonHeight * 4, windowButtonWidth, windowButtonHeight), "Sorcerer:", centeredStyle);

		// Sub-Classes.
		/*group-1*/fighterSelection = GUI.SelectionGrid (new Rect (screenWidth25 - labelWidth50, windowY + windowButtonHeight * 5, windowButtonWidth, windowButtonHeight*3), fighterSelection, fighterSelectionStrings, 1);
		/*group-2*/sorcererSelection = GUI.SelectionGrid (new Rect (screenWidth75 - labelWidth50, windowY + windowButtonHeight * 5, windowButtonWidth, windowButtonHeight*3), sorcererSelection, sorcererSelectionStrings, 1);

		// Start-Button.
//		if (GUI.Button (new Rect (screenWidth50 - labelWidth50, windowY + windowButtonHeight * 10, windowButtonWidth, windowButtonHeight), "Start"))
//		{
//			Fighter fighter;
//			Sorcerer sorcerer;
//			if(fighterSelectionStrings[fighterSelection]=="Berserker"){
//				fighter = (Berserker) Instantiate(berserker,Vector3.zero,Quaternion.identity);
//			}
//			else if(fighterSelectionStrings[fighterSelection]=="Knight"){
//				fighter = (Knight) Instantiate(knight,Vector3.zero,Quaternion.identity);
//			}
//			else/*(fighterSelectionStrings[fighterSelection]=="Monk")*/{
//				fighter = (Monk) Instantiate(monk,Vector3.zero,Quaternion.identity);
//			}
//
//			if(sorcererSelectionStrings[sorcererSelection]=="Mage"){
//				sorcerer = (Sorcerer) Instantiate(mage,Vector3.zero,Quaternion.identity);
//			}
//			else if(sorcererSelectionStrings[sorcererSelection]=="Druid"){
//				sorcerer = (Druid) Instantiate(druid,Vector3.zero,Quaternion.identity);
//			}
//			else/*(sorcererSelectionStrings[sorcererSelection]=="Priest")*/{
//				sorcerer = (Priest) Instantiate(priest,Vector3.zero,Quaternion.identity);
//			}
//			fighter.gameObject.name = "Fighter";
//			sorcerer.gameObject.name = "Sorcerer";
//			DontDestroyOnLoad(fighter);
//			DontDestroyOnLoad(sorcerer);
//
//			Application.LoadLevel (sceneName);
//		}
	}
}
