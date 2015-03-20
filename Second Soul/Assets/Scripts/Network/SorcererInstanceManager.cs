using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererInstanceManager : MonoBehaviour {

	private static Sorcerer sorcerer;
	public static bool doCheckForNewSorcererNetworkInstantiatedByClient;
	private static List<ISorcererSubscriber> subscriberList = new List<ISorcererSubscriber> (); // needs early init because everyone calls subscribe(this) in their Awake() or Start() methods.

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

	// To be used by any class that uses the sorcerer (i.e. any class that .finds the sorcerer or uses .getSorcerer())
	// each of these classes now needs to implement an interface: SorcererSubscriber and implement a method: updateMySorcerer() that the publish method will call below:
	public static void subscribe(ISorcererSubscriber subscriber) {
		subscriberList.Add (subscriber);
	}

	public static void publishNewSorcererInstance() {
		foreach(ISorcererSubscriber subscriber in subscriberList) {
			print (subscriber);
			subscriber.updateMySorcerer(SorcererInstanceManager.sorcerer);
		}
	}

	// all methods that did: GameObject.FindObjectOfType (typeof (Sorcerer)) now call getSorcerer() instead of doing .find sorcerer 
	public static Sorcerer getSorcerer() {
		if(SorcererInstanceManager.sorcerer == null) {
			print ("no sorcerer was found in the game.");
		}
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

		// if client: tell server to look for and store Sorcerer (in Update() above).
		if(Network.isClient) {
			//onClientConnects();
		}

			
			
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




//	// ================================================
//	[RPC]
//	public void onClientConnects() {
//		//if(networkView.isMine){
//		networkView.RPC("startDoCheckForNewSorcererNetworkInstantiatedByClient", RPCMode.All);//RPCMode.Others);
//		//print ("hello");
//		//}
//	}
//	
//	[RPC]
//	void startDoCheckForNewSorcererNetworkInstantiatedByClient() {
//		doCheckForNewSorcererNetworkInstantiatedByClient = true;
//	}

}
