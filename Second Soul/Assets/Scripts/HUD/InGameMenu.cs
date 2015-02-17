using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	//Variables
	bool isPaused;
	//FighterNetworkScript fighterNetworkScript;
	
	void Start() {
		//fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
	}
	
	// Update is called once per frame, checks if Escape-button is pressed.
	void Update () {
		if(Input.GetKeyDown("escape"))
		{
			//Debug.Log("Escape Pressed");

			Pause();

			// networking event listener:
			//fighterNetworkScript.onPauseGame();
		}
	}
	
	public void Pause(){
		if (isPaused == true)
		{
			isPaused = false;
		}
		else
		{
			isPaused = true;
		}
	}
	
	void OnGUI() {

		// Iterator variable.
		var i = 0;

		// Box and Buttons variables.
		var boxWidth  = 200;
		var boxHeight = 300;
		var buttonWidth  = 150;
		var buttonHeight = 50;
		var spaceButtons = 20;

		// Screen percentage: 50% of the screen.
		var screenWidth50 = Screen.width / 2;
		var screenHeight50 = Screen.height / 2;

		// Box position.
		var boxWidthPosition = screenWidth50 - boxWidth / 2;
		var boxHeightPosition = screenHeight50 - boxHeight / 2;

		if(isPaused)
		{
			// Drawing the Box.
			GUI.Box(new Rect(screenWidth50 - boxWidth/2, screenHeight50 - boxHeight/2, boxWidth, boxHeight), "Main Menu");

			// Drawing the Buttons.
			if (GUI.Button(new Rect (screenWidth50 - buttonWidth/2, boxHeightPosition + (i++ * buttonHeight) + (i * spaceButtons), buttonWidth, buttonHeight), "Exit Menu"))
			{

			}

			if (GUI.Button(new Rect (screenWidth50 - buttonWidth/2, boxHeightPosition + (i++ * buttonHeight) + (i * spaceButtons), buttonWidth, buttonHeight), "Resume"))
			{
				
			}

			if (GUI.Button(new Rect (screenWidth50 - buttonWidth/2, boxHeightPosition + (i++ * buttonHeight) + (i * spaceButtons), buttonWidth, buttonHeight), "Options"))
			{
				
			}

			if (GUI.Button(new Rect (screenWidth50 - buttonWidth/2, boxHeightPosition + (i++ * buttonHeight) + (i * spaceButtons), buttonWidth, buttonHeight), "Exit"))
			{
				Application.Quit();
			}
		}
	}
}
