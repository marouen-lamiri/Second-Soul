using UnityEngine;
using System.Collections;

public class ServerNetwork : MonoBehaviour {

	public string serverIP = "127.0.0.1";
	private int port = 25000;
	private int playerCount = 0;
	private string _messageLog = "";
	string someInfo = "";
	private NetworkPlayer _myNetworkPlayer;

	
	public void Awake() {

		//
		// AddNetworkView();
		//Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0); // error prefab not isntantiated.
		
	}
	
	[RPC]
	private void AddNetworkView() {
		//		gameObject.AddComponent<NetworkView>();
		//		gameObject.networkView.observed = this;
		//		gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		//		gameObject.networkView.viewID = Network.AllocateViewID();
	}


	// Immediately instantiate new connected player's character
	// when successfully connected to the server.
	//public Transform playerPrefab;
	//moved code 
	
	
	public void Update() {
		//code from: http://www.palladiumgames.net/tutorials/unity-networking-tutorial/ 
		if (Network.isServer)
		{
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			float speed = 5;
			transform.Translate(speed * moveDir * Time.deltaTime);
		}
	}
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
		

		// button to connect as a client:
		if(GUI.Button(new Rect(100, 400, 150, 25), "Connect as a client")) {
			//			if (Network.peerType == NetworkPeerType.Disconnected) {
			//				if (GUI.Button(new Rect(100, 100, 150, 25), "Connect")) {
			ConnectToServer();
			//				}
			//			}
		}
		
		// after connecting, if you're a client:
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

	private void ConnectToServer() {
		Network.Connect(serverIP, port);
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		AskClientForInfo(player);
		//print ();
		SendInfoToClient ("Received Info from Client");
	}
	
	void AskClientForInfo(NetworkPlayer player) {
		networkView.RPC("SetPlayerInfo", player, player);
	}
	
	[RPC]
	void ReceiveInfoFromClient(string someInfo) {
		_messageLog += someInfo + "\n";
	}
	
	//string someInfo = "Server: hello client";
	[RPC]
	void SendInfoToClient(string msg) {
		msg = "SERVER: " + msg;
		networkView.RPC("ReceiveInfoFromServer", RPCMode.Others, msg);
		print (msg);
		_messageLog += msg + "\n";
	}

	[RPC]
	void SendInfoToServer(string msg){
		msg = "CLIENT " + _myNetworkPlayer.guid + ": " + msg;
		_messageLog += msg + "\n";
		//someInfo = "Client " + _myNetworkPlayer.guid + ": hello server";
		networkView.RPC("ReceiveInfoFromClient", RPCMode.Server, msg);
	}

	
	// fix RPC errors
	//[RPC]
	//void SendInfoToServer() { }
//	[RPC]
//	void SetPlayerInfo(NetworkPlayer player) { }

	[RPC]
	void SetPlayerInfo(NetworkPlayer player) {
		_myNetworkPlayer = player;
		someInfo = "Player setted";
		networkView.RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	void ReceiveInfoFromServer(string someInfo) { }

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