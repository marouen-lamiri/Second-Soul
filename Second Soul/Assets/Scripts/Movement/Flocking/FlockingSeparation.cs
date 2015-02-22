using UnityEngine;
using System.Collections;

public class FlockingSeparation : FlockingBehaviour {

	public int multiple;
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
				totalVector+= transform.position-agent.transform.position;
			}
			totalVector*=multiple;
			return MaxAcceleration * totalVector.normalized;
		}
	}
}
