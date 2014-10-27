﻿using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public Fighter player;

	private Vector3 position;

	public static bool attacking;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!attacking && player.health > 0) {
			if (Input.GetMouseButton (0)) {
					//Locate player click position
					locatePosition ();
			}

			//move player to position
			moveToPosition ();
		} 
		else {

		}
	}

	void locatePosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if(Physics.Raycast(ray, out hit, 1000)){
			if(hit.collider.tag != "Player" && hit.collider.tag != "Enemy"){
				position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			}
			//Debug.Log(position);
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
}
