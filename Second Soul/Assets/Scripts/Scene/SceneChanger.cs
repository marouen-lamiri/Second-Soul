using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string gameSceneToLoad;
	Fighter fighter;
	// Use this for initialization
	void Start () {
		fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		fighter.transform.position = new Vector3(75,fighter.transform.position.y,75);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Tab)){
			Loading.show ();
			NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1);
		}

	}
}
