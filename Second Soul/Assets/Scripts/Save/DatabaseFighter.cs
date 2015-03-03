using UnityEngine;
using System.Collections;

public class DatabaseFighter : MonoBehaviour {
	
	// Use this for initialization
	int interval = 3000;
	int count;
	int reset = 0;
	public Character player;
	private Fighter fighter;
	
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			savePrimaryStats();
			UnityNotificationBar.UNotify("Saved Fighter Stats"); //although this might appear false in Mono-Develop, it actually works as an external asset
			count = reset;
		}
		count++;
	}
	
	void savePrimaryStats(){
		PlayerPrefs.SetInt ("Strength", fighter.getStrength ());
		PlayerPrefs.SetInt ("Dexterity", fighter.getDexterity ());
		PlayerPrefs.SetInt ("Endurance", fighter.getEndurance ());
		PlayerPrefs.SetInt ("Level", fighter.getLevel());
	}
	
	public void readPrimaryStats(){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		fighter.setStrength((int) PlayerPrefs.GetInt("Strength"));
		fighter.setDexterity((int) PlayerPrefs.GetInt("Dexterity"));
		fighter.setEndurance((int) PlayerPrefs.GetInt("Endurance"));
		fighter.setLevel((int)PlayerPrefs.GetInt("Level"));
		fighter.calculateNewPrimaryStats();
		fighter.calculateSecondaryStats();
		//fighter.calculateLevel();
		fighter.calculateNextLevelXP();
	}
}
