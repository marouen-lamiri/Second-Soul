using UnityEngine;
using System.Collections;

public class PathFinding : ClickToMove {

	public GameObject enemyPrefab;
	GameObject player;
	GameObject player2; 
	Vector3 newPosition;
	Vector3 currentPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Fighter");
		player2 = GameObject.Find("Sorcerer"); 
	}
	
	// Update is called once per frame
	void Update () {
		checkTrajectory(player);
	}

	void checkTrajectory(GameObject player){
		currentPosition = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
		if (player.gameObject.tag != "Enemy") {
			newPosition = returnPosition ();
		} else {
			Debug.Log ("it didn't go!");
		}
//		Debug.Log ("this is the current position: "+ currentPosition);
		Debug.Log ("this is the new position: "+ newPosition);
	}
}
