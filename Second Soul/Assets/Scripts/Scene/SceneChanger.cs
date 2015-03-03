using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string gameSceneToLoad;
	Fighter fighter;
	float randomPosition = 75;
	// Use this for initialization
	void Start () {
		fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		fighter.transform.position = new Vector3(randomPosition,fighter.transform.position.y,randomPosition);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Tab)){
			Loading.show ();
			NetworkLevelLoader.Instance.LoadLevel(gameSceneToLoad,1);
		}

	}
}
