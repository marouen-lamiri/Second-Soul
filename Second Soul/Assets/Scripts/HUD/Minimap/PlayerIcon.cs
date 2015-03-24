using UnityEngine;
using System.Collections;

public class PlayerIcon : MonoBehaviour, ISorcererSubscriber {

	private Fighter fighter;
	private Sorcerer sorcerer;

	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
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

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}
}
