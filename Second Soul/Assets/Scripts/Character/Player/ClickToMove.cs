using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour {

	public Character player;
	private Vector3 position;
	private List<Vector3> nextPositions;
	private float timer = 0f; //checks if the player input has been put very recently
	private Grid grid;
	private PathFinding pathing;
	bool initial = false;

	public static bool busy;

	void awake(){
		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));
	}

	// Use this for initialization
	void Start () {
		position = transform.position;
		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));
	}
	
	// Update is called once per frame
	void Update (){
//		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
//		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));
//		timer -= Time.deltaTime;
//		if(!player.isDead() && player.playerEnabled && !busy){
//			if(!player.chasing && !player.actionLocked()){
//				if(Input.GetMouseButton(0)&& timer <= 0)
//				{
//					timer = 1f;
//					//Locate where the player clicked on the terrain
//					locatePosition();
//				}
//				if(initial){
//			 		//Move the player to the position
//					moveToPosition();
//				}
//			}
//		}
//		else
//		{
//		}
	}

	public void move () {
//		//Locate player click position
//		locatePosition ();
//		//move player to position
//		moveToPosition ();
	}

	public void locatePosition(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		initial = true;
		hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);
		
		if(hits != null){
			RaycastHit hit = hits[0];
			if(hit.collider.tag != "Player" /*&& hit.collider.tag != "Enemy"*/ && hit.collider.tag != "GUI"){
				//position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
				position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			}
			//Debug.Log(position);
		}

	}

	void moveToPosition(){
		//Player moving
		if (Vector3.Distance (transform.position, position) > 1) {
			pathing.findPath(transform.position, position);
			List<Vector3> path = grid.worldFromNode(grid.path);
			Vector3 destination;
			if (Vector3.Distance (transform.position, position) > 1) {
				destination = path[1];
			} 
			else {
				destination = position;
			}
			Quaternion newRotation = Quaternion.LookRotation (destination - transform.position);
			newRotation.x = 0;
			newRotation.z = 0;
			
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 7);
			player.controller.SimpleMove (transform.forward * player.speed);

			player.animateRun();

			// networking: event listener to RPC the attack anim
//			Fighter fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			CharacterNetworkScript playerNetworkScript = player.GetComponent<CharacterNetworkScript>();
			if(playerNetworkScript != null) {
				playerNetworkScript.onRunTriggered();
			} else {
				print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
			}

		}
		//Player not moving
		else {
			player.animateIdle();

			// networking: event listener to RPC the attack anim
//			Fighter fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			CharacterNetworkScript playerNetworkScript = player.GetComponent<CharacterNetworkScript>();
			if(playerNetworkScript != null) {
				playerNetworkScript.onIdleTriggered();
			} else {
				print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
			}

		}
	}
}
