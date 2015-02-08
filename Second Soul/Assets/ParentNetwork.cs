using UnityEngine;
using System.Collections;

public class ParentNetwork : MonoBehaviour {

	public static string serverIP = "127.0.0.1";
	public static string serverLocalIP;
	public static int port = 25000;
	protected static string _messageLog = "";
	protected static string someInfo = "";
	protected static NetworkPlayer _myNetworkPlayer;

	protected static bool bothPlayerAndSorcererWereFound;

	protected static int networkWindowX;
	protected static int networkWindowY;
	protected static int networkWindowButtonWidth;
	protected static int networkWindowButtonHeight;

	protected static bool displayChat;

	protected static ChooseClass classChooser;

	protected static GameObject lines;
	
	public Fighter playerPrefab;
	public static Sorcerer sorcererPrefab;
	protected static bool sorcererWasCreated;


	public void Awake () {

		bothPlayerAndSorcererWereFound = false;

		// parent class:
		networkWindowX = Screen.width - 500;
		networkWindowY = 10;
		networkWindowButtonWidth = 150;
		networkWindowButtonHeight = 25;
		//AddNetworkView();

		displayChat = true;

		sorcererWasCreated = false;

		classChooser = (ChooseClass)GameObject.FindObjectOfType(typeof(ChooseClass));

		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// client and server --> toggle display chat window:
		if(Input.GetKeyDown ("n")){
			displayChat = !displayChat;
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
