using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	//Variables
	float globeHeight = 64;
	public Texture globePic;
	int globeSize = 64;
	double hp;
	double maxhp;
	double healthPercent;
	float ratioWidth;
	float ratioHeight;
	private Fighter player;
	private bool isStillADummyFighter;

	//Initialization of variables
	void Start () {
		player = new Fighter ();
		player.health = 3.0;
		isStillADummyFighter = true;
	}

	// Update is called once per frame
	void Update () {

		if(isStillADummyFighter) {
			Fighter playerTemp = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			if(playerTemp != null) {
				player = playerTemp;

				hp = player.health;
				maxhp = player.maxHealth;
				healthPercent = 1;
				ratioWidth = Screen.width / 800;  
				ratioHeight = Screen.height/ 600;
				globeHeight = (float)(globeHeight + (Screen.height * ratioHeight * 0.03));
				globeSize = (int)(globeSize + (Screen.width * ratioWidth * 0.03));
			}
		} else {
			hp = player.health;
		}



	}

	//Display health bar in GUI
	void OnGUI(){
		//determine the remain percentange of health Bar
		healthPercent = hp/maxhp;
		if(healthPercent<0)
		{
			healthPercent = 0;
		}
		if(healthPercent> 1)
		{
			healthPercent = 1;
		}
		//Draw the appropriate amount of health bar
		globeHeight= (float) healthPercent*globeSize;
		//Drawing health Bar
		GUI.BeginGroup(new Rect(Screen.width * 0.02f, Screen.height-(globeHeight+20), globeSize, globeSize));
		GUI.DrawTexture(new Rect(0, (-globeSize+globeHeight), globeSize, globeSize),globePic);
		GUI.EndGroup();

	}
}

