using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour {

	public Character player;
	private Vector3 position;
	private Vector3 midPosition;
	private float timer = 0f; //checks if the player input has been put very recently
	GameObject[] path = new GameObject[20];
	Node node;

	public static bool busy;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		timer -= Time.deltaTime;
		if(!player.isDead() && player.playerEnabled && !busy)
		{
			if(!player.chasing && !player.actionLocked()){
				if(Input.GetMouseButton(0)&& timer <= 0)
				{
					timer = 0.5f;
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
			Debug.DrawLine(transform.position, position, Color.red, 50f);
//			Debug.Log ("Initial Position: "+ transform.position);
//			Debug.Log ("Ending Position: "+ position);
			checkTrajectory(ray, hit, position);
		}
	}

	public void checkTrajectory(Ray ray, RaycastHit hit, Vector3 position){
		if(Physics.Raycast(ray, out hit, 1000)){
			if (hit.collider.tag == "Obstacle") {
				//position = findClosest(position).transform.position;
				//findPath(transform.position, position, hit, ray);
				Debug.Log("Came here!");
			}
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

//	GameObject findClosest(Vector3 nextPosition, int iteration, RaycastHit hit, Ray ray){
//		GameObject[] Nodes;
//		Nodes = GameObject.FindGameObjectsWithTag("Node"); // an array of every node in the game
//		GameObject player = GameObject.Find("Fighter");
//		GameObject closest = GameObject.Find("Node");
//		float distance = Mathf.Infinity;
//		Vector3 position = player.transform.position;
//		foreach (GameObject Node in Nodes) {
//			Vector3 diff = Node.transform.position - position;
//			float curDistance = diff.sqrMagnitude;
//			if((curDistance - distance < 5 || distance - curDistance < 5) && arrayChecker (path, Node)){
//				if (curDistance < distance) {
//					closest = Node;
//					path[iteration]= Node;
//					distance = curDistance;
//					//Debug.Log ("Current distance between node and position is: " + curDistance);
//				}
//			}
//			Vector3 diff2 = nextPosition - position;
//			float curDistance2 = diff2.sqrMagnitude;
//			if (curDistance2 < curDistance) {
//				path[iteration]= null;
//				closest = null;
//				distance = curDistance2;
//			}
//		}
//		return closest;
//	}
//
//	bool arrayChecker (GameObject[] array, GameObject node){
//		for (int i = 0; i<20; i++) {
//			if(array[i] == node){
//				return false;
//			}
//		}
//		return true;
//	}
//
//	void findPath(Vector3 sPosition, Vector3 ePosition, RaycastHit hit,Ray ray){
//		Vector3[] listOfPosition = new Vector3[11];
//		int i = 0;
//		GameObject previous = null;
//		while(i<10 && findClosest (ePosition, i, hit, ray) != null ){
//			previous =  findClosest (ePosition, i, hit, ray);
//			midPosition = previous.transform.position;
//			listOfPosition[i] = midPosition;
//			Debug.Log ("midpoint Position: "+ listOfPosition[i]);
//			i++;
//		}
//	}
}
