using UnityEngine;
using System.Collections;

public class ClientNetwork : MonoBehaviour {
	
	public string serverIP = "127.0.0.1";
	public string serverLocalIP;
	public int port = 25000;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;
	
	public bool isConnectedToServer = false;
	
	public int networkWindowX; // 
	public int networkWindowY; // 
	
	private int chatTextAreaWidth;
	private int chatTextAreaHeight;
	private int chatTextAreaOffsetX;
	private int chatTextAreaOffsetY;
	
	private int chatInputWidth;
	private int chatInputHeight;
	private int chatInputOffsetX;
	private int chatInputOffsetY;

	private int chatSendButtonWidth; 
	private int chatSendButtonHeight;
	private int chatSendButtonOffsetX;
	private int chatSendButtonOffsetY;

	public int networkWindowButtonWidth;
	public int networkWindowButtonHeight;
	private int networkWindowButtonsOffsetX;
	private int networkWindowButtonsOffsetY;
	
	private Fighter playerPrefab;
	private Sorcerer sorcererPrefab;
	public GameObject linesPrefab;
	public GameObject lines;
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
	public ChooseClass classChooser;
	private int framesToWaitForFocusCorrectCharacter;

	public GUIStyle style;
	public GUIStyle styleDefaultTextArea;
	public GUIStyle labelStyle;
	public GUIStyle background;
	float backgroundBox = 100f;

	bool displayChat;

	string textFieldString = "--";
	string textFieldStringInPreviousFrame;
	bool selectTextField = true;

	private int framesCounterBeforeFadeOutChat = 0;
	private int numberOfFramesToWaitBeforeFadingOutChat = 800;

	public void Awake() {

		networkWindowX = 0;//Screen.width - 500;
		networkWindowY = Screen.height - 150;//10;

		chatTextAreaWidth = 300;
		chatTextAreaHeight = 125;
		chatTextAreaOffsetX = networkWindowX;
		chatTextAreaOffsetY = networkWindowY;
		
		chatInputWidth = 250;
		chatInputHeight = 25;
		chatInputOffsetX = 8;//180;
		chatInputOffsetY = chatTextAreaHeight;//125;
		
		chatSendButtonWidth = chatTextAreaWidth - chatInputWidth; 
		chatSendButtonHeight = chatInputHeight;
		chatSendButtonOffsetX = networkWindowX + chatInputOffsetX + chatInputWidth - 25;
		chatSendButtonOffsetY = networkWindowY + chatInputOffsetY + chatInputHeight - 25;

		networkWindowButtonWidth = 150;
		networkWindowButtonHeight = 25;
		networkWindowButtonsOffsetX = chatTextAreaOffsetX + chatTextAreaWidth;
		networkWindowButtonsOffsetY = chatTextAreaOffsetY;


		//AddNetworkView();
		framesToWait = 0;
		playerWasCreated = false;
		sorcererWasCreated = false;
		bothPlayerAndSorcererWereFound = false;

		sorcererPositionAfterMapCreation = Vector3.zero;
		sorcererPositionWasSent = false;
		focusCorrectPlayerWasDone = false;

		framesToWaitForFocusCorrectCharacter = 0;

		displayChat = true;

		Input.eatKeyPressOnTextFieldFocus = true; // to allow detecting enter key when the chat input field is focused.
		//styleDefaultTextArea = GUI.skin.textArea; 

		framesCounterBeforeFadeOutChat = 0;
		numberOfFramesToWaitBeforeFadingOutChat = 800;
	} 

	public void Start() {
	}

	public void Update() {

		// toggle display chat window:
		if(Input.GetKeyDown ("enter") || Input.GetKeyDown ("return")){ //if(Input.GetKeyDown ("n")){
			displayChat = !displayChat;
			framesCounterBeforeFadeOutChat = 0;
			GUI.FocusControl("ChatBox"); // not always working why?
		}
		if(textFieldString != textFieldStringInPreviousFrame) {
			framesCounterBeforeFadeOutChat = 0;
			textFieldStringInPreviousFrame = textFieldString;
		}

		// make chat control disappear after a few seconds, but keep the textarea:
		//print ("HAI --> "+GUI.GetNameOfFocusedControl());
		if(displayChat == true && framesCounterBeforeFadeOutChat < numberOfFramesToWaitBeforeFadingOutChat ) { // GUI.GetNameOfFocusedControl() != "ChatBox" --> not working !
			framesCounterBeforeFadeOutChat++;
		}
		else if(displayChat == true && framesCounterBeforeFadeOutChat >= numberOfFramesToWaitBeforeFadingOutChat) { //GUI.GetNameOfFocusedControl() != "ChatBox" --> working!
			displayChat = false;
		}


		// generate the map only after the players have been created (because they are needed for some reason for the map generation code:
		bool serverAndClientAreBothConnected = Network.connections.Length != 0; // 0 length means no connection, i.e. no client connected to server.
		if(serverAndClientAreBothConnected && Application.loadedLevelName == "StartScreen" && bothPlayerAndSorcererWereFound) {	// && Network.isServer

			if(Network.isServer) {
				framesToWait++;
				if(framesToWait > 700) {
					Loading.show ();
					// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
					NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
				}

			} else {
				Loading.show ();
				NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration

			}
		}


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

		// =======================\
		// client and server --> logic for keeping fighter and sorcerer across scenes (dontdestroyonload):
		if(Application.loadedLevelName == "StartScreen") { //&& (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer)).name != "Sorcerer"

			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer));
			Fighter fighter = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));	

			//GameObject lines = GameObject.Find ("lines");
			if(lines != null){
				DontDestroyOnLoad(lines);
				lines.gameObject.layer = LayerMask.NameToLayer ("Minimap");
				lines.name = "lines";
			}
			GameObject lines2 = GameObject.Find ("lines(Clone)");
			if(Network.isClient && lines2 != null){

				DontDestroyOnLoad(lines2);
				lines2.gameObject.layer = LayerMask.NameToLayer ("Minimap");
				lines2.name = "lines";
			}
			
			if(sorcerer != null && fighter != null) {
				DontDestroyOnLoad (sorcerer);
				DontDestroyOnLoad (fighter);
				sorcerer.name = "Sorcerer";
				fighter.name = "Fighter";
				bothPlayerAndSorcererWereFound = true;
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
	
	[RPC]
	private void AddNetworkView() {
		// this is to dynamically add a networkview at runtime, can be useful for example if creating players or other game objects at runtime.
		
		//        gameObject.AddComponent<NetworkView>();
		//        gameObject.networkView.observed = this;
		//        gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		//        gameObject.networkView.viewID = Network.AllocateViewID();
	}
	

	void OnGUI() {

		GUI.skin.button = style;

		if(!Application.isLoadingLevel){
			// button to connect as server:
			background.fontSize = 100 * Screen.height/598;
			if (Network.peerType == NetworkPeerType.Disconnected) {
				GUI.Box(new Rect(0.1f, 0.1f, Screen.width - 0.1f, Screen.height - 0.1f),"Second Soul", background);
				GUI.Label (new Rect(Screen.width / 2 - 150, Screen.height/2 + 25, 300, 50),"<Size=30>Network Choices</Size>",style);
				if (GUI.Button (new Rect (Screen.width / 2 - 225, Screen.height/2 + 100, 150, 50), "Connect as a server", style)) {
					// connect:
					Network.InitializeServer (10, port, false);
					displayChat = true;
				}
			}
			
			// after connecting: if you're a server:
			if(displayChat) {
				if (Network.peerType == NetworkPeerType.Server) {
					GUI.Label(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 0, networkWindowButtonWidth, networkWindowButtonHeight), "Server", labelStyle);
					GUI.Label(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 1, networkWindowButtonWidth, networkWindowButtonHeight), "Clients attached: " + Network.connections.Length, labelStyle);
					
					if (GUI.Button(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 2, networkWindowButtonWidth, networkWindowButtonHeight), "Quit server")) {
						Network.Disconnect(); 
						Application.Quit();
					}
					if (GUI.Button(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 3, networkWindowButtonWidth, networkWindowButtonHeight), "Send hi to client"))
						SendInfoToClient("Hello client!");
					
					if (GUI.Button(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 4, networkWindowButtonWidth, networkWindowButtonHeight), "Remove Client")) {
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
						} else if (Network.connections.Length > 1) {
							print ("Error on disconnecting client: More than one client is connected -- this is not allowed.");
						}
						//Instantiate (sorcerer);
					}

					// that's good for both: 
					if (Network.peerType == NetworkPeerType.Disconnected)
					{
						//GUI.Label(new Rect(10, 10, 200, 20), "Status: Disconnected");
						print ("Status: Disconnected.");
					}

					// chat text area:
					//GUI.TextArea(new Rect(networkWindowX + 175, networkWindowY, chatTextAreaWidth, 125), _messageLog, style); // style // "box"
					//styleDefaultTextArea

					// chat text input:
					GUI.SetNextControlName("ChatBox");
					textFieldString = GUI.TextField(new Rect(networkWindowX + chatInputOffsetX, networkWindowY + chatInputOffsetY, chatInputWidth, chatInputHeight), textFieldString, style); // style // "box"

					//bool isEnterPressed = (Event.current.Equals (Event.KeyboardEvent("return")));
					//bool isEnterPressed = (Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.Return);
					bool isEnterPressed = (Event.current.type == EventType.keyUp) && (Event.current.keyCode == KeyCode.Return);
					//GUI.GetNameOfFocusedControl() == "input" && Event.current.keyCode == KeyCode.Return
					if (isEnterPressed || GUI.Button (new Rect (chatSendButtonOffsetX, chatSendButtonOffsetY, chatSendButtonWidth, chatSendButtonHeight), "Send", "box") && textFieldString != "") {
						//_messageLog += textFieldString + "\n";
						SendInfoToClient(textFieldString);
						textFieldString = "";
					}
				}

			}

			// =========================
			
			// button to connect as a client:
			if (Network.peerType == NetworkPeerType.Disconnected) {
				if (GUI.Button (new Rect (Screen.width / 2 + 75, Screen.height / 2 + 100, 150, 50), "Connect as a Client")) {
					ConnectToServer ();
					displayChat = true;

				}
			}
			
			// after connecting if you're a client:
			if(displayChat &&  !(Application.loadedLevelName != "StartScreen")) {
				if (Network.peerType == NetworkPeerType.Client) {
					GUI.Label(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 0, 150, networkWindowButtonHeight), "client", labelStyle);
					
					if (GUI.Button(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 1, 150, networkWindowButtonHeight), "Logout")) {
						Network.Disconnect();
						// also destroy the player game object here, since OnPlayerDisconnected only works on the server side, which means the player will be destroyed for everyone except the one who created it.
						
					}
					
					if (GUI.Button(new Rect(networkWindowButtonsOffsetX, networkWindowButtonsOffsetY + networkWindowButtonHeight * 2, 150, networkWindowButtonHeight), "SendHello to server")) {
						someInfo = "hello server!";
						SendInfoToServer(someInfo);
					}

					// chat text input:
					GUI.SetNextControlName("ChatBox");
					textFieldString = GUI.TextField(new Rect(networkWindowX + chatInputOffsetX, networkWindowY + chatInputOffsetY, chatInputWidth, chatInputHeight), textFieldString, style); // style // "box"

					//var isEnterPressed = (Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.Return);
					bool isEnterPressed = (Event.current.type == EventType.keyUp) && (Event.current.keyCode == KeyCode.Return);
					if ((isEnterPressed || GUI.Button (new Rect (chatSendButtonOffsetX, chatSendButtonOffsetY, chatSendButtonWidth, chatSendButtonHeight), "Send", "box")) && textFieldString != "") {
						//_messageLog += textFieldString + "\n";
						SendInfoToServer(textFieldString);
						textFieldString = "";
					}
					
				}

			}

			// chat text area:
			//GUI.TextArea(new Rect(250, 100, 300, 100), _messageLog, labelStyle);
			//GUI.TextArea(new Rect(networkWindowX + 175, networkWindowY, chatTextAreaWidth, 125), _messageLog, style); // style // "box"
			GUI.TextArea(new Rect(chatTextAreaOffsetX, chatTextAreaOffsetY, chatTextAreaWidth, chatTextAreaHeight), _messageLog, style); // style // "box"


			// button to play one player mode:
			if (Network.peerType == NetworkPeerType.Disconnected) {
				if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 100, 150, 50), "1 Player Mode")) {
					
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
					
					Loading.show ();
					// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
					NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
					
					
				}
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
		isConnectedToServer = true;
	}
	void OnDisconnectedToServer() {
		_messageLog += "Disconnected from server" + "\n";
		Network.Destroy (playerPrefab.gameObject);
		isConnectedToServer = false;
	}
	
	[RPC]
	void SendInfoToClient(string msg) {
		msg = "SERVER: " + msg;
		networkView.RPC("ReceiveInfoFromServer", RPCMode.Others, msg);
		print (msg);
		_messageLog += msg + "\n";
	}


	void OnLevelWasLoaded(int level) {
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

	[RPC]
	public void startClientLines(string s){
		networkView.RPC ("setClientLines", RPCMode.All, s);
	}

	[RPC]
	public void setClientLines(string s){
		MiniMap.setLine (s);
	}

	[RPC]
	public void setServerBoolean(){
		sorcererPositionWasSent = true;//!sorcererPositionWasSent;
	}

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
		//print ("toggle stats displayed WORKED");
	}
	
}

