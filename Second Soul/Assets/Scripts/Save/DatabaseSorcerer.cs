using UnityEngine;
using System.Collections;

public class DatabaseSorcerer : MonoBehaviour {
	
	// Use this for initialization
	int interval = 120;
	int count;
	public Character player;
	private Sorcerer sorcerer;
	
	void Start () {
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			Debug.Log ("Save Sorcerer's Primary Stats!");
			savePrimaryStats();
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
//		Debug.Log ("Read the stats of the " + player.name);
		sorcerer.setIntelligence((int) PlayerPrefs.GetInt("Intelligence"));
		sorcerer.setWisdom((int) PlayerPrefs.GetInt("Wisdom"));
		sorcerer.setSpirit((int) PlayerPrefs.GetInt("Spirit"));
	}
}
