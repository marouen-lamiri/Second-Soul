using UnityEngine;
using System.Collections;

public class ClientNetwork : MonoBehaviour {
	
	public string serverIP = "127.0.0.1";
	public int port = 25000;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;

	public bool isConnectedToServer = false;
	
	public void Awake() {
		//AddNetworkView();
	} 
	
	[RPC]
	private void AddNetworkView() {
		// this is to dynamically add a networkview at runtime, can be useful for example if creating players or other game objects at runtime.
		
		//		gameObject.AddComponent<NetworkView>();
		//		gameObject.networkView.observed = this;
		//		gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		//		gameObject.networkView.viewID = Network.AllocateViewID();
	}
	
	// Immediately instantiate new connected player's character
	// when successfully connected to the server.
	//public Transform playerPrefab;
	//code move to already existing OnConnectedToServer function
	
	void OnGUI() {

		
		// button to connect as server:
		if(GUI.Button(new Rect(100, 300, 150, 25), "Connect as a server")) {
			
			// connect:
			if (Network.peerType == NetworkPeerType.Disconnected)
				Network.InitializeServer(10, port, false);
			
		}
		
		// after connecting: if you're a server:
		if (Network.peerType == NetworkPeerType.Server) {
			GUI.Label(new Rect(100, 100, 150, 25), "Server");
			GUI.Label(new Rect(100, 125, 150, 25), "Clients attached: " + Network.connections.Length);
			
			if (GUI.Button(new Rect(100, 150, 150, 25), "Quit server")) {
				Network.Disconnect(); 
				Application.Quit();
			}
			if (GUI.Button(new Rect(100, 175, 150, 25), "Send hi to client"))
				SendInfoToClient("Hello client!");
			
			
			GUI.TextArea(new Rect(275, 100, 300, 300), _messageLog);
			
			// that's good for both: 
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				//GUI.Label(new Rect(10, 10, 200, 20), "Status: Disconnected");
				print ("Status: Disconnected.");
			}
			
		}

		// =========================

		// that's for the client code:
		// button to connect as a client:
		if(GUI.Button(new Rect(100, 400, 150, 25), "Connect as a client")) {
//			if (Network.peerType == NetworkPeerType.Disconnected) {
//				if (GUI.Button(new Rect(100, 100, 150, 25), "Connect")) {
					ConnectToServer();
//				}
//			}
		}

		// after connecting if you're a client:
		if (Network.peerType == NetworkPeerType.Client) {
			GUI.Label(new Rect(100, 100, 150, 25), "client");
			
			if (GUI.Button(new Rect(100, 125, 150, 25), "Logout"))
				Network.Disconnect();
			
			if (GUI.Button(new Rect(100, 150, 150, 25), "SendHello to server")) {
				someInfo = "hello server!";
				SendInfoToServer(someInfo);
			}

			GUI.TextArea(new Rect(250, 100, 300, 300), _messageLog);

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
		//Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
		isConnectedToServer = true;
	}
	void OnDisconnectedToServer() {
		_messageLog += "Disconnected from server" + "\n";
		isConnectedToServer = false;
	}
	
	// fix RPC errors
//	[RPC]
//	void ReceiveInfoFromClient(string someInfo) { }
//	[RPC]
//	void SendInfoToClient(string someInfo) { } 
	[RPC]
	void SendInfoToClient(string msg) {
		msg = "SERVER: " + msg;
		networkView.RPC("ReceiveInfoFromServer", RPCMode.Others, msg);
		print (msg);
		_messageLog += msg + "\n";
	}


	// use networkView.isMine 

	// this is spectating code, can go in both server and client cube/sphere code:
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting) {
			Vector3 pos = transform.position;
			stream.Serialize (ref pos);
		}
		else {
			Vector3 receivedPosition = Vector3.zero;
			stream.Serialize(ref receivedPosition);
			transform.position = receivedPosition;
		}
	}
}