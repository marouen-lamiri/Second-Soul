using UnityEngine;
using System.Collections;

public class DisplayPlayerStats : MonoBehaviour {

	//Variables
	private Fighter player;
	public GUIStyle style;
	public GUIStyle labelStyle;
	public bool isStatsDisplayed = false;
	public Font myFont;
	float height = 25f;
	float width = 225f;
	float basicScreenHeight = 320;
	float basicScreenWidth = 797;
	float boxPercentage = 0.005f;
	float boxHeight = 0.01f;
	float labelHeight = 0.03f;
	float labelHeightReset = 0.03f;
	float heightIncrease = 0.05f;
	float ratioWidth;
	float ratioHeight;
	float nbrOfDraws = 5; //value has to be changed depending on the number of labels to be drawn
	float offset;

	void Start(){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	//Checks if the s button was pressed
	void FixedUpdate()
	{
		ratioHeight = Screen.height/basicScreenHeight;
		ratioWidth = Screen.width/basicScreenWidth;
		offset = ratioWidth * 0.01f;
		if (Input.GetKeyDown ("s")) 
		{
			boolChange ();

			//networking event listener:
			FighterNetworkScript fighterNetworkScript = (FighterNetworkScript) GameObject.FindObjectOfType(typeof(FighterNetworkScript));
			fighterNetworkScript.onStatsDisplayed();
		}
	}

	//Draw the Stats on the screen
	void OnGUI () {
		GUI.skin.font = myFont;
		GUI.skin.box = style;
		labelHeight = labelHeightReset;
		if (isStatsDisplayed) {
			GUI.Box (new Rect (Screen.width * boxPercentage, Screen.height * boxHeight, width  * ratioWidth, height * nbrOfDraws * ratioHeight), "");
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Size=20><Color=black>-Character Stats-</Color></Size>", labelStyle);
			labelHeight =+ heightIncrease;
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Color=black>Health: </Color>" + (int)player.health + "/" + (int)player.maxHealth, labelStyle);
			labelHeight =+ heightIncrease;
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Color=black>Energy: </Color>" + (int)player.energy  + "/" + (int)player.maxEnergy, labelStyle); 
			labelHeight =+ heightIncrease;
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Color=black>Speed: </Color>" + player.speed, labelStyle);
			labelHeight =+ heightIncrease;
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Color=black>Defense: </Color>" + player.armor, labelStyle);
			labelHeight =+ heightIncrease;
			GUI.Label (new Rect (Screen.width * boxPercentage * 2, Screen.height * labelHeight, width , height * ratioHeight), "<Color=black>Level: </Color>" + player.level, labelStyle);
		}
	}

	//Change the status of the display (displaying or not)
	public void boolChange (){
		if (isStatsDisplayed) {
			isStatsDisplayed = false;
		} 
		else {
			isStatsDisplayed = true;
		}
	}
}
