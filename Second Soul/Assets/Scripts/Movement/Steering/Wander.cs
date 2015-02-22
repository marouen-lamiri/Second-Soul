using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wander : MonoBehaviour
{
	public GameObject wanderingObject;
	
	private float timetoChange;
	public float distanceOfCircle;
	public float radiusOfCircle;
	public Vector3 positionOfCircle;

	int maxNeighbourRadius;
    void Start() {
		timetoChange = 0;
		wanderingObject = new GameObject ();
		wanderingObject.name = "Wandering Object";
		wanderingObject.transform.parent = GameObject.Find ("Wandering Objects").transform;
		wanderingObject.transform.position = transform.position;

		maxNeighbourRadius = 5;
    }

	public void wanderForward(){
		Vector3 target = Vector3.zero;
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

	public void wanderInCircle(){
		timetoChange -= Time.fixedDeltaTime;
		if (timetoChange <= 0) {
			timetoChange = Random.Range (2f, 5f);
			List<SteeringAgent> neighbours = new List<SteeringAgent>();

			Collider[] hits = Physics.OverlapSphere (transform.position, maxNeighbourRadius);
			foreach (Collider hit in hits) {
				SteeringAgent agent = hit.gameObject.GetComponent<SteeringAgent>();
				if(agent!=null){
					if(agent!=null)
						neighbours.Add (agent);
				}
			}
			if(neighbours.Count == 0){
				wanderingObject.transform.position = transform.position;
				return;
			}
			Vector3 totalVector = new Vector3();
			foreach(SteeringAgent agent in neighbours){
				totalVector+= agent.transform.position;
			}
			totalVector/=neighbours.Count;
			float maxRadius = 0f;
			foreach(SteeringAgent agent in neighbours){
				float radius = Vector3.Distance(totalVector,agent.transform.position);
				if(Vector3.Distance(totalVector,agent.transform.position)>maxRadius){
					maxRadius = radius;
				}
			}
			Vector2 offset = Random.insideUnitCircle*maxNeighbourRadius;
			Vector3 wanderToPosition = totalVector + new Vector3(offset.x, 0, offset.y);
			wanderingObject.transform.position = wanderToPosition;
		}
	}

	public bool obstacleInWay(Vector3 position, float radius, Vector3 direction, float distance){
		RaycastHit hit;
		LayerMask unwalkableMask = GameObject.FindObjectOfType<Grid>().unwalkableMask;
		return Physics.SphereCast (position, radius, direction, out hit, distance, unwalkableMask);
	}
}
