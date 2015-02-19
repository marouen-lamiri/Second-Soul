using UnityEngine;
using System.Collections;

public class FlockingCohesion : FlockingBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override Vector3 Acceleration
	{
		get
		{
			getNeighbours();
			Vector3 totalVector = new Vector3();
			foreach(SteeringAgent agent in neighbours){
				totalVector+= agent.transform.position-transform.position;
			}
			return MaxAcceleration * totalVector.normalized;
		}
	}
}
