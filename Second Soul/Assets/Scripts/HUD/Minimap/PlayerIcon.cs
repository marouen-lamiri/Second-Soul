using UnityEngine;
using System.Collections;

public class PlayerIcon : MonoBehaviour {

	private Fighter fighter;
	private Sorcerer sorcerer;
	// Use this for initialization
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fighter.playerEnabled) {
			transform.rotation = fighter.transform.rotation;
		}
		else {
			transform.rotation = sorcerer.transform.rotation;
		}
	}
}
