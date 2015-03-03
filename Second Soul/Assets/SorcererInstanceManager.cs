using UnityEngine;
using System.Collections;

public class SorcererInstanceManager : MonoBehaviour {

	private static Sorcerer sorcerer;

	void Start () {
		// this.sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)); // no, really just get the sorcerer when a client connects, end of story.
	}
	
	void Update () {
	
	}

	public static Sorcerer getSorcerer() {
		return SorcererInstanceManager.sorcerer;
	}

	// to use when a client reconnects and wants to take over existing sorcerer, re-create it at the exact same position and rotation
	public static void createAndSwapNewSorcerer() {

		// copy old sorcerer so the server owns it
		// publish that copy to all
		// destroy old sorcerer

		SorcererInstanceManager.sorcerer = Network.Instantiate(SorcererInstanceManager.sorcerer, SorcererInstanceManager.sorcerer.transform.position, SorcererInstanceManager.sorcerer.transform.rotation, 3) as Sorcerer;


	}

	//to use when first time connecting in the start menu scene, allows to place the sorcerer
	public static void createAndSwapNewSorcerer(Sorcerer sorcererPrefab, Transform transf) {
		
		// copy old sorcerer so the server owns it
		// publish that copy to all
		// destroy old sorcerer
		
		sorcerer = Network.Instantiate(sorcererPrefab, transf.position, transf.rotation, 3) as Sorcerer;
		
		
	}

}
