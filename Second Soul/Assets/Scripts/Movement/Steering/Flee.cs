using UnityEngine;
using System.Collections;
using System;

public class Flee : SteeringBehavior
{
	Vector3 pursuer;

    void Start()
    {
		pursuer = gameObject.GetComponent<Character> ().goalPosition;
    }

    public override Vector3 Acceleration
    {
		get
		{
			pursuer = gameObject.GetComponent<Character> ().goalPosition;
			return MaxAcceleration * (transform.position-pursuer).normalized;
		}
    }
}
