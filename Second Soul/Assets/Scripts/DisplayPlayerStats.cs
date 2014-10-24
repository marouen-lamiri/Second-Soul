﻿using UnityEngine;
using System.Collections;

public class DisplayPlayerStats : MonoBehaviour {

	public Fighter player;
	public bool isStatsDisplayed = false;
	public float countdown = 3.0f;
	public Font myFont;
	
	void Update()
	{
		if (Input.GetKey ("s")) 
		{
			boolChange ();
		}
	}

	void OnGUI () {
		GUI.skin.font = myFont;
		if (isStatsDisplayed) {
			GUI.Box (new Rect (10, 10, 150, 150), "-Character Stats-");
			GUI.Label (new Rect (20, 40, 80, 20), "Health: " + player.health);
			GUI.Label (new Rect (20, 70, 80, 20), "Energy: " + player.energy); 
		}
	}

	void boolChange ()
	{
		if (isStatsDisplayed) {
			isStatsDisplayed = false;
		} 
		else {
			isStatsDisplayed = true;
		}
	}
}
