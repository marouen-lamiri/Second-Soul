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
		AddNetworkView();
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
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUI.Button(new Rect(100, 100, 150, 25), "Connect")) {
				ConnectToServer();
			}
		} else {
			if (Network.peerType == NetworkPeerType.Client) {
				GUI.Label(new Rect(100, 100, 150, 25), "client");
				
				if (GUI.Button(new Rect(100, 125, 150, 25), "Logut"))
					Network.Disconnect();
				
				if (GUI.Button(new Rect(100, 150, 150, 25), "SendHello to server")) {
					someInfo = "hello server!";
					SendInfoToServer(someInfo);
				}
			}
		}
		
		GUI.TextArea(new Rect(250, 100, 300, 300), _messageLog);
	}

	public void ConnectToServer() {
		Network.Connect(serverIP, port);
	}
	
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
		_messageLog += "Disco from server" + "\n";
		isConnectedToServer = false;
	}
	
	// fix RPC errors
	[RPC]
	void ReceiveInfoFromClient(string someInfo) { }
	[RPC]
	void SendInfoToClient(string someInfo) { }
}