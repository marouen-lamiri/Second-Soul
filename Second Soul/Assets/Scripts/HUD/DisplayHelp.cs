
using UnityEngine;
using System.Collections;

public class DisplayHelp : MonoBehaviour {
	
	//Variables
	public bool isHelpDisplayed = false;
	public GUIStyle style;
	public GUIStyle labelStyle;
	public Font myFont;
	float height = 25f;
	float width = 300f;
	float basicScreenHeight = 320;
	float basicScreenWidth = 797;
	float ratioWidth;
	float ratioHeight;
	float nbrOfDraws = 5; //value has to be changed depending on the number of labels to be drawn
	float offset;
	
	//Checks if the 'h'-button was pressed
	void FixedUpdate()
	{
		ratioHeight = Screen.height/basicScreenHeight;
		ratioWidth = Screen.width/basicScreenWidth;
		offset = ratioWidth * 0.01f;
		if (Input.GetKeyDown ("h")) 
		{
			boolChange ();
		}
	}
	
	//Draw the controls on the screen
	void OnGUI () { 
		GUI.skin.button = style;
		GUI.skin.box = style;
		GUI.skin.font = myFont;
		if (isHelpDisplayed) {
			GUI.Box (new Rect (Screen.width * 0.005f, Screen.height * 0.01f, width  * ratioWidth, height * nbrOfDraws * ratioHeight),"", style);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.03f, width * ratioWidth - offset, height * ratioHeight), "<Size=20>-Controls-</Size>", labelStyle);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.13f, width , height * ratioHeight), "LeftClick - Move character and attack.", labelStyle);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.18f, width, height * ratioHeight), "RightClick - For secondary ability.", labelStyle);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.23f, width * ratioWidth - offset, height * ratioHeight), "'e' - Switch between characters.", labelStyle);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.08f, width * ratioWidth - offset, height * ratioHeight), "'s' - Display game's statistics.", labelStyle);
			GUI.TextArea (new Rect (Screen.width * 0.01f, Screen.height * 0.28f, width * ratioWidth - offset, height * ratioHeight), "'i' - Display player's inventory.", labelStyle);
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
