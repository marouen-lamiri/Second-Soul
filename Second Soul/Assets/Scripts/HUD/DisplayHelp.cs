
using UnityEngine;
using System.Collections;

public class DisplayHelp : MonoBehaviour {
	
	//Variables
	public bool isHelpDisplayed = false;
	public Font myFont;
	
	//Checks if the 'h'-button was pressed
	void Update()
	{
		if (Input.GetKeyDown ("h")) 
		{
			boolChange ();
		}
	}
	
	//Draw the controls on the screen
	void OnGUI () {
		GUI.skin.font = myFont;
		if (isHelpDisplayed) {
			GUI.Box (new Rect (Screen.width * 0.005f, Screen.height * 0.01f, Screen.width * 0.5f, Screen.height * 0.4f), "-Controls-");
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.07f, Screen.width * 0.5f, Screen.height * 0.4f), "LeftClick   - Move to the clicked position and Basic attack." );
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.11f, Screen.width * 0.5f, Screen.height * 0.4f), "RightClick - For secondary ability." );
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.17f, Screen.width * 0.5f, Screen.height * 0.4f), "' e ' - Switch the character (eg. Fighter/Sorcerer)." );
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.21f, Screen.width * 0.5f, Screen.height * 0.4f), "' s ' - Display game's statistics." );
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.4f), "' i '  - Display player's inventory." );
		}
	}
	
	//Change the status of the display (displaying or not)
	public void boolChange (){
		if (isHelpDisplayed) {
			isHelpDisplayed = false;
		} 
		else {
			isHelpDisplayed = true;
		}
	}
}
