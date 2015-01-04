using UnityEngine;
using System.Collections;

public class Database : MonoBehaviour {

	// Use this for initialization
	int interval = 120;
	int count;
	public Character player;

	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			Debug.Log ("Save!");
			savePrimaryStats();
			count = 0;
		}
		count++;
	}

	void savePrimaryStats(){
		PlayerPrefs.SetFloat("Health: ", (float) player.health);
		PlayerPrefs.SetFloat("Energy ", (float) player.energy);
	}

	public void readPrimaryStats(){
		player.health = (double) PlayerPrefs.GetFloat("Health: ");
		player.energy = (double) PlayerPrefs.GetFloat("Energy ");
	}
}
