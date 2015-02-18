using UnityEngine;
using System.Collections;

public class Wander : Seek
{
	public GameObject wanderingObject;

	private float timetoChange;
	public float distanceOfCircle;
	public float radiusOfCircle;
	public Vector3 positionOfCircle;
    void Start() {
		
		timetoChange = 0;
		wanderingObject = new GameObject ();
		wanderingObject.name = "Wandering Object";
		wanderingObject.transform.parent = GameObject.Find ("Wandering Objects").transform;
		target = wanderingObject.transform.position;
    }

	public void wanderUpdate(){
		target = wanderingObject.transform.position;
		timetoChange -= Time.fixedDeltaTime;
		if(timetoChange <= 0 ){
			timetoChange = 0.50f;
			positionOfCircle = transform.position + transform.forward * distanceOfCircle;
			float angle = UnityEngine.Random.Range (0, 360);
			float x = (radiusOfCircle * (float)System.Math.Cos (angle * Mathf.PI / 180f)) + positionOfCircle.x;
			float z = (radiusOfCircle * (float)System.Math.Sin (angle * Mathf.PI / 180f)) + positionOfCircle.z;
			
			target = new Vector3 (x, 0, z);

			bool obstacleFront = obstacleInWay(target, radiusOfCircle+2, transform.forward, distanceOfCircle+2);
			bool obstacleRight = obstacleInWay(target, radiusOfCircle+2, transform.right, distanceOfCircle+2);
			bool obstacleLeft = obstacleInWay(target, radiusOfCircle+2, transform.right * -1, distanceOfCircle+2);
			if(obstacleFront && obstacleRight){
				target = transform.right * -1 * distanceOfCircle;
			}
			else if(obstacleFront && obstacleLeft){
				target = transform.right * distanceOfCircle;
			}
			else if(obstacleFront){
				target = transform.right * distanceOfCircle;
			}
			wanderingObject.transform.position = target;
		}
	}

	public bool obstacleInWay(Vector3 position, float radius, Vector3 direction, float distance){
		RaycastHit hit;
		LayerMask unwalkableMask = GameObject.FindObjectOfType<Grid>().unwalkableMask;
		return Physics.SphereCast (position, radius, direction, out hit, distance, unwalkableMask);
	}
}
