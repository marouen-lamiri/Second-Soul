using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {

	//variables
	public float globeHeight;
	public Texture globepic;
	public int globesize;
	public double hp;
	public double maxhp;
	public double healthpercent;
	public PlayerCombat player;

	// Use this for initialization
	void Start () {
		healthpercent = 1;
		hp = player.health;
		maxhp = hp;
	}

	// Update is called once per frame
	void Update () {
		hp = player.health;
	}

	void OnGUI(){
		healthpercent = hp/maxhp;
		if(healthpercent<0)
		{
			healthpercent = 0;
		}
		if(healthpercent> 100)
		{
			healthpercent = 100;
		}
		globeHeight= (float) healthpercent*globesize;
		//Drawing health Bar
		GUI.BeginGroup(new Rect(20, Screen.height-(globeHeight+20), globesize,globesize));
		GUI.DrawTexture(new Rect(0, -globesize+globeHeight, globesize, globesize),globepic);
		GUI.EndGroup();
		}

	void OnTriggerEnter () {
		if (hp > 0) return;
		//Load the level
		Application.LoadLevel ("GameOver");
		
	}

}

