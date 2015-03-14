﻿using UnityEngine;
using System.Collections;

public class SorcererInstanceManager : MonoBehaviour {

	private static Sorcerer sorcerer;
	private bool doCheckForNewSorcererNetworkInstantiatedByClient;

	void Start () {
		// this.sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)); // no, really just get the sorcerer when a client connects, end of story.
		doCheckForNewSorcererNetworkInstantiatedByClient = false;
	}
	
	void Update () {
	
		// on the server side: start checking for a second instance, network instantiated by the client:
		if(doCheckForNewSorcererNetworkInstantiatedByClient) {
			Sorcerer[] sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); // no, really just get the sorcerer when a client connects, end of story.
			if(sorcerers.Length == 2) {
				// get the one that is not the same reference as the one we already have.
				bool indexZeroIsOldSorcerer = Object.ReferenceEquals(sorcerers[0], SorcererInstanceManager.sorcerer);
				//bool OneIsNewSorcerer = Object.ReferenceEquals(sorcerers[1], SorcererInstanceManager.sorcerer);
				if(indexZeroIsOldSorcerer) {
					SorcererInstanceManager.createAndSwapNewSorcerer(sorcerers[1], sorcerers[1].transform);
					doCheckForNewSorcererNetworkInstantiatedByClient = false;
				} 
				else {
					SorcererInstanceManager.createAndSwapNewSorcerer(sorcerers[0], sorcerers[0].transform);
					doCheckForNewSorcererNetworkInstantiatedByClient = false;
				}
			}
		}
	}

	public static Sorcerer getSorcerer() {
		return SorcererInstanceManager.sorcerer;
	}

	// to use when a client reconnects and wants to take over existing sorcerer, re-create it at the exact same position and rotation
	public static void createAndSwapNewSorcerer() {

		// copy old sorcerer so the server owns it
		// publish that copy to all
		// destroy old sorcerer

		Sorcerer s = Network.Instantiate(SorcererInstanceManager.sorcerer, SorcererInstanceManager.sorcerer.transform.position, SorcererInstanceManager.sorcerer.transform.rotation, 3) as Sorcerer;
		Destroy (SorcererInstanceManager.sorcerer);
		SorcererInstanceManager.sorcerer = s;

				
				
	}

	//to use when first time connecting in the start menu scene, allows to place the sorcerer
	public static void createAndSwapNewSorcerer(Sorcerer sorcererPrefab, Transform transf) {
		
		// copy old sorcerer so the server owns it
		// publish that copy to all
		// destroy old sorcerer
		
		Sorcerer s = Network.Instantiate(sorcererPrefab, transf.position, transf.rotation, 3) as Sorcerer;
		Destroy (SorcererInstanceManager.sorcerer);
		SorcererInstanceManager.sorcerer = s;

		
	}




	// ================================================
	[RPC]
	public void onClientConnects() {
		//if(networkView.isMine){
		networkView.RPC("startDoCheckForNewSorcererNetworkInstantiatedByClient", RPCMode.All);//RPCMode.Others);
		//print ("hello");
		//}
	}
	
	[RPC]
	void startDoCheckForNewSorcererNetworkInstantiatedByClient() {
		doCheckForNewSorcererNetworkInstantiatedByClient = true;
	}

}