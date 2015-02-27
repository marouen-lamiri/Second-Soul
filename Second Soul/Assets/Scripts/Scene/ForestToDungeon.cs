using UnityEngine;
using System.Collections;

public class ForestToDungeon : MonoBehaviour {

	Fighter fPosition;
	Sorcerer sPosition;
	string playerTag = "Player";
	string sceneName = "SkillTree";
	
	// Use this for initialization
	void Start () {
		//fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		//sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
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
