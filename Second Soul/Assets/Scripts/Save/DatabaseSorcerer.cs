using UnityEngine;
using System.Collections;

public class DatabaseSorcerer : MonoBehaviour {
	
	// Use this for initialization
	int interval = 300;
	int count;
	public Character player;
	private Sorcerer sorcerer;
	private Fighter fighter;
	
	void Start () {
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			//Debug.Log ("Save Sorcerer's Primary Stats!");
			savePrimaryStats();
			UnityNotificationBar.UNotify("Saved Sorcerer Stats"); //although this might appear false in Mono-Develop, it actually works as an external asset
			count = 0;
		}
		count++;
	}
	
	void savePrimaryStats(){
		//		Debug.Log ("Save the stats of the " + player.name);
		PlayerPrefs.SetInt ("Intelligence", sorcerer.getIntelligence ());
		PlayerPrefs.SetInt ("Wisdom", sorcerer.getWisdom ());
		PlayerPrefs.SetInt ("Spirit", sorcerer.getSpirit ());
	}
	
	public void readPrimaryStats(){
		if(fighter == null) {
			fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		}
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		sorcerer.setIntelligence((int) PlayerPrefs.GetInt("Intelligence"));
		sorcerer.setWisdom((int) PlayerPrefs.GetInt("Wisdom"));
		sorcerer.setSpirit((int) PlayerPrefs.GetInt("Spirit"));
		sorcerer.calculateNewPrimaryStats();
		sorcerer.calculateSecondaryStats();
		sorcerer.calculateLevel();
		sorcerer.calculateNextLevelXP();
	}
}
