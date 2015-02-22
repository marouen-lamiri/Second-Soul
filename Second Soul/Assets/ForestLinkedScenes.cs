using UnityEngine;
using System.Collections;

public class ForestLinkedScenes : MonoBehaviour {

	public Fighter fPosition;
	public Sorcerer sPosition;

	// Use this for initialization
	void Start () {
		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(){
		NetworkLevelLoader.Instance.LoadLevel("Town",1);
	}
}
