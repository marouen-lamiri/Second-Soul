using UnityEngine;
using System.Collections;
public class PlayerEnergyBar : MonoBehaviour {
	//Variable
	float globeHeight = 164f;
	public Texture globePic;
	public Texture2D background;
	float globeSize = 164f;
	float initialGlobeSize;
	float initialGlobeHeight;
	double energy;
	double maxEnergy;
	double energyPercent;
	float ratioWidth;
	float ratioHeight;
	float offset = 8f;
	float multiplierOffset = 0.636f;
	float screenDefaultWidth = 1366f;
	float minValue = 0.5f;
	private Fighter player;
	//Initialization of variables
	void Start (){
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		energyPercent = 1;
		energy = player.energy;
		maxEnergy = player.maxEnergy;
		initialGlobeSize = globeSize;
		initialGlobeHeight = globeHeight;
	}
	// Update is called once per frame
	void Update (){
		energy = player.energy;
		ratioWidth = (Screen.width/screenDefaultWidth);
		if(ratioWidth < minValue ){
			ratioWidth = minValue;
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
		//GUI.Box(new Rect(Screen.width * (0.6435f), (Screen.height-(globeHeight+2f)), initialGlobeSize, initialGlobeHeight), background);
		GUI.BeginGroup(new Rect(Screen.width * multiplierOffset, (Screen.height-(globeHeight-offset)), globeSize, globeSize));
		GUI.DrawTexture(new Rect(0, (-globeSize+globeHeight), globeSize, globeSize),globePic);
		GUI.EndGroup();

	}
}