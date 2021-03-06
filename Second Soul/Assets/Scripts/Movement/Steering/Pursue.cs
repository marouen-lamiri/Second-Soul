﻿using UnityEngine;
using System.Collections;

public class Pursue : SteeringBehavior {

	Transform target;    
	Character character;
	
	void Start() {
		character = gameObject.GetComponent<Character> ();
		target = character.target.transform;
	}
	
	
	public override Vector3 Acceleration {
		get
		{
			target = character.target.transform;
			Vector3 futureTargetPosition;
			float distance = Vector3.Distance(target.position,transform.position);
			float time2Target = distance / gameObject.GetComponent<SteeringAgent>().Velocity.magnitude;
			futureTargetPosition = target.position + target.GetComponent<SteeringAgent>().Velocity*time2Target;
			if(time2Target == Mathf.Infinity || distance > 6){
				//return gameObject.GetComponent<Seek>().Acceleration;
				return MaxAcceleration * (target.position-transform.position).normalized;
			}
			return MaxAcceleration * (futureTargetPosition-transform.position).normalized;


		}
	}
}
