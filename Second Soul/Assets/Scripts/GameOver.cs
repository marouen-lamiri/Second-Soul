using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	//Variable declaration
	bool isDead;
	public Character player;

	// Update is called once per frame, checks if the player is dead
	void FixedUpdate () {
		if(player.isDead ())
		{
			Death();
		}
	}

	//Checks if the player is dead, if he is, stops time
	void Death () {
		if (isDead == true)
		{
			Time.timeScale = 1;
			isDead = false;
		}
		else
		{
			Time.timeScale = 0;
			isDead = true;
		}
	}

	//Display GameOver Screen
	void OnGUI() {
		if(isDead)
		{
			GUI.Label (new Rect (Screen.width/2 - 75, Screen.height/2 - 30, 300, 100),"<Color=red><size=15>This is the end of his story \nA story that will never be completed \nAn unfullifed Destin \nHis destination is death \nOnly his regrets remain</size></Color>");
		}
	}


}
