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
	
	public void Awake() {
		
		networkWindowX = Screen.width - 500;
		networkWindowY = 10;
		networkWindowButtonWidth = 150;
		networkWindowButtonHeight = 25;
		//AddNetworkView();
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
				//Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0); // N.B. place the network game object exactly where you want to spawn players.
				
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
		Fighter fighterrrr = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Fighter; // N.B. place the network game object exactly where you want to spawn players.
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
}

