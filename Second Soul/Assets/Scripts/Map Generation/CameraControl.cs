using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float maxHeight;
	public float maxDistance;
	public float minDistance;
	public float heightDamping;
	public float scrollSpeed;
	float distance;
	float height;
	float idealRatio;
	float angle;
	Fighter fighter;
	Sorcerer sorcerer;
	// Use this for initialization
	void Start () {
		fighter = GameObject.FindObjectOfType<Fighter> ();
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer ();
		idealRatio = maxHeight / maxDistance;
		height = maxHeight;
		distance = maxDistance;
		angle = 45f;
	}
	
	// Update is called once per frame
	void Update () {
		float scrollChange = Input.GetAxis ("Mouse ScrollWheel")*scrollSpeed;
		if (scrollChange > 0) {//scroll forwards
			distance-=scrollChange;
		}
		else if (scrollChange < 0) {//scroll backwards
			distance-=scrollChange;
		}
		if (distance > maxDistance) {
			distance = maxDistance;
		}
		else if(distance < minDistance){
			distance = minDistance;
		}
		height = idealRatio * distance;

		Transform target = (fighter.playerEnabled) ? fighter.transform : sorcerer.transform;
		// Calculate the current rotation angles
		var wantedHeight = target.position.y + height;

		var currentHeight = transform.position.y;

		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// distance meters behind the target
		transform.position = target.position;

		transform.position -= Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		transform.RotateAround (target.position, new Vector3 (0, 1, 0), angle);
		// Always look at the target
		transform.LookAt (target);
	}
	//worth looking into eventually
	/*void OriginalSmoothFollow(){
		// Calculate the current rotation angles
		wantedRotationAngle = target.eulerAngles.y;
		wantedHeight = target.position.y + height;
		
		currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position.y = currentHeight;
		
		// Always look at the target
		transform.LookAt (target);
	}*/
}
