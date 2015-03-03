using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	//Variable declaration
	public bool isRespawned;
	int scaleTime;
	int pausedTime = 0;
	int resumedTime = 1;
	float buttonWidth = 300;
	float buttonHeight = 200;
	float offsetWidth = 90;
	float offsetHeight = 50;
	float buttonSmallWidth = 95;
	float buttonSmallHeight = 25;
	float offsetSmallWidth = 10;
	float offsetSmallHeight = 80;
	private Fighter player;
	public Font myFont;

	//Sets default value of variables
	void Start () {
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		isRespawned = false;
		Time.timeScale = resumedTime;
		scaleTime = resumedTime;
	}

	// Update is called once per frame, checks if the player is dead
	void FixedUpdate () {
		if(player.isDead () && isRespawned == false){
			Death();
		}
	}

	//Checks if the player is dead, if he is, stops time
	public void Death () {
		if (player.isDead ()){
			isRespawned = false;
		}else{
			isRespawned = true;
		}
	}

	//Display GameOver Screen
	void OnGUI() {
		if(player.isDead ()){
			GUI.skin.font = myFont;
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width/2 - offsetWidth, Screen.height/2 - offsetHeight, buttonWidth, buttonHeight),"<Color=red><size=15>This is the end of his story \nA story that will never be completed \nAn unfullifed Destiny \nHis destination is death \nOnly his regrets remain</size></Color>");
			if (GUI.Button (new Rect (Screen.width/2 - offsetWidth, Screen.height/2 + offsetSmallHeight, buttonSmallWidth, buttonSmallHeight), "-Respawn-")) {
				Respawn ();
				// networking respawn listener:
				FighterNetworkScript fighterNetworkingScript = (FighterNetworkScript) GameObject.FindObjectOfType (typeof(FighterNetworkScript));
				fighterNetworkingScript.onRespawn ();
			}
			if (GUI.Button (new Rect (Screen.width/2 + offsetSmallWidth, Screen.height/2 + offsetSmallHeight, buttonSmallWidth, buttonSmallHeight), "-Close-")){
				Application.Quit();
			}
		}
	}

	public void Respawn (){
		restoreHealthAndEnergy ();
		player.transform.position = new Vector3(player.getInitialPositionX(), player.getInitialPositionY(), player.getInitialPositionZ());
		player.isDead ();
		Death ();
	}

	void restoreHealthAndEnergy(){
		player.health = player.maxHealth;
		player.energy = player.maxEnergy;
	}
	
	public int getScaleTime(){
		return scaleTime;
	}
}
