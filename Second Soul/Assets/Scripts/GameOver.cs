using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	//Variable declaration
	bool isDead = false;
	bool isRespawned = false;
	public Character player;
	public Font myFont;

	void Start () {
		Time.timeScale = 1;
	}

	// Update is called once per frame, checks if the player is dead
	void Update () {
		if(player.isDead () && isRespawned == false)
		{
			isRespawned = true;
			Death();
		}
	}

	//Checks if the player is dead, if he is, stops time
	public void Death () {
		if (isDead == true)
		{
			player.transform.position = new Vector3(player.getInitialPositionX(), player.getInitialPositionY(), player.getInitialPositionZ());
			Time.timeScale = 1;
			isDead = false;
		}
		else
		{
			Time.timeScale = 0;
			isDead = true;
		}
	}

	//Display GameOver Screen
	void OnGUI() {
		if(isDead){
			GUI.skin.font = myFont;
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width/2 - 90, Screen.height/2 - 50, 300, 200),"<Color=red><size=15>This is the end of his story \nA story that will never be completed \nAn unfullifed Destiny \nHis destination is death \nOnly his regrets remain</size></Color>");
			if (GUI.Button (new Rect (Screen.width/2 - 90, Screen.height/2 + 80, 95, 25), "-Respawn-")) {
				player.health = player.maxHealth;
				player.energy = player.maxEnergy;
				isRespawned = false;
				Death ();
			}
			if (GUI.Button (new Rect (Screen.width/2 + 10, Screen.height/2 + 80, 95, 25), "-Close-")){
				Application.Quit();
			}
		}
	}

	public bool getIsRespawned(){
		return isRespawned;
	}



}
