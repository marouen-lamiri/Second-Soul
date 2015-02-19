using UnityEngine;
using System.Collections;
using System;

public class Flee : SteeringBehavior
{
	Vector3 pursuer;
	Character character;

    void Start()
    {
		character = gameObject.GetComponent<Character> ();
    }

    public override Vector3 Acceleration
    {
		get
		{
			pursuer = character.goalPosition;
			return MaxAcceleration * (transform.position-pursuer).normalized;
		}
    }
}
