using UnityEngine;
using System.Collections;

public class ForestLinkedScenes : SceneManager, ISorcererSubscriber {

	public GUIStyle buttons;
	protected Fighter fPosition;
	protected Sorcerer sPosition;
	protected Vector3 fInitial = new Vector3 (75, 0, 75);
	protected Vector3 sInitial = new Vector3 (72, 0, 72);
	protected string playerTag = "Player";
	protected string sceneName = "Town";
	protected string teleporter = "Teleporter";

	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		fPosition.transform.position = fInitial;
		sPosition.transform.position = sInitial;
	}
	
	// Update is called once per frame
	void OnGUI () {
		clicked ();
		if(showMenu){
			menu ();
		}
	}
	
	void clicked(){
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distance)){
				if (hit.transform.name == teleporter){
					showMenu = true;
				}
			}
		}
	}
	
	void menu(){
		GUIStyle centeredStyle = GUI.skin.GetStyle("textarea");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = sizeFont;
		GUI.Box (new Rect (Screen.width/3 - Screen.width/36, Screen.height/3 + Screen.height/32, Screen.width/3  + Screen.width/22, Screen.height/3), greeting, centeredStyle);
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + Screen.height/12, Screen.width/3, Screen.height/12), okMessage, buttons)){
			NetworkLevelLoader.Instance.LoadLevel(sceneName,1);
		}
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 2 * Screen.height/12, Screen.width/3, Screen.height/12), noMessage, buttons)){
			showMenu = !showMenu;
		}
		if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 3 * Screen.height/12, Screen.width/3, Screen.height/12), closeMessage, buttons)){
			showMenu = !showMenu;
		}
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sPosition = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

}
