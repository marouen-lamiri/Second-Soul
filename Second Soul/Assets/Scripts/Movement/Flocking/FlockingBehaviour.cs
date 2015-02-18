using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingBehaviour : MonoBehaviour {

	protected Transform target;
	public float MaxAcceleration;
	public float radius;
	public List<FlockingAgent> neighbours;

	public virtual Vector3 Acceleration
	{
		get
		{
			return Vector3.zero;
		}
	}
	
	public virtual float AngularAcceleration
	{
		get
		{
			return 0f;
		}
	}
	
	public virtual bool HaltTranslation
	{
		get
		{
			return false;
		}
	}
	
	public virtual bool HaltRotation
	{
		get
		{
			return false;
		}
	}

	public void getNeighbours(){
		List<FlockingAgent> agents = new List<FlockingAgent>();
		Collider[] hits = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider hit in hits) {
			FlockingAgent agent = hit.gameObject.GetComponent<FlockingAgent>();
			if(agent!=null && agent.GetType() == (typeof(FlockingAgent))){
				if(agent!=null)
					agents.Add (agent);
			}
		}
		neighbours = agents;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
