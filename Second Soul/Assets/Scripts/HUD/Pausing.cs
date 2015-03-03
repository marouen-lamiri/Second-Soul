using UnityEngine;
using System.Collections;

public class Pausing : MonoBehaviour {

	//Variables
	bool isPaused;
	int TimeScale;
	int pausedTime = 0;
	int regualrTime = 1;
	float buttonWidth = 200; 
	float buttonHeight = 50;
	float widthOffset = 75;
	float heightOffset = 25;
	FighterNetworkScript fighterNetworkScript;

	void Start() {
		fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
	}

	// Update is called once per frame, checks if p is pressed
	void FixedUpdate () {
		if(Input.GetKeyDown("p"))
		{
			Pause();

			// networking event listener:
			fighterNetworkScript.onPauseGame();
		}
	}

	public void Pause(){
		if (isPaused == true)
		{
			Time.timeScale = regualrTime;
			TimeScale = regualrTime;
			isPaused = false;
		}
		else
		{
			Time.timeScale = pausedTime;
			TimeScale = pausedTime;
			isPaused = true;
		}
	}

	void OnGUI() {
		if(isPaused)
		{
			//size = 40 cannot be replaced with an int or float, it has to be an exact value
			GUI.Label (new Rect (Screen.width/2 - widthOffset, Screen.height/2 - heightOffset, buttonWidth, buttonHeight),"<Color=red><size=40>-Paused-</size></Color>");
		}
	}

	public int getTimeScale() {
		return TimeScale;
	}
}
