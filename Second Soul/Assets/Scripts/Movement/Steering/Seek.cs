using UnityEngine;
using System.Collections;
using System;

public class Seek : SteeringBehavior
{
    public Vector3 target;    
	protected SteeringAgent steeringScript;
    void Start() {
		steeringScript = gameObject.GetComponent<SteeringAgent> ();
		target = steeringScript.targetPosition;
    }


    public override Vector3 Acceleration {
        get
        {
			target = steeringScript.targetPosition;
			return MaxAcceleration * (target-transform.position).normalized;
        }
    }
}
