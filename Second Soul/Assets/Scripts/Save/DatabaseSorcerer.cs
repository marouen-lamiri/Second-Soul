using UnityEngine;
using System.Collections;

public class DatabaseSorcerer : MonoBehaviour, ISorcererSubscriber {
	
	// Use this for initialization
	int interval = 3000;
	int count;
	int reset = 0;
	public Character player;
	private Sorcerer sorcerer;
	private Fighter fighter;
	
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)); //sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // 
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			//Debug.Log ("Save Sorcerer's Primary Stats!");
			savePrimaryStats();
			UnityNotificationBar.UNotify("Saved Sorcerer Stats"); //although this might appear false in Mono-Develop, it actually works as an external asset
			count = reset;
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
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		sorcerer.setIntelligence((int) PlayerPrefs.GetInt("Intelligence"));
		sorcerer.setWisdom((int) PlayerPrefs.GetInt("Wisdom"));
		sorcerer.setSpirit((int) PlayerPrefs.GetInt("Spirit"));
		sorcerer.calculateNewPrimaryStats();
		sorcerer.calculateSecondaryStats();
		sorcerer.calculateLevel();
		sorcerer.calculateNextLevelXP();
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

}
