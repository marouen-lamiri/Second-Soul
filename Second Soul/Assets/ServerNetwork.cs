using UnityEngine;
using System.Collections;

public class ServerNetwork : ParentNetwork {

	int framesToWait;
	private bool playerWasCreated;
	private bool sorcererPositionWasSent; // after map generation, but used before sending it.
	public GameObject linesPrefab;

	public string gameSceneToLoad;
	
	public void Awake() {

		// server:
		framesToWait = 0;
		
		playerWasCreated = false;
		sorcererPositionWasSent = false;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// server --> generate the map only after the players have been created (because they are needed for some reason in the map generation code:
		bool serverAndClientAreBothConnected = Network.connections.Length != 0; // 0 length means no connection, i.e. no client connected to server.
		if(serverAndClientAreBothConnected && Application.loadedLevelName == "StartScreen" && bothPlayerAndSorcererWereFound) {	// && Network.isServer
			
			if(Network.isServer) {
				framesToWait++;
				if(framesToWait > 700) {
					// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
					NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
				}
				
			} else {
				NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
				
			}
		}


		// ------------------
		// server --> logic for creating (different types of) fighter:
		if(Application.loadedLevelName == "StartScreen" && !playerWasCreated) { //Network.isServer && 

			// --> moved out of if(isServer), since we want the server to be able to set the type of fighter even before connecting as a server.
			if(classChooser.fighterSelectionStrings[classChooser.fighterSelection]=="Berserker"){
				//				fighter = (Berserker) Instantiate(classChooser.berserker,Vector3.zero,Quaternion.identity);
				playerPrefab = classChooser.berserker;
			}
			else if(classChooser.fighterSelectionStrings[classChooser.fighterSelection]=="Knight"){
				//				fighter = (Knight) Instantiate(classChooser.knight,Vector3.zero,Quaternion.identity);
				playerPrefab = classChooser.knight;
			}
			else/*(fighterSelectionStrings[fighterSelection]=="Monk")*/{
				//				fighter = (Monk) Instantiate(classChooser.monk,Vector3.zero,Quaternion.identity);
				playerPrefab = classChooser.monk;
			}
			
			// if player choose to play as Server, instantiate fighter:
			if(Network.isServer) {
				transform.position = new Vector3(-2,0,0); // put the fighter to the left under the fighter buttons
				Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; //as Fighter; // N.B. place the network game object exactly where you want to spawn players.
				playerWasCreated = true;
				
				lines = Network.Instantiate (linesPrefab,transform.position,transform.rotation,7)as GameObject;
			}
		}

		//=========================
		// server --> send 
		if (Network.isServer && MapGeneration.mapGenerationCompleted && !sorcererPositionWasSent) { // 
			OnSorcererPositionDeterminedAfterMapCreation(MapGeneration.playerStartPositionVector3);
			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
			//sorcererPositionWasSent = true;
		}


	
	}

	void OnGUI() {
		
		// button to connect as server:
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2, 150, 50), "Connect as a server")) {
				// connect:
				Network.InitializeServer (10, port, false);
				displayChat = true;
			}
		}

		// after connecting: if you're a server:
		if(displayChat) {
			if (Network.peerType == NetworkPeerType.Server) {
				GUI.Label(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 0, networkWindowButtonWidth, networkWindowButtonHeight), "Server");
				GUI.Label(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 1, networkWindowButtonWidth, networkWindowButtonHeight), "Clients attached: " + Network.connections.Length);
				
				if (GUI.Button(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 2, networkWindowButtonWidth, networkWindowButtonHeight), "Quit server")) {
					Network.Disconnect(); 
					Application.Quit();
				}
				if (GUI.Button(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 3, networkWindowButtonWidth, networkWindowButtonHeight), "Send hi to client"))
					SendInfoToClient("Hello client!");
				
				if (GUI.Button(new Rect(networkWindowX, networkWindowY + networkWindowButtonHeight * 4, networkWindowButtonWidth, networkWindowButtonHeight), "Remove Client")) {
					//find the sorcerer:
					Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType(typeof(Sorcerer)) as Sorcerer;
					//1-create the sorcerer copy --> Instantiate(...) 
					//Instantiate (sorcerer);
					//2-Network.Destroy the other one
					// Network.Destroy(GetComponent<NetworkView>().viewID);
					//Destroy(sorcerer.gameObject);
					//3- remove the client from the network -- disconnect it.
					if (Network.connections.Length == 1) {
						Debug.Log("Disconnecting: " + Network.connections[0].ipAddress + ":" + Network.connections[0].port);
						Network.CloseConnection(Network.connections[0], true);
					} else {
						print ("Error on disconnecting client: More than one client is connected -- this is not allowed.");
					}
					//Instantiate (sorcerer);
				}
				
				GUI.TextArea(new Rect(networkWindowX + 175, networkWindowY, 300, 125), _messageLog);
				
				// that's good for both: 
				if (Network.peerType == NetworkPeerType.Disconnected)
				{
					//GUI.Label(new Rect(10, 10, 200, 20), "Status: Disconnected");
					print ("Status: Disconnected.");
				}
				
			}
			
		}

		// server --> button to play one player mode:
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 50, 150, 50), "1 Player Mode")) {
				
				// connect only the server, no client:
				Network.InitializeServer (10, port, false);
				displayChat = true;
				
				//network instantiate both the fighter and sorcerer:
				transform.position = new Vector3(-2,0,0); // put the fighter to the left under the fighter buttons
				Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; //as Fighter; // N.B. place the network game object exactly where you want to spawn players.
				playerWasCreated = true;
				
				// always create the sorcerer before the fighter.
				transform.position = new Vector3(2,0,0); // put the sorcerer to the right under the sorcerer buttons
				Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
				sorcererWasCreated = true;
				
				lines = Network.Instantiate (linesPrefab,transform.position,transform.rotation,7)as GameObject;
				
				
				// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
				NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
				
				
			}
		}


	}


	// for server:
	void OnPlayerConnected(NetworkPlayer player) {
		AskClientForInfo(player);
		//print ();
		SendInfoToClient ("Received Info from Client");
	}
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("Host Destroyed disconnected Player"+player.ipAddress);
		
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
		
		//PlayerCamera.CameraTarget=transform;
	}

	// for server:
	void AskClientForInfo(NetworkPlayer player) {
		networkView.RPC("SetPlayerInfo", player, player);
	}
	
	// for server:
	[RPC]
	void ReceiveInfoFromClient(string someInfo) {
		_messageLog += someInfo + "\n";
	}
	

	// server receiving?
	[RPC]
	public void setServerBoolean(){
		sorcererPositionWasSent = true;//!sorcererPositionWasSent;
	}

	//============= RPC functions called from elsewhere in the code. ====================
	[RPC]
	public void OnSorcererPositionDeterminedAfterMapCreation(Vector3 position) {
		//if (networkView.isMine) {
		networkView.RPC ("SetSorcererPositionAfterMapCreation", RPCMode.All, position.x.ToString()+","+position.y.ToString()+","+position.z.ToString());
		//}
		//print (position);
		
	}

	[RPC]
	void SendInfoToClient(string msg) {
		msg = "SERVER: " + msg;
		networkView.RPC("ReceiveInfoFromServer", RPCMode.Others, msg);
		print (msg);
		_messageLog += msg + "\n";
	}




	[RPC]
	public void startClientLines(string s){
		networkView.RPC ("setClientLines", RPCMode.All, s);
	}
	
	[RPC]
	public void setClientLines(string s){
		MiniMap.setLine (s);
	}


}
