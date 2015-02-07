using UnityEngine;
using System.Collections;
public class PlayerHealthBar : MonoBehaviour {
	//Variables
	float globeHeight = 128;
	public Texture globePic;
	int globeSize = 128;
	double hp;
	double maxhp;
	double healthPercent;
	float ratioWidth;
	float ratioHeight;
	private Fighter player;
	//Initialization of variables
	void Start () {
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		hp = player.health;
		maxhp = player.maxHealth;
		healthPercent = 1;
	}
	// Update is called once per frame
	void Update () {
		hp = player.health;
		ratioWidth = (Screen.width/1366f);
		if(ratioWidth < 0.5f ){
			ratioWidth = 0.5f;
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
		GUI.BeginGroup(new Rect(Screen.width * 0.26f * ratioWidth, Screen.height-(globeHeight+2f), globeSize, globeSize));
		GUI.DrawTexture(new Rect(0, (-globeSize+globeHeight), globeSize, globeSize),globePic);
		GUI.EndGroup();
	}
}
