using UnityEngine;
using System.Collections;

public class playerPosition : MonoBehaviour {

	private Fighter fighter;
	private Sorcerer sorcerer;
	bool once = false;
	private Vector3 initialFighterPosition = new Vector3(25,0,23);
	private Vector3 initialSorcererPosition = new Vector3(25,0,24);

	// Use this for initialization
	void Start () {
		Debug.Log ("Am i second?");
		fighter = (Fighter)GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer ();
		fighter.transform.position = initialFighterPosition;
		sorcerer.transform.position = initialSorcererPosition;
		checkSorcererPosition();
	}

	void Update(){
		if(!once && checkSorcererPosition()){
			once = false;
		}
	}

	bool checkSorcererPosition(){
		if(Physics.Linecast(fighter.transform.position, sorcerer.transform.position)){
			Debug.Log ("Linecast done");
			sorcerer.transform.position = initialSorcererPosition;
			return true;
		}
		return false;
	}
}
