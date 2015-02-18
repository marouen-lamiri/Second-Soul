using UnityEngine;
using System.Collections;

public class FlockingAlignment : FlockingBehaviour {

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
			foreach(FlockingAgent agent in neighbours){
				totalVector+= agent.Velocity;
			}
			totalVector/=neighbours.Count;
			return MaxAcceleration * totalVector.normalized;
		}
	}
}
