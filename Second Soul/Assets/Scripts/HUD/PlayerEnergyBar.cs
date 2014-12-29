using UnityEngine;
using System.Collections;

public class PlayerEnergyBar : MonoBehaviour {

	//Variable
	float globeHeight = 64;
	public Texture globePic;
	int globeSize = 64;
	double energy;
	double maxEnergy;
	double energyPercent;
	float ratioWidth;
	float ratioHeight;
	private Fighter player;
	private bool isStillADummyFighter;

	//Initialization of variables
	void Start (){
		player = new Fighter ();
		player.health = 3.0;
		isStillADummyFighter = true;
	}
	
	// Update is called once per frame
	void Update (){

		if(isStillADummyFighter) {
			Fighter playerTemp = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			if(player != null) {
				player = playerTemp;
				isStillADummyFighter = false;

				energyPercent = 1;
				energy = player.energy;
				maxEnergy = player.maxEnergy;
				ratioWidth = Screen.width / 800;  
				ratioHeight = Screen.height/ 600;
				globeHeight = (float)(globeHeight + (Screen.height * ratioHeight * 0.03));
				globeSize = (int)(globeSize + (Screen.width * ratioWidth * 0.03));
			}
		} else {
			energy = player.energy;
		}

	}

	//Draw Energy Bar
	void OnGUI () {
		//Determining the health (through Percentage)
		energyPercent = energy/maxEnergy;
		if(energyPercent < 0)
		{
			energyPercent = 0;
		}
		if(energyPercent > 1)
		{
			energyPercent = 1;
		}

		//Draw the appropriate amount of health bar
		globeHeight = (float) energyPercent*globeSize;
		
		//Drawing Energy Bar
		GUI.BeginGroup(new Rect(Screen.width * 0.9f , (Screen.height-(globeHeight+20)), globeSize, globeSize));
		GUI.DrawTexture(new Rect(0, -globeSize+globeHeight, globeSize, globeSize),globePic);
		GUI.EndGroup();
	}

}
