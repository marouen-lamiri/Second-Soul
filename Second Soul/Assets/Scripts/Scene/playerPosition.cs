using UnityEngine;
using System.Collections;

public class playerPosition : MonoBehaviour {

	private Fighter fighter;
	private Sorcerer sorcerer;
	private Vector3 initialFighterPosition = new Vector3(25,0,23);
	private Vector3 initialSorcererPosition = new Vector3(24,0,22);

	// Use this for initialization
	void Start () {
		fighter = (Fighter)GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer ();
		fighter.transform.position = initialFighterPosition;
		sorcerer.transform.position = initialSorcererPosition;
	}
}
