using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string gameSceneToLoad;
	string townScene = "Town";
	string forestScene = "Forest";
	Fighter fighter;
	float randomPositionTown = 75;
	float randomPositionForestX = 93;
	float randomPositionForestZ = 57;

	// Use this for initialization
	void Start () {
		fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		if(Application.loadedLevelName == townScene){
			fighter.transform.position = new Vector3(randomPositionTown,fighter.transform.position.y,randomPositionTown);
		}
		else if(Application.loadedLevelName == forestScene){
			fighter.transform.position = new Vector3(randomPositionForestX,fighter.transform.position.y,randomPositionForestZ);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Tab)){
			Loading.show ();
			NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1);
		}

	}
}
