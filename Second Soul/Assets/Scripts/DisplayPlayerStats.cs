﻿using UnityEngine;
using System.Collections;

public class DisplayPlayerStats : MonoBehaviour {

	//Variables
	public Fighter player;
	public bool isStatsDisplayed = false;
	public Font myFont;
	
	//Checks if the s button was pressed
	void Update()
	{
		if (Input.GetKeyDown ("s")) 
		{
			boolChange ();
		}
	}

	//Draw the Stats on the screen
	void OnGUI () {
		GUI.skin.font = myFont;
		if (isStatsDisplayed) {
			GUI.Box (new Rect (Screen.width * 0.005f, Screen.height * 0.01f, Screen.width * 0.2f, Screen.height * 0.2f), "-Character Stats-");
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.05f, Screen.width * 0.2f, Screen.height * 0.2f), "Health: " + player.health + " /" + player.maxHealth);
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.08f, Screen.width * 0.2f, Screen.height * 0.2f), "Energy: " + player.energy  + " /" + player.maxEnergy); 
			GUI.Label (new Rect (Screen.width * 0.01f, Screen.height * 0.11f, Screen.width * 0.2f, Screen.height * 0.2f), "Speed: " + player.speed);
		}
	}

	//Change the status of the display (displaying or not)
	void boolChange (){
		if (isStatsDisplayed) {
			isStatsDisplayed = false;
		} 
		else {
			isStatsDisplayed = true;
		}
	}
}
