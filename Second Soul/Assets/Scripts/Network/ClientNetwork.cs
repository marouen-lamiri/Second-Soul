using UnityEngine;
using System.Collections;

public class ClientNetwork : MonoBehaviour, ISorcererSubscriber {

	// Old code to keep to use a localhost connection on one computer:
	//	public string serverIP = "127.0.0.1";
	//	public string serverLocalIP;
	//	public int port = 25000;

	// master server connection variables
	private int connections = 4;
	private int masterServerPort = 25000;

	// 
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


	// master server:
	private const string typeName = "SecondSoul";
	private const string gameName = "RoomName";

	private int hostButtonsPositionX;
	private int hostButtonsPositionY;
	private int hostButtonsHeight;
	private int hostButtonsWidth;
	private int hostButtonsSpacing;

	private int connectAsClientButtonDistanceAwayFromCenterOfScreenX;
	private int connectAsClientButtonDistanceAwayFromCenterOfScreenY;
	private int connectAsClientButtonPositionX;
	private int connectAsClientButtonPositionY;
	private int connectAsClientButtonWidth;
	private int connectAsClientButtonHeight;

	private int connectAsServerButtonDistanceAwayFromCenterOfScreenX;
	private int connectAsServerButtonDistanceAwayFromCenterOfScreenY;
	private int connectAsServerButtonPositionX;
	private int connectAsServerButtonPositionY;
	private int connectAsServerButtonWidth;
	private int connectAsServerButtonHeight;

	private float SecondSoulLabelPositionX = 0.1f;
	private float SecondSoulLabelPositionY = 0.1f;
	private float SecondSoulLabelWitdh = Screen.width - 0.1f;
	private float SecondSoulLabelHeight = Screen.height - 0.1f;
	private string SecondSoulLabel = "Second Soul";

	private int networkChoicesLabelPositionX = Screen.width / 2 - 150;
	private int networkChoicesLabelPositionY = Screen.height/2 + 25;
	private int networkChoicesLabelHeight = 300;
	private int networkChoicesLabelWidth = 50;
	private string networkChoicesLabel = "<Size=30>Network Choices</Size>";

	private int onePlayerModeButtonPositionX = Screen.width / 2 - 75;
	private int onePlayerModeButtonPositionY = Screen.height / 2 + 100;
	private int onePlayerModeButtonWidth = 150;
	private int onePlayerModeButtonHeight = 50;

	
	// jump into game:
	private SorcererInstanceManager sim;
	private Sorcerer sorcerer; // needs to be a variable now so it can be set by the ISorcerer function.

	// more magic numbers strings:
	private string chatBoxGUIName = "ChatBox";
	public static string StartScreenOfficialSceneName = "StartScreen";
	private int numberOfFramesToWaitBeforeServerLoadsGameScene = 700; 
	
	// master server server methods:
	private void StartServer()
	{
		Network.InitializeServer(connections, masterServerPort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
	}
	// master server client methods:
	private HostData[] hostList;
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
		print ("OnMasterServerEvent fired !!!!!!!!!!!!!! ");
	}
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}


	public void Awake() {

		subscribeToSorcererInstancePublisher (); // jump into game

		//jump into game:
		if(sim == null) {
			//GameObject network = GameObject.FindObjectOfType(typeof (Network));
			//Transform parent_ = transform.parent;
			sim = GetComponentInParent<SorcererInstanceManager>();
			if(sim == null){
				sim = GetComponent<SorcererInstanceManager> ();
				//sim = GameObject.FindObjectOfType(typeof (SorcererInstanceManager));
				if(sim == null) {
					print ("Hey the Network game object is probably missing in this scene. (If it's the case the chat won't work).");
					sim = (SorcererInstanceManager)GameObject.FindObjectOfType(typeof (SorcererInstanceManager));
				}
			}
		}
		
		// chat & network window widgets:
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

		// connection buttons:
		connectAsClientButtonDistanceAwayFromCenterOfScreenX = 75;
		connectAsClientButtonDistanceAwayFromCenterOfScreenY = 100;
		connectAsClientButtonPositionX = Screen.width / 2 + connectAsClientButtonDistanceAwayFromCenterOfScreenX;
		connectAsClientButtonPositionY = Screen.height / 2 + connectAsClientButtonDistanceAwayFromCenterOfScreenY;
		connectAsClientButtonWidth = 150;
		connectAsClientButtonHeight = 50;

		connectAsServerButtonDistanceAwayFromCenterOfScreenX = -225;
		connectAsServerButtonDistanceAwayFromCenterOfScreenY = 100;
		connectAsServerButtonPositionX = Screen.width / 2 + connectAsServerButtonDistanceAwayFromCenterOfScreenX;
		connectAsServerButtonPositionY = Screen.height / 2 + connectAsServerButtonDistanceAwayFromCenterOfScreenY;
		connectAsServerButtonWidth = 150;
		connectAsServerButtonHeight = 50;
		
		SecondSoulLabelPositionX = 0.1f;
		SecondSoulLabelPositionY = 0.1f;
		SecondSoulLabelWitdh = Screen.width - 0.1f;
		SecondSoulLabelHeight = Screen.height - 0.1f;
		SecondSoulLabel = "Second Soul";

		networkChoicesLabelPositionX = Screen.width / 2 - 150;
		networkChoicesLabelPositionY = Screen.height/2 + 25;
		networkChoicesLabelHeight = 300;
		networkChoicesLabelWidth = 50;
		networkChoicesLabel = "<Size=30>Network Choices</Size>";

		onePlayerModeButtonPositionX = Screen.width / 2 - 75;
		onePlayerModeButtonPositionY = Screen.height / 2 + 100;
		onePlayerModeButtonWidth = 150;
		onePlayerModeButtonHeight = 50;

		
		// master server room list buttons:
		hostButtonsPositionX = connectAsClientButtonPositionX + connectAsClientButtonWidth + 10; // put the list to the right of the client connection button.
		hostButtonsPositionY = connectAsClientButtonPositionY; // 100;
		hostButtonsHeight = connectAsClientButtonHeight - 10;
		hostButtonsWidth = connectAsClientButtonWidth - 30;
		hostButtonsSpacing = connectAsClientButtonHeight;


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
			//displayChat = !displayChat;
			displayChat = true;
			framesCounterBeforeFadeOutChat = 0;
			if(displayChat) {
				GUI.FocusControl(chatBoxGUIName); // not always working why?
			}

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
		if(serverAndClientAreBothConnected && Application.loadedLevelName == StartScreenOfficialSceneName && bothPlayerAndSorcererWereFound) {	// && Network.isServer

			if(Network.isServer) {
				framesToWait++;
				if(framesToWait > numberOfFramesToWaitBeforeServerLoadsGameScene) {
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
		if (SorcererInstanceManager.getSorcerer() == null) {// Application.loadedLevelName == StartScreenOfficialSceneName && !sorcererWasCreated) { //Network.isClient && 

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
				// this is for the case: in start menu, first connection:
				SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient();
				SorcererInstanceManager.createAndSwapNewSorcerer(sorcererPrefab, this.transform); // 
				//Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
				sorcererWasCreated = true;
			}


		} 
		//debug:
		if(SorcererInstanceManager.getSorcerer() == null) {
			print ("sorcerer IS ABSENT, is null in sorcererInstanaceManager in update funciton.");
		} else {
			print ("sorcerer IS PRESENT, is NOT null in sorcererInstanaceManager in update funciton");
		}


		// ------------------
		// server --> logic for creating (different types of) fighter:
		if(Application.loadedLevelName == StartScreenOfficialSceneName && !playerWasCreated) { //Network.isServer && 

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
		//if(Application.loadedLevelName == StartScreenOfficialSceneName) { //&& (Sorcerer)GameObject.FindObjectOfType(typeof(Sorcerer)).name != "Sorcerer"

			Sorcerer sorcerer2 = (Sorcerer)GameObject.FindObjectOfType (typeof (Sorcerer)); //sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // 
			Fighter fighter2 = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));	

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
			
			if(sorcerer2 != null && fighter2 != null) {
				DontDestroyOnLoad (sorcerer2);
				DontDestroyOnLoad (fighter2);
				sorcerer2.name = "Sorcerer";
				fighter2.name = "Fighter";
				bothPlayerAndSorcererWereFound = true;
			}
			

		//}

		//=========================
		// server --> send 
		if (Network.isServer && MapGeneration.mapGenerationCompleted && !sorcererPositionWasSent) { // 
			OnSorcererPositionDeterminedAfterMapCreation(MapGeneration.playerStartPositionVector3);
			Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType (typeof (Sorcerer)); //sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // 
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

			// =========================

			// button to connect as server:
			background.fontSize = 100 * Screen.height/598;
			if (Network.peerType == NetworkPeerType.Disconnected) {
				GUI.Box(new Rect(SecondSoulLabelPositionX, SecondSoulLabelPositionY, SecondSoulLabelWitdh, SecondSoulLabelHeight),SecondSoulLabel, background);
				GUI.Label (new Rect(networkChoicesLabelPositionX, networkChoicesLabelPositionY, networkChoicesLabelHeight, networkChoicesLabelWidth), networkChoicesLabel, style);
				if (GUI.Button (new Rect (connectAsServerButtonPositionX, connectAsServerButtonPositionY, connectAsServerButtonWidth, connectAsServerButtonHeight), "Connect as a server", style)) {
					StartServer();
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
						Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)) as Sorcerer; //sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // 
						//1-create the sorcerer copy --> Instantiate(...) 
						//Instantiate (sorcerer);
						//2-Network.Destroy the other one
						// Network.Destroy(GetComponent<NetworkView>().viewID);
						//Destroy(sorcerer.gameObject);
						//3- remove the client from the network -- disconnect it.

						//instead: swap them with the manager:
						//SorcererInstanceManager.createAndSwapNewSorcerer(); // now server owns a newly created sorcerer. // no need to make the server own it. the network instance works great for all purposes.

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
					GUI.SetNextControlName(chatBoxGUIName);
					textFieldString = GUI.TextField(new Rect(networkWindowX + chatInputOffsetX, networkWindowY + chatInputOffsetY, chatInputWidth, chatInputHeight), textFieldString); // style // "box"

					//bool isEnterPressed = (Event.current.Equals (Event.KeyboardEvent("return")));
					//bool isEnterPressed = (Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.Return);
					bool isEnterPressed = (Event.current.type == EventType.keyUp) && (Event.current.keyCode == KeyCode.Return);
					//GUI.GetNameOfFocusedControl() == "input" && Event.current.keyCode == KeyCode.Return
					if ((isEnterPressed || GUI.Button (new Rect (chatSendButtonOffsetX, chatSendButtonOffsetY, chatSendButtonWidth, chatSendButtonHeight), "Send", "box")) && textFieldString != "") {
						//_messageLog += textFieldString + "\n";
						SendInfoToClient(textFieldString);
						textFieldString = "";
					}
				}

			}

			// =========================
			
			// button to connect as a client:
			if (Network.peerType == NetworkPeerType.Disconnected) {
				if (GUI.Button (new Rect (connectAsClientButtonPositionX, connectAsClientButtonPositionY, connectAsClientButtonWidth, connectAsClientButtonHeight), "Connect as a Client")) {
					Loading.show ();
					RefreshHostList(); //ConnectToServer (); // to replaced with the real master server code above. 
					displayChat = true;

				}
			}
			
			// after connecting if you're a client:
			if(displayChat) { // &&  !(Application.loadedLevelName != "StartScreen")
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
					GUI.SetNextControlName(chatBoxGUIName);
					textFieldString = GUI.TextField(new Rect(networkWindowX + chatInputOffsetX, networkWindowY + chatInputOffsetY, chatTextAreaWidth + chatInputOffsetX, chatInputHeight), textFieldString); // style // "box"

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
			GUI.TextArea(new Rect(networkWindowX + chatInputOffsetX, chatTextAreaOffsetY, chatTextAreaWidth, chatTextAreaHeight), _messageLog); // style // "box"


			// button to play one player mode:
			if (Network.peerType == NetworkPeerType.Disconnected) {
				if (GUI.Button (new Rect (onePlayerModeButtonPositionX, onePlayerModeButtonPositionY, onePlayerModeButtonWidth, onePlayerModeButtonHeight), "1 Player Mode")) {

					Loading.show ();

					// connect only the server, no client:
					StartServer (); //Network.InitializeServer (10, port, false); // also to replace with real master server call StartServer();
					displayChat = true;
					
					//network instantiate both the fighter and sorcerer:
					transform.position = new Vector3(-2,0,0); // put the fighter to the left under the fighter buttons
					Fighter fighter = (Fighter) Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; //as Fighter; // N.B. place the network game object exactly where you want to spawn players.
					playerWasCreated = true;
					
					// always create the sorcerer before the fighter.
					transform.position = new Vector3(2,0,0); // put the sorcerer to the right under the sorcerer buttons
					if(sorcererWasCreated) {
						SorcererInstanceManager.createAndSwapNewSorcerer(); // 
						//Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
					}
					else {
						SorcererInstanceManager.createAndSwapNewSorcerer(sorcererPrefab, this.transform); // 
						//Sorcerer sorcerer = (Sorcerer) Network.Instantiate(sorcererPrefab, transform.position, transform.rotation, 0) as Sorcerer; //as Sorcerer; // N.B. place the network game object exactly where you want to spawn players.
					}
					sorcererWasCreated = true;
					
					lines = Network.Instantiate (linesPrefab,transform.position,transform.rotation,7)as GameObject;

					// load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
					NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1); //NetworkingCollaboration
					
					
				}
			}
		}

		// master server: show list of available rooms:
		if (!Network.isClient && !Network.isServer)
		{
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(hostButtonsPositionX, hostButtonsPositionY + ((hostButtonsHeight+hostButtonsSpacing) * i), hostButtonsWidth, hostButtonsHeight), hostList[i].gameName))
					{
						if(sim != null) {
							//sim.onClientConnects();

						}
						//onClientConnects();
						JoinServer(hostList[i]);
					}
				}
			}
		}
		
	}

	// Old code to keep to use a localhost connection:

	// for client:
	//	private void ConnectToServer() {
	//		Network.Connect(serverIP, port);
	//		if (!Network.isClient) {
	//			//Network.Connect(serverLocalIP,port);
	//		}
	//	}
	
	// for server:
	void OnPlayerConnected(NetworkPlayer player) {
		AskClientForInfo(player);
		//print ();
		SendInfoToClient ("Received Info from Client");

	}
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("Host Destroyed disconnected Player"+player.ipAddress);
		
		//Network.RemoveRPCs(player, 0); // NB beware confusion here, player here is a client player -- i.e. a sorcerer!
		//Network.RemoveRPCs (SorcererInstanceManager.getSorcerer().networkView.viewID); // avoid that the client when reconnecting gets the server's sorcerer, in addition to his.
		//Network.DestroyPlayerObjects(player);
		//Network.Destroy (playerPrefab.gameObject);//?

	
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
	void OnDisconnectedFromServer(NetworkDisconnection info) {

		isConnectedToServer = false;

		//-----
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
		else {
			Debug.Log("Successfully diconnected from the server");

			_messageLog += "Disconnected from server" + "\n";
			//Network.Destroy (playerPrefab.gameObject);
			
		}

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
		
		Sorcerer sorcerer = (Sorcerer)GameObject.FindObjectOfType (typeof (Sorcerer)); //sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // 
		sorcerer.transform.position = positionVector3;
		sorcererPositionWasUpdated = true;

		// if the sorcerer is around the origin, even if not exactly on it:
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

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

	
	// ================================================
//	[RPC]
//	public void onClientConnects() {
//		//if(networkView.isMine){
//		networkView.RPC("startDoCheckForNewSorcererNetworkInstantiatedByClient", RPCMode.All, "hello");//RPCMode.Others);
//		//print ("hello");
//		//}
//		SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient(); 
//	}
//
//	[RPC]
//	void startDoCheckForNewSorcererNetworkInstantiatedByClient(string s) {
//		print ("RECEIVED !!!!!! "+s);
//		SorcererInstanceManager.doCheckForNewSorcererNetworkInstantiatedByClient = true;
//		SorcererInstanceManager.checkForNewSorcererNetworkInstantiatedByClient(); 
//	}


	
}

