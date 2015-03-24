using UnityEngine;
using System.Collections;
public class PlayerHealthBar : MonoBehaviour {
	//Variables
	float globeHeight = 164;
	public Texture globePic;
	public Texture2D background;
	int globeSize = 164;
	float initialGlobeSize;
	float initialGlobeHeight;
	double hp;
	double maxhp;
	double healthPercent;
	float ratioWidth;
	float ratioHeight;
	float offset = 11f;
	float multiplierOffset = 0.244f;
	float defaultWidth = 1366f;
	float minValue = 0.5f;
	private Fighter player;
	//Initialization of variables
	void Start () {
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		hp = player.health;
		maxhp = player.maxHealth;
		healthPercent = 1;
		initialGlobeSize = globeSize;
		initialGlobeHeight = globeHeight;
	}
	// Update is called once per frame
	void Update () {
		hp = player.health;
		ratioWidth = (Screen.width/defaultWidth);
		if(ratioWidth < minValue ){
			ratioWidth = minValue;
		}
	}
	//Display health bar in GUI
	void OnGUI(){
		if(!Application.isLoadingLevel){
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
			//GUI.Box(new Rect(Screen.width * (0.60f), (Screen.height-(globeHeight+2f)), initialGlobeSize, initialGlobeHeight), background);
			GUI.BeginGroup(new Rect(Screen.width * multiplierOffset * ratioWidth, Screen.height-(globeHeight-offset), globeSize, globeSize));
			GUI.DrawTexture(new Rect(0, (-globeSize+globeHeight), globeSize, globeSize),globePic);
			GUI.EndGroup();
		}
	}
}
