using UnityEngine;
using System.Collections;

public class PlayerEnergyBar : MonoBehaviour {

	//Variable
	public float globeHeight;
	public Texture globePic;
	public int globeSize;
	double energy;
	double maxEnergy;
	double energyPercent;
	float ratioWidth;
	float ratioHeight;
	public Fighter player;

	//Initialization of variables
	void Start (){
		energyPercent = 1;
		energy = player.energy;
		maxEnergy = player.maxEnergy;
		ratioWidth = Screen.width / 800;  
		ratioHeight = Screen.height/ 600;
		globeHeight = (float)(globeHeight + (Screen.height * ratioHeight * 0.03));
		globeSize = (int)(globeSize + (Screen.width * ratioWidth * 0.03));
	}
	
	// Update is called once per frame
	void Update (){
		energy = player.energy;
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
		GUI.BeginGroup(new Rect(Screen.width * 0.02f, Screen.height-(globeHeight+20)*2f, globeSize, globeSize));
		GUI.DrawTexture(new Rect(0, -globeSize+globeHeight, globeSize, globeSize),globePic);
		GUI.EndGroup();
	}

}
