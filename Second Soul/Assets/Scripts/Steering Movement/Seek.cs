using UnityEngine;
using System.Collections;
using System;

public class Seek : SteeringBehavior
{
    public Vector3 target;    

    void Start() {
		target = gameObject.GetComponent<SteeringAgent> ().targetPosition;
    }


    public override Vector3 Acceleration {
        get
        {
			target = gameObject.GetComponent<SteeringAgent> ().targetPosition;
			return MaxAcceleration * (target-transform.position).normalized;
        }
    }
}
