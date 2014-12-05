using UnityEngine;
using System.Collections;

public class NavClickToMove : MonoBehaviour {
	
	public Character player;
	private Vector3 position;
	
	public static bool busy;
	
	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		if(!player.isDead() && player.playerEnabled)
		{
			if(!player.chasing && !player.actionLocked()){
				if(Input.GetMouseButton(0) && !busy)
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
	
	void locatePosition(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);
		
		if(hits != null){
			RaycastHit hit = hits[0];
			if(hit.collider.tag != "Player" && hit.collider.tag != "Enemy" && hit.collider.tag != "GUI"){
				position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			}
			//Debug.Log(position);
		}
	}
	
	void moveToPosition(){
		//Player moving
		if (Vector3.Distance (transform.position, position) > 1) {
			player.meshAgent.SetDestination(position);
			/*Quaternion newRotation = Quaternion.LookRotation (position - transform.position);
			
			newRotation.x = 0;
			newRotation.z = 0;
			
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 7);
			player.controller.SimpleMove (transform.forward * player.speed);*/
			
			player.animateRun();
		}
		//Player not moving
		else {
			position = transform.position;
			player.animateIdle();
		}
	}
}
