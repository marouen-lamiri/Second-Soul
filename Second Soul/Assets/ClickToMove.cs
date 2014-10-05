using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public float speed;
	public CharacterController controller;

	private Vector3 position;

	public AnimationClip run;
	public AnimationClip idle;

	public static bool attacking;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!attacking) {
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
			controller.SimpleMove (transform.forward * speed);

			animation.CrossFade(run.name);
		}
		//Player not moving
		else {
			animation.CrossFade(idle.name);
		}
	}
}
