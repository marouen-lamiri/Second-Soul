﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour {

	public Character player;
	private Vector3 position;
	private Vector3 nextPosition;
	private float timer = 0f; //checks if the player input has been put very recently
	public Grid grid;
	public PathFinding pathing;

	public static bool busy;

	void awake(){
		grid = GetComponent<Grid>();
		pathing = GetComponent<PathFinding>();
	}

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
			pathing.findPath(transform.position, position);
		}
	}

	public Vector3 CheckTrajectory(){
		return nextPosition;
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
}
