using UnityEngine;
using System.Collections;

public class DatabaseFighter : MonoBehaviour {
	
	// Use this for initialization
	int interval = 120;
	int count;
	public Character player;
	private Fighter fighter;
	
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			Debug.Log ("Save Fighter's Primary Stats!");
			savePrimaryStats();
			count = 0;
		}
		count++;
	}
	
	void savePrimaryStats(){
//		Debug.Log ("Save the stats of the " + player.name);
		PlayerPrefs.SetInt ("Strength", fighter.getStrength ());
		PlayerPrefs.SetInt ("Dexterity", fighter.getDexterity ());
		PlayerPrefs.SetInt ("Endurance", fighter.getEndurance ());
	}
	
	public void readPrimaryStats(){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
//		Debug.Log ("Read the stats of the " + player.name);
		fighter.setStrength((int) PlayerPrefs.GetInt("Strength"));
		fighter.setDexterity((int) PlayerPrefs.GetInt("Dexterity"));
		fighter.setEndurance((int) PlayerPrefs.GetInt("Endurance"));
	}
}
