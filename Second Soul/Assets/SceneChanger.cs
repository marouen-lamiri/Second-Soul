using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string gameSceneToLoad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Tab)){
			Loading.show ();
			NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1);
		}

	}
}
