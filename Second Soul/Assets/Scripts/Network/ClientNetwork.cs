using UnityEngine;
using System.Collections;

public class ClientNetwork : ParentNetwork {
	
	public bool isConnectedToServer = false;
	


//	public Sorcerer sorcerer;
//	public Fighter fighter;


	
	private static Vector3 sorcererPositionAfterMapCreation;
	private bool sorcererPositionWasUpdated; // ... AfterMapGeneration;
	private bool focusCorrectPlayerWasDone;
	private int framesToWaitForFocusCorrectCharacter;

	bool displayChat;

	public void Awake() {


		sorcererPositionAfterMapCreation = Vector3.zero;
		focusCorrectPlayerWasDone = false;

		framesToWaitForFocusCorrectCharacter = 0;

	} 

	public void Start() {
	}

	public void Update() {





		//===================
		// client --> logic for creating (different types of) sorcerer:
		if (Application.loadedLevelName == "StartScreen" && !sorcererWasCreated) { //Network.isClient && 

			//  --> moved out of if(isClient), because we still want the server to be able to determine the sorcerer's type for 1 player mode.
			if(classChooser.sorcererSelectionStrings[classChooser.sorcererSelection]=="Mage"){
				//				sorcerer = (Sorcerer) Instantiate(classChooser.mage,Vector3.zero,Quaternion.identity);
				sorcererPrefab = classChooser.mage;
			}
			else if(classChooser.sorcererSelectionStrings[classChooser.sorcererSelection]=="Druid"){
				//				sorcerer = (Druid) Instantiate(classChooser.druid,Vector3.zero,Quaternion.identity);
				sorcererPrefab = classChooser.druid;
			}
			else/*(sorcererSelectionStrings[sorcererSelection]=="Priest")*/{
				//				sorcerer = (Priest) Instantiate(classChooser.priest,Vector3.zero,Quaternion.identity);
				sorcererPrefab = classChooser.priest;
			}

			// if player choose to play as Client, instantiate sorcerer:
			if(Network.isClient){
				transform.position = new Vector3(2,0,0); // put the sorcerer to the right under the sorcerer buttons
				Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
				sorcererWasCreated = true;
			}


		} 





	}
	
	[RPC]
	private void AddNetworkView() {
		// this is to dynamically add a networkview at runtime, can be useful for example if creating players or other game objects at runtime.
		
		//        gameObject.AddComponent<NetworkView>();
		//        gameObject.networkView.observed = this;
		//        gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		//        gameObject.networkView.viewID = Network.AllocateViewID();
	}
	

	void OnGUI() {




		// =========================
		
		// button to connect as a client:
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2, 150, 50), "Connect as a Client")) {
				ConnectToServer ();
				displayChat = true;

			}
		}
		
		// after connecting if you're a client:
		if(displayChat) {
			if (Network.peerType == NetworkPeerType.Client) {
				GUI.Label(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 0, 150, networkWindowButtonHeight), "client");
				
				if (GUI.Button(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 1, 150, networkWindowButtonHeight), "Logout")) {
					Network.Disconnect();
					// also destroy the player game object here, since OnPlayerDisconnected only works on the server side, which means the player will be destroyed for everyone except the one who created it.
					
				}
				
				if (GUI.Button(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 2, 150, networkWindowButtonHeight), "SendHello to server")) {
					someInfo = "hello server!";
					SendInfoToServer(someInfo);
				}
				
				GUI.TextArea(new Rect(250, 100, 300, 100), _messageLog);
				
			}

		}



		
	}
	
	// for client:
	private void ConnectToServer() {
		Network.Connect(serverIP, port);
		if (!Network.isClient) {
			//Network.Connect(serverLocalIP,port);
		}
	}
	


	// for client:
	[RPC]
	void SendInfoToServer(string msg){
		msg = "CLIENT " + _myNetworkPlayer.guid + ": " + msg;
		_messageLog += msg + "\n";
		//someInfo = "Client " + _myNetworkPlayer.guid + ": hello server";
		networkView.RPC("ReceiveInfoFromClient", RPCMode.Server, msg);
	}
	[RPC]
	void SetPlayerInfo(NetworkPlayer player) {
		_myNetworkPlayer = player;
		someInfo = "Player setted";
		networkView.RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}
	
	[RPC]
	void ReceiveInfoFromServer(string someInfo) {
		_messageLog += someInfo + "\n";
		
	}
	
	void OnConnectedToServer() {
		_messageLog += "Connected to server" + "\n";
		isConnectedToServer = true;
	}
	void OnDisconnectedToServer() {
		_messageLog += "Disconnected from server" + "\n";
		Network.Destroy (playerPrefab.gameObject);
		isConnectedToServer = false;
	}
	


	void OnLevelWasLoaded(int level) {
	}

	//============= RPC functions called from elsewhere in the code. ====================

	[RPC]
	public void SetSorcererPositionAfterMapCreation(string position) {
		string[] positions = position.Split (',');
		Vector3 positionVector3 = Vector3.zero;
		positionVector3.x = float.Parse(positions[0]);
		positionVector3.y = float.Parse(positions[1]);
		positionVector3.z = float.Parse(positions[2]);
		
		Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
		sorcerer.transform.position = positionVector3;
		sorcererPositionWasUpdated = true;
		if(sorcerer.transform.position.x > 1.0f || sorcerer.transform.position.x  < -1.0f){
			//sorcererPositionWasSent = true;
			networkView.RPC ("setServerBoolean",RPCMode.All); // RPCMode.Server --> now All so that it works in 1 player mode too: server calling iself to set that boolean.
		}
	}





	
}

