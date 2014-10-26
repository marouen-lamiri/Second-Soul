using UnityEngine;
using System.Collections;

public class Pausing : MonoBehaviour {

	//Variables
	bool isPaused;

	// Update is called once per frame, checks if p is pressed
	void Update () {
		if(Input.GetKeyDown("p"))
		{
			Pause();
		}
	}

	void Pause(){
		if (isPaused == true)
		{
			Time.timeScale = 1;
			isPaused = false;
		}
		else
		{
			Time.timeScale = 0;
			isPaused = true;
		}
	}

	void OnGUI() {
		if(isPaused)
		{
			GUI.Label (new Rect (Screen.width/2 - 75, Screen.height/2 - 25, 200, 100),"<Color=red><size=40>-Paused-</size></Color>");
		}
	}
}
