using UnityEngine;
using System.Collections;

public class ForestLinkedScenes : MonoBehaviour, ISorcererSubscriber {

	public Fighter fPosition;
	public Sorcerer sPosition;
	Vector3 fInitial = new Vector3 (75, 0, 75);
	Vector3 sInitial = new Vector3 (72, 0, 72);
	string playerTag = "Player";
	string sceneName = "Town";

	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		fPosition.transform.position = fInitial;
		sPosition.transform.position = sInitial;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject col){
		if(col.gameObject.tag == playerTag){
			NetworkLevelLoader.Instance.LoadLevel(sceneName,1);
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
