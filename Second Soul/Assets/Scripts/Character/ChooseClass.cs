using UnityEngine;
using System.Collections;

public class ChooseClass : MonoBehaviour {
	// Positioning
	public int windowX;
	public int windowY;
	public int windowButtonWidth;
	public int windowButtonHeight;

	// Represents the Sub-Classes for both Main-Classes
	public int fighterSelection = 0;
	public string[] fighterSelectionStrings = {"Berserker", "Knight", "Monk"};
	public int sorcererSelection = 0;
	public string[] sorcererSelectionStrings = {"Mage", "Priest", "Druid"};

	public void Awake() {
		windowX = 10;
		windowY = 10;
		windowButtonWidth = 100;
		windowButtonHeight = 25;
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
		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY + windowButtonHeight * 1, windowButtonWidth, windowButtonHeight), "Second Soul", centeredStyle);
		GUI.Label(new Rect(screenWidth50 - labelWidth50, windowY + windowButtonHeight * 2, windowButtonWidth, windowButtonHeight), "Class Selection", centeredStyle);

		// Main-Classes.
		/*group-1*/GUI.Label(new Rect(screenWidth25 - labelWidth50, windowY + windowButtonHeight * 4, windowButtonWidth, windowButtonHeight), "Fighter:", centeredStyle);
		/*group-2*/GUI.Label(new Rect(screenWidth75 - labelWidth50, windowY + windowButtonHeight * 4, windowButtonWidth, windowButtonHeight), "Sourcerer:", centeredStyle);

		// Sub-Classes.
		/*group-1*/fighterSelection = GUI.SelectionGrid (new Rect (screenWidth25 - labelWidth50, windowY + windowButtonHeight * 5, windowButtonWidth, windowButtonHeight*3), fighterSelection, fighterSelectionStrings, 1);
		/*group-2*/sorcererSelection = GUI.SelectionGrid (new Rect (screenWidth75 - labelWidth50, windowY + windowButtonHeight * 5, windowButtonWidth, windowButtonHeight*3), sorcererSelection, sorcererSelectionStrings, 1);

		// Start-Button.
		GUI.Button(new Rect(screenWidth50 - labelWidth50, windowY + windowButtonHeight * 10, windowButtonWidth, windowButtonHeight), "Start");

	}
}
