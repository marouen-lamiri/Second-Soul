using UnityEngine;
using System.Collections;

public class TownPosition : MonoBehaviour {

	private Fighter fighter;
	private Sorcerer sorcerer;
	private bool trial = false;
	private Vector3 initialFighterPosition = new Vector3(75,0,75);
	private Vector3 initialSorcererPosition = new Vector3(74,0,74);
	
	// Use this for initialization
	void Update () {
		if(!trial){
			fighter = (Fighter)GameObject.FindObjectOfType (typeof (Fighter));
			sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer ();
			fighter.transform.position = initialFighterPosition;
			sorcerer.transform.position = initialSorcererPosition;
			trial = true;
		}
	}
}
