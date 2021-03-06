﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Fighter player;
	public Enemy enemy;
	
	public Texture2D healthFrame;
	public Rect healthFrameRect;
	float healthFrameWidth;
	float healthFrameHeight;
	float height = 10f;
	
	public Texture2D healthFill;
	public Rect healthFillRect;
	float healthFillWidth;
	float healthFillHeight;
	
	float healthPercent;
	
	//these "fixes" are to make the dimensions work for any size display
	float healthFrameWidthFix = 0.46875f;
	float healthFrameHeightFix = 0.0586f;
	float healthFillWidthFix = 0.375f;
	float healthFillHeightFix=0.0293f;

	// Use this for initialization
	void Start () {
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		healthFrameWidth = (float)Screen.width*healthFrameWidthFix;
		healthFrameHeight = (float)Screen.height*healthFrameHeightFix;
		healthFillWidth = (float)Screen.width*healthFillWidthFix;
		healthFillHeight =(float)Screen.height*healthFillHeightFix;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(player.target!=null){
			enemy = (Enemy)player.target;
			healthPercent =((float)enemy.health / (float)enemy.maxHealth);
		}
		else{
			enemy = null;
			healthPercent = 0;
		}
	}
	void OnGUI(){
		//if statement to determine whether the enemy is this enemy
		if (enemy != null) {
			drawHealthBar ();
			drawHealthFrame ();
		}
	}
	void drawHealthFrame(){
		healthFrameRect.Set (Screen.width/2-healthFrameWidth/2, height, healthFrameWidth, healthFrameHeight);
		GUI.DrawTexture (healthFrameRect, healthFrame);
	}
	void drawHealthBar(){
		healthFillRect.Set (Screen.width/2-healthFillWidth/2, height+healthFrameHeight/4, healthFillWidth*healthPercent, healthFillHeight);
		GUI.DrawTexture (healthFillRect, healthFill);
	}
}
