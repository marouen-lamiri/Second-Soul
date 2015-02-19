using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingBehaviour : SteeringBehavior {

	protected Transform target;
	public float radius;
	public List<SteeringAgent> neighbours;



	public void getNeighbours(){
		List<SteeringAgent> agents = new List<SteeringAgent>();
		Collider[] hits = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider hit in hits) {
			SteeringAgent agent = hit.gameObject.GetComponent<SteeringAgent>();
			if(agent!=null){
				if(agent!=null)
					agents.Add (agent);
			}
		}
		neighbours = agents;
	}
}
