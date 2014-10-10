using UnityEngine;
using System.Collections;

public class DisplayPlayerStats : MonoBehaviour {

	public PlayerCombat player;
	public bool isStatsDisplayed = false;
	public float countdown = 3.0f;
	
	void Update()
	{
		if (Input.GetKey ("s")) 
		{
			boolChange ();
		}
	}

	void OnGUI () {
		if (isStatsDisplayed) {
			GUI.Box (new Rect (10, 10, 150, 150), "Character Stats");
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
