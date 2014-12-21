using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public Character player;
	private Vector3 position;

	public static bool busy;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		if(!player.isDead() && player.playerEnabled && !busy)
		{
			if(!player.chasing && !player.actionLocked()){
				if(Input.GetMouseButton(0))
				{
					//Locate where the player clicked on the terrain
					locatePosition();
				}
			 	//Move the player to the position
				moveToPosition();
			}
		}
		else
		{
		}
	}

	public void move () {
		//Locate player click position
		locatePosition ();
		//move player to position
		moveToPosition ();
	}

	void locatePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 1000)){
			if(hit.collider.tag != "Player" && hit.collider.tag != "Enemy"){
				position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			}
			checkTrajectory(hit, position);
		}
	}

	public void checkTrajectory(RaycastHit hit, Vector3 position){
		if (hit.collider.tag == "Obstacle") {
			findClosest(position);
		}
	}

	void moveToPosition(){
		//Player moving
		if (Vector3.Distance (transform.position, position) > 1) {
			Quaternion newRotation = Quaternion.LookRotation (position - transform.position);

			newRotation.x = 0;
			newRotation.z = 0;

			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 7);
			player.controller.SimpleMove (transform.forward * player.speed);

			player.animateRun();
		}
		//Player not moving
		else {
			player.animateIdle();
		}
	}

	GameObject findClosest(Vector3 nextPosition){
		GameObject[] Nodes;
		Nodes = GameObject.FindGameObjectsWithTag("Node"); // an array of every node in the game
		GameObject player = GameObject.Find("Fighter");
		GameObject closest = GameObject.Find("Node");
		float distance = Mathf.Infinity;
		Vector3 position = player.transform.position;
		foreach (GameObject Node in Nodes) {
			Vector3 diff = Node.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance - distance < 2 || distance - curDistance < 2){
				if (curDistance < distance) {
					closest = Node;
					distance = curDistance;
				}
			}
		}
		Vector3 diff2 = nextPosition - position;
		float curDistance2 = diff2.sqrMagnitude;
		if(curDistance2 - distance < 2 || distance - curDistance2 < 2){
			if (curDistance2 < distance) {
				closest = null;
				distance = curDistance2;
			}
		}
		return closest;
	}
}
