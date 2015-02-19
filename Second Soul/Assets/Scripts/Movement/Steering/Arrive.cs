using UnityEngine;
using System.Collections;
using System;

public class Arrive : SteeringBehavior
{
	public Vector3 target;
    public float slowRadius;
    public float arriveRadius;
	protected SteeringAgent steeringScript;

    void Start() {
		steeringScript = gameObject.GetComponent<SteeringAgent> ();
		target = steeringScript.targetPosition;
    }
	
    public override Vector3 Acceleration {
         get
        {
			target = steeringScript.targetPosition;

			if(Vector3.Distance(transform.position,target)<slowRadius){
				Vector3 accel = MaxAcceleration * (transform.position-target).normalized;
					return accel;
			}
			else
				return Vector3.zero;
        }
    }

    public override bool HaltTranslation {
        get
        {
			if(Vector3.Distance(transform.position,target)<arriveRadius)
				return true;
			else
				return false;
        }
    }
}
