using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	//Variable declaration
	public bool isRespawned;
	int scaleTime;
	public Fighter player;
	public Font myFont;

	//Sets default value of variables
	void Start () {
		isRespawned = false;
		Time.timeScale = 1;
		scaleTime = 1;
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
			GUI.Label (new Rect (Screen.width/2 - 90, Screen.height/2 - 50, 300, 200),"<Color=red><size=15>This is the end of his story \nA story that will never be completed \nAn unfullifed Destiny \nHis destination is death \nOnly his regrets remain</size></Color>");
			if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height/2 + 80, 95, 25), "-Respawn-")) {
				Respawn ();
				// networking respawn listener:
				FighterNetworkScript fighterNetworkingScript = (FighterNetworkScript) GameObject.FindObjectOfType (typeof(FighterNetworkScript));
				fighterNetworkingScript.onRespawn ();
			}
			if (GUI.Button (new Rect (Screen.width/2 + 10, Screen.height/2 + 80, 95, 25), "-Close-")){
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
