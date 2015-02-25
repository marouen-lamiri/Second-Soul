using UnityEngine;
using System.Collections;

public class ForestLinkedScenes : MonoBehaviour {

	public Fighter fPosition;
	public Sorcerer sPosition;
	string playerTag = "Player";
	string sceneName = "Town";

	// Use this for initialization
	void Start () {
		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		fPosition.transform.position = new Vector3 (75, 0, 75);
		sPosition.transform.position = new Vector3 (72, 0, 72);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject col){
		if(col.gameObject.tag == playerTag){
			NetworkLevelLoader.Instance.LoadLevel(sceneName,1);
		}
	}
}
