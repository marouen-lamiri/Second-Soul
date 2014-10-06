using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public PlayerCombat player;
	public Mob enemy;
	public Texture2D healthFrame;
	public Rect healthFrameRect;
	float healthFrameWidth;
	float healthFrameHeight;
	public Texture2D healthFill;
	public Rect healthFillRect;
	float healthFillWidth;
	float healthFillHeight;
	float healthPercent;
	// Use this for initialization
	void Start () {
		healthFrameWidth = (float)Screen.width*0.46875f;
		healthFrameHeight = (float)Screen.height*0.0586f;
		healthFillWidth = (float)Screen.width*0.375f;
		healthFillHeight =(float)Screen.height*0.0293f;
	}
	
	// Update is called once per frame
	void Update () {
		healthPercent =((float)enemy.health / (float)enemy.maxHealth);
	}
	void OnGUI(){
		//if statement to determine whether the enemy is this enemy
		if (player.enemy == enemy) {
			drawHealthBar ();
			drawHealthFrame ();
		}
	}
	void drawHealthFrame(){
		healthFrameRect.Set (Screen.width/2-healthFrameWidth/2, 10, healthFrameWidth, healthFrameHeight);
		GUI.DrawTexture (healthFrameRect, healthFrame);
	}
	void drawHealthBar(){
		healthFillRect.Set (Screen.width/2-healthFillWidth/2, 10+healthFrameHeight/4, healthFillWidth*healthPercent, healthFillHeight);
		GUI.DrawTexture (healthFillRect, healthFill);
	}
}
