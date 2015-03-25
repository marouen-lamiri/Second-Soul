using UnityEngine;
using System.Collections;

public class ForestPositioner : MonoBehaviour {

	private Fighter fighter;
	private Sorcerer sorcerer;
	private Vector3 initialFighterPosition = new Vector3(93,0,57);
	private Vector3 initialSorcererPosition = new Vector3(95,0,56);
	
	// Use this for initialization
	void Start () {
		fighter = (Fighter)GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer ();
		fighter.transform.position = initialFighterPosition;
		sorcerer.transform.position = initialSorcererPosition;
	}
}
