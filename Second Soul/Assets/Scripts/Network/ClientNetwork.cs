using UnityEngine;
using System.Collections;

public class ClientNetwork : MonoBehaviour {
	
	public string serverIP = "127.0.0.1";
	public int port = 25000;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;
	
	public bool isConnectedToServer = false;
	
	public int networkWindowX;
	public int networkWindowY;
	public int networkWindowButtonWidth;
	public int networkWindowButtonHeight;
	
	public Fighter playerPrefab;
	public Sorcerer sorcererPrefab;

	int framesToWait;

	public string gameSceneToLoad;
//	public Sorcerer sorcerer;
//	public Fighter fighter;
	private bool playerWasCreated;
	private bool sorcererWasCreated;
	private bool bothPlayerAndSorcererWereFound;

	private static Vector3 sorcererPositionAfterMapCreation;
	private bool sorcererPositionWasUpdated; // ... AfterMapGeneration;
	private bool sorcererPositionWasSent; // after map generation, but used before sending it.
	private bool focusCorrectPlayerWasDone;

	private int framesToWaitForFocusCorrectCharacter;

	public void Awake() {
		
		networkWindowX = Screen.width - 500;
		networkWindowY = 10;
		networkWindowButtonWidth = 150;
		networkWindowButtonHeight = 25;
		//AddNetworkView();
		framesToWait = 0;
		playerWasCreated = false;
		sorcererWasCreated = false;
		bothPlayerAndSorcererWereFound = false;

		sorcererPositionAfterMapCreation = Vector3.zero;
		sorcererPositionWasSent = false;
		focusCorrectPlayerWasDone = false;

		this.gameSceneToLoad = "SaveNetwork";
		framesToWaitForFocusCorrectCharacter = 0;
	} 

	public void Start() {
	}

	public void Update() {

		// generate the map only after the players have been created (becasue they are needed for some reason for the map generation code:
		bool serverAndClientAreBothConnected = Network.connections.Length != 0; // 0 length means no connection, i.e. no client connected to server.
		//print ("BEFORE 1: "+Network.connections.Length);
		if(serverAndClientAreBothConnected && Application.loadedLevelName == "NetworkStartMenu" && bothPlayerAndSorcererWereFound) {	// && Network.isServer

			if(Network.isServer) {
				framesToWait++;
				if(framesToWait > 700) {
					// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
					print ("BEFORE 3");
					NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
					print ("AFTER");
				}

			} else {
				print ("BEFORE 3");
				NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
				print ("AFTER");

			}
		}


		//===================
		if (Network.isClient && Application.loadedLevelName == "NetworkStartMenu" && !sorcererWasCreated) {
			Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
			//sorcerer.name = "Sorcerer";
			//fighter.name = "Fighter";
			//						DontDestroyOnLoad (sorcerer);
			//						DontDestroyOnLoad (fighter);
			sorcererWasCreated = true;


		} 
		if(Network.isServer && Application.loadedLevelName == "NetworkStartMenu" && !playerWasCreated) {
			Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; //as Fighter; // N.B. place the network game object exactly where you want to spawn players.
			playerWasCreated = true;

		}

		if(Application.loadedLevelName == "NetworkStartMenu") { //&& (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer)).name != "Sorcerer"
			//			print ("we're in the if for is NetworkStartMenu");
			//			Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
			//			Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
			//			sorcerer.name = "Sorcerer";
			//			fighter.name = "Fighter";
			//			//		DontDestroyOnLoad (sorcerer.gameObject);
			//			//		DontDestroyOnLoad (fighter.gameObject);

			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
			Fighter fighter = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));			

			if(sorcerer != null && fighter != null) {
				DontDestroyOnLoad (sorcerer);
				DontDestroyOnLoad (fighter);
				sorcerer.name = "Sorcerer";
				fighter.name = "Fighter";
				bothPlayerAndSorcererWereFound = true;
			}
			

		}

//		// focus correct player:
//		if (Application.loadedLevelName == this.gameSceneToLoad && !focusCorrectPlayerWasDone) {
//
//			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
//			Fighter fighter = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));			
//
//			if(sorcerer != null && fighter != null) {
//
//				framesToWaitForFocusCorrectCharacter++;
//				if(framesToWaitForFocusCorrectCharacter > 900) {
//					if(Network.isServer) {
//						//focus sorcerer:
//						fighter.playerEnabled = true;
//						//sorcerer.playerEnabled = false; // sorcerer is always the reverse of the fighter, see Sorcerer.cs:57
//						
//					}
//					else if (Network.isClient) {
//						//focus sorcerer:
//						//sorcerer.playerEnabled = true;
//						fighter.playerEnabled = false;
//						
//					}
//					focusCorrectPlayerWasDone = true;
//				}
//
//
//
//			}
//		}

		//=========================
		// updating the sorcerer position after the map was 
		if(sorcererPositionAfterMapCreation != Vector3.zero && !sorcererPositionWasUpdated) {
//			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
//			sorcerer.transform.position = sorcererPositionAfterMapCreation;
//			sorcererPositionWasUpdated = true;
		}

		if(Network.isClient) {
			//networkView.RPC ("SetSorcererPositionAfterMapCreation", RPCMode.Others, "hello sent from client");

		} else if (Network.isServer && MapGeneration.mapGenerationCompleted && !sorcererPositionWasSent) { // 
			//networkView.RPC ("SetSorcererPositionAfterMapCreation", RPCMode.Others, "hello sent from server");
			//networkView.RPC ("onStatsDisplayed", RPCMode.Others);

			//onStatsDisplayed();
			//ClientNetwork clientNetwork = (ClientNetwork)GameObject.FindObjectOfType(typeof(ClientNetwork));

			OnSorcererPositionDeterminedAfterMapCreation(MapGeneration.playerStartPositionVector3);
			//clientNetwork.onStatsDisplayed();
//			sorcererPositionWasSent = true;

			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
			//if(sorcerer.transform.position != Vector3.zero) {

			
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
	
	// Immediately instantiate new connected player's character
	// when successfully connected to the server.
	//public Transform playerPrefab;
	//code move to already existing OnConnectedToServer function
	
	void OnGUI() {
		
		
		// button to connect as server:
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2, 150, 50), "Connect as a server")) {
				
				// connect:
				Network.InitializeServer (10, port, false);
//				Fighter fighter = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
//				DontDestroyOnLoad(fighter.gameObject);

//				//Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
//				Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
//				//sorcerer.name = "Sorcerer";
//				fighter.name = "Fighter_FIGHTER";
//				//DontDestroyOnLoad (sorcerer.gameObject);
//				DontDestroyOnLoad (fighter.gameObject);


				// disable second soul:

				
			}
		}
		
		// after connecting: if you're a server:
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
			
			
			GUI.TextArea(new Rect(networkWindowX + 175, networkWindowY, 300, 100), _messageLog);
			
			// that's good for both: 
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				//GUI.Label(new Rect(10, 10, 200, 20), "Status: Disconnected");
				print ("Status: Disconnected.");
			}
			
		}
		
		// =========================
		
		// button to connect as a client:
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2, 150, 50), "Connect as a Client")) {
				//            if (Network.peerType == NetworkPeerType.Disconnected) {
				//                if (GUI.Button(new Rect(100, 100, 150, 25), "Connect")) {
				ConnectToServer ();

				
				//                }
				//            }
				
				// disable controlling PC:
				
				// or instead, spaw a new sorcerer:
				//Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
				//sorcerer2 = Instantiate()
				
				//Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
				//Network.Instantiate(sorcerer, new Vector3(349.2448f, 0, 973.0397f), Quaternion.identity, 0);
				
			}
		}
		
		// after connecting if you're a client:
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
	
	// for client:
	private void ConnectToServer() {
		Network.Connect(serverIP, port);
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
		// added:
		//Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
		//DontDestroyOnLoad(sorcerer.gameObject);

//		Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
//		//Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
//		sorcerer.name = "Sorcerer_SORCERER";
//		//fighter.name = "Fighter";
//		DontDestroyOnLoad (sorcerer.gameObject);
//		//DontDestroyOnLoad (fighter.gameObject);


		isConnectedToServer = true;
	}
	void OnDisconnectedToServer() {
		_messageLog += "Disconnected from server" + "\n";
		Network.Destroy (playerPrefab.gameObject);
		isConnectedToServer = false;
	}
	
	//    [RPC]
	//    void ReceiveInfoFromClient(string someInfo) { }
	//    [RPC]
	//    void SendInfoToClient(string someInfo) { } 
	[RPC]
	void SendInfoToClient(string msg) {
		msg = "SERVER: " + msg;
		networkView.RPC("ReceiveInfoFromServer", RPCMode.Others, msg);
		print (msg);
		_messageLog += msg + "\n";
	}
	
	
	// use networkView.isMine 
	
	// this is spectating code, can go in both server and client cube/sphere code:
	//    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	//    {
	//        if (stream.isWriting) {
	//            Vector3 pos = transform.position;
	//            stream.Serialize (ref pos);
	//        }
	//        else {
	//            Vector3 receivedPosition = Vector3.zero;
	//            stream.Serialize(ref receivedPosition);
	//            transform.position = receivedPosition;
	//        }
	//    }

	void OnLevelWasLoaded(int level) {

//		if(Network.isClient) {
//			Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
//			sorcerer.name = "Sorcerer";
//		} 
//		else if (Network.isServer) {
//			Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.		fighter.name = "Fighter";
//			fighter.name = "Fighter";
//		}
//		if (Network.isServer) {
//			sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
//			fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
//			//sorcerer.name = "Sorcerer";
//			//fighter.name = "Fighter";
////						DontDestroyOnLoad (sorcerer);
////						DontDestroyOnLoad (fighter);
//		} 

//		if(Network.isClient) {
//			sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
//			fighter = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));
////			sorcerer.name = "Sorcerer";
////			fighter.name = "Fighter"; // not working, they are still named Fighter(clone) on the server.
////						DontDestroyOnLoad (sorcerer);
////						DontDestroyOnLoad (fighter);
//
//		}


		
	}

	//============= RPC functions called from elsewhere in the code. ====================
	[RPC]
	public void OnSorcererPositionDeterminedAfterMapCreation(Vector3 position) {
		//if (networkView.isMine) {
		networkView.RPC ("SetSorcererPositionAfterMapCreation", RPCMode.All, position.x.ToString()+","+position.y.ToString()+","+position.z.ToString());
		//}
		print (position);

	}
	[RPC]
	public void SetSorcererPositionAfterMapCreation(string position) {
		//sorcererPositionAfterMapCreation = position;
		print (position);
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
			networkView.RPC ("setServerBoolean",RPCMode.Server);
		}
	}
	[RPC]
	public void setServerBoolean(){
		sorcererPositionWasSent = !sorcererPositionWasSent;
	}
	
	// debug:
	// watch onStatsDisplayed:
	[RPC]
	public void onStatsDisplayed() {
		//if(networkView.isMine){
		networkView.RPC("toggleStatsDisplayed", RPCMode.All);//RPCMode.Others);
		//print ("hello");
		//}
	}
	[RPC]
	void toggleStatsDisplayed() {
		//DisplayPlayerStats statsDisplayScript = (DisplayPlayerStats) GameObject.FindObjectOfType(typeof(DisplayPlayerStats));
		//statsDisplayScript.boolChange ();
		print ("toggle stats displayed WORKED");
	}
	
}

