using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererInstanceManager : MonoBehaviour {

	public static Sorcerer sorcerer; // TODO change back to private
	public static bool doCheckForNewSorcererNetworkInstantiatedByClient;
	private static List<ISorcererSubscriber> subscriberList = new List<ISorcererSubscriber> (); // needs early init because everyone calls subscribe(this) in their Awake() or Start() methods.
	private static NetworkPlayer sorcererAIGameObjectToDestroy;
	private static bool doLateSorcererDetection;
	private static int numberOfFramesForLateSorcDetect = 100;
	private static int frameCounterForLateSorcDetect = 0;

	/**
	 * 
	 * To be used by any class that uses the sorcerer (i.e. any class that .finds the sorcerer or uses .getSorcerer())
	 * each of these classes now needs to implement an interface: SorcererSubscriber and implement a method: updateMySorcerer() that the publish method will call below:
	 * 
	 */
	public static void subscribe(ISorcererSubscriber subscriber) {
		subscriberList.Add (subscriber);
	}
	
	private static void publishNewSorcererInstance() {
		foreach(ISorcererSubscriber subscriber in subscriberList) {
			print (subscriber);
			subscriber.updateMySorcerer(SorcererInstanceManager.sorcerer);
		}
	}


	// ===================================
	void Awake() {
		SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient();
		//sorcererAIGameObjectToDestroy = -1;
		doLateSorcererDetection = false;
		frameCounterForLateSorcDetect = 0;
	}

	void Start () {
		// this.sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)); // no, really just get the sorcerer when a client connects, end of story.
		// well actually this is problematic, because in a new scene, this class doesn't carry over, so it needs to find the sorcerer right away:
		//SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient(); 

		//doCheckForNewSorcererNetworkInstantiatedByClient = false;
	}
	
	void Update () {

		// debug:
		//		if(SorcererInstanceManager.sorcerer == null) {
		//			print (" DEBUG NO sorcerer was found in the game.");
		//		} else {
		//			print (" DEBUG sorcerer WAS found in the game.");
		//		}

		//Sorcerer[] sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); 
		//print ("sorcerers.Length -> "+sorcerers.Length);

		// late sorcerer detection --> it takes a while after OnClientConnect before the network.instantiated sorcerer shows up (and that .length == 2)
		if(doLateSorcererDetection) {
			//Debug.Log ("IN LATE DETECT SORCERER NOW.");
			if(frameCounterForLateSorcDetect > numberOfFramesForLateSorcDetect) {
				checkForNewSorcererNetworkInstantiatedByClient();
				doLateSorcererDetection = false;
				frameCounterForLateSorcDetect = 0;
			} 
			else {
				frameCounterForLateSorcDetect++;
			}
		}
		
		// on the server side: start checking for a second instance, network instantiated by the client:
		//if(doCheckForNewSorcererNetworkInstantiatedByClient) {
		//SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient ();
		//}

	}

	// ======================
	/**
	 * 
	 * 
	 * 
	 */
	public static void checkForNewSorcererNetworkInstantiatedByClient() {

		Sorcerer[] sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); // no, really just get the sorcerer when a client connects, end of story. // TODO optimize with FindGameObjectsWithTag instead.
		//sorcerers = GameObject.FindGameObjectsWithTag ("Sorcerer");
		if(sorcerers.Length == 2) {
			Debug.Log ("2 SORCERERS FOUND -- THE OLDER ONE SHOULD BE DELETED SOON.");
			// get the one that is not the same reference as the one we already have.
			bool indexZeroIsOldSorcerer = Object.ReferenceEquals(sorcerers[0], SorcererInstanceManager.sorcerer);
			//bool OneIsNewSorcerer = Object.ReferenceEquals(sorcerers[1], SorcererInstanceManager.sorcerer);
			if(indexZeroIsOldSorcerer) {
				//SorcererInstanceManager.createAndSwapNewSorcerer(sorcerers[1], sorcerers[1].transform);
				SorcererInstanceManager.swapSorcerers(sorcerers[1]);
			} 
			else {
				//SorcererInstanceManager.createAndSwapNewSorcerer(sorcerers[0], sorcerers[0].transform);
				SorcererInstanceManager.swapSorcerers(sorcerers[0]);
			}
		}
		else if(sorcerers.Length == 1) {
			//SorcererInstanceManager.createAndSwapNewSorcerer(sorcerers[0], sorcerers[0].transform);
			SorcererInstanceManager.sorcerer = sorcerers[0];
		}
		else if(sorcerers.Length >= 3) {
			print ("3 or more sorcerer found.");
		}
		else {
			print (" 0 sorcerer found");
		}

	}

	/**
	 * 
	 * all methods that did: GameObject.FindObjectOfType (typeof (Sorcerer)) 
	 * 	... now call getSorcerer() instead of doing .find sorcerer 
	 * 
	 *  (exception to this: classes that .find the sorcerer in the start menu scene, like DatabaseSorcerer.cs 
	 *  ... but they still need to implement ISorcererSubscriber.cs)
	 * 
	 */ 
	public static Sorcerer getSorcerer() {
		//		if(SorcererInstanceManager.sorcerer == null) {
		//			print ("no sorcerer was found in the game.");
		//			string name = Application.loadedLevelName;
		//			print (name);
		//		}
		return SorcererInstanceManager.sorcerer;
	}


	/**
	 * 
	 * --> allows to place the sorcerer on map with params.
	 * 
	 * use null params when a client reconnects and wants to take over existing sorcerer, (will re-create it at the same position and rotation)
	 * use also when connecting for first time in the start menu scene, allows to place the sorcerer with params
	 * 
	*/
	public static void createAndSwapNewSorcerer(Sorcerer sorcererPrefab, Transform transf) {

		// important, to get the old sorcerer's (AI) position, so we can create the new one at the right position:
		// but if we do things like that (instead of getting the old sorcerer's position with an rpc call)
		// then we need to NOT removeRPCs for the old sorcerer on the server side, so we can detect it from the client.
		checkForNewSorcererNetworkInstantiatedByClient ();
		
		// copy old sorcerer so the server owns it
		// publish that copy to all
		// destroy old sorcerer

		// debug:
		Sorcerer[] sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); 
		print (sorcerers.Length);
	
		Sorcerer s;

		if(sorcererPrefab != null && transf != null) {
			s = Network.Instantiate(sorcererPrefab, transf.position, transf.rotation, 0) as Sorcerer;
		}
		else if (SorcererInstanceManager.sorcerer != null) {
			// place new sorcerer where old sorcerer is before swaping/destroying it:
			// (this else if actually never runs, we need to set its position in swapSorcerers() on the server side, 
			// because we usually don't have the old sorcerer on the client side when reconnecting)
			s = Network.Instantiate(sorcererPrefab, SorcererInstanceManager.sorcerer.transform.position, SorcererInstanceManager.sorcerer.transform.rotation, 0) as Sorcerer;
		}
		else {
			// else use some defaults: new Vector3(2,0,0) at map origin:
			s = Network.Instantiate(sorcererPrefab, new Vector3(2,0,0), Quaternion.identity, 0) as Sorcerer;
		}

		// debug:
		sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); 
		print (sorcerers.Length);

		swapSorcerers (s);
		
	}

	// helper: replace old with new sorcerer:
	private static void swapSorcerers(Sorcerer newSorcerer) {

		if(SorcererInstanceManager.sorcerer != null) {

			// before destroying, try placing the new one at the old position and rotation:
			// (this isn't sufficient because it will only position the sorcerer correctly on the server instance
			// which is the slave game instance for the sorcerer, will be overriden as soon as player moves sorcerer on client)
			newSorcerer.transform.position = SorcererInstanceManager.sorcerer.transform.position;
			newSorcerer.transform.rotation = SorcererInstanceManager.sorcerer.transform.rotation;
			// So --> also send an RPC to send the position on the client, which is the master game instance for the sorcerer:
			ClientNetwork clientNetScript = (ClientNetwork)GameObject.FindObjectOfType(typeof(ClientNetwork));
			clientNetScript.changeSorcererPositionOnClient(SorcererInstanceManager.sorcerer.transform);

			Network.Destroy (SorcererInstanceManager.sorcerer.networkView.viewID);

		}
		SorcererInstanceManager.sorcerer = newSorcerer;
		
		
		publishNewSorcererInstance ();
		
		
		// debug:
		Sorcerer[] sorcerers = GameObject.FindObjectsOfType<Sorcerer>(); 
		print (sorcerers.Length);

	}


	public static void DestroySorcerer() {

		if(Network.isClient) {
			//destroy the sorcerer on the client, so we can later reconnect and recreate a new one if we wish.
			Network.RemoveRPCs(SorcererInstanceManager.sorcerer.networkView.viewID); // essential, otherwise a new client would get all old sorcerer created, see: http://answers.unity3d.com/questions/293129/networkdestroy-doesnt-remove-objects-on-other-syst.html 
			Network.Destroy (SorcererInstanceManager.sorcerer.networkView.viewID);// don't network destroy it, so the server still keeps it, (as an AI controlled sorcerer now).
			//SorcererInstanceManager.sorcerer = null;
			print (SorcererInstanceManager.sorcerer == null);

		}
//		Network.RemoveRPCs(player, 0);
//		Network.DestroyPlayerObjects(player);



		
	}


	// -----------------
	// for server:
	void OnPlayerConnected(NetworkPlayer player) {

		// 
		SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient(); 
		Sorcerer sor = (Sorcerer)GameObject.FindObjectOfType (typeof(Sorcerer));
		if(sor == null ) {
			print("sorc is null");
		}

		//if(sorcererAIGameObjectToDestroy != null) {
			//Network.Destroy(sorcererAIGameObjectToDestroy);
			//Network.DestroyPlayerObjects(sorcererAIGameObjectToDestroy);
		//}

		doLateSorcererDetection = true;

	}
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("Host Destroyed disconnected Player"+player.ipAddress);

		//sorcererAIGameObjectToDestroy = player;
		
		Network.RemoveRPCs(player, 0); // NB beware confusion here, player here is a client player -- i.e. a sorcerer!
		Network.RemoveRPCs (SorcererInstanceManager.getSorcerer().networkView.viewID); // avoid that the client when reconnecting gets the server's sorcerer (now an AI), in addition to his.

		//PlayerCamera.CameraTarget=transform;
	}

	// -------- for client:
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		

		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else if (info == NetworkDisconnection.LostConnection)
			Debug.Log("Lost connection to the server");
		else {
			Debug.Log("Successfully diconnected from the server");
			//Network.Destroy (playerPrefab.gameObject);
			SorcererInstanceManager.DestroySorcerer();

			// FIX: fighter duplicated on the client side after reconnecting:
			Fighter fighter = (Fighter)GameObject.FindObjectOfType (typeof(Fighter));
			Destroy (fighter.gameObject); // TODO optimize for speed by passing a pointer to the fighter from the network script (when it creates the fighter).

			// bring back start menu scene when disconnected:
			NetworkLevelLoader.Instance.LoadLevel(ClientNetwork.StartScreenOfficialSceneName, 1);
			

		}
		
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
