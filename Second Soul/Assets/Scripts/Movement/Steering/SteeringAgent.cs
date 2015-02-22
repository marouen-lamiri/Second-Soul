using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class SteeringAgent : MonoBehaviour
{
    float maxVelocity;

    public Vector3 Velocity { get; private set; }
	Character character;
	Align alignScript;
	SteeringBehavior [] behaviours;
	public Vector3 targetPosition;
    void Start(){
		character = GetComponent<Character> ();
		alignScript = GetComponent<Align> ();
		behaviours = gameObject.GetComponents<SteeringBehavior> ();
		ResetVelocities ();
    }
    
	public void steeringUpdate(){
		maxVelocity = character.speed;
        UpdateVelocities(Time.deltaTime);
        UpdatePosition(Time.deltaTime);
        UpdateRotation(Time.deltaTime);
    }

    public void ResetVelocities(){
        Velocity = Vector3.zero;
    }

    private void UpdateVelocities(float deltaTime){		
	//	if (hault)
			//return;

		for (int i=0; i<behaviours.Length; ++i) {
			if(behaviours[i].enabled == false)
				continue;
			if(behaviours[i].Acceleration == Vector3.zero){
				Velocity += Vector3.zero;
				continue;
			}
			else
				Velocity += behaviours[i].Acceleration*Time.fixedDeltaTime;
			if(behaviours[i].HaltTranslation){
				Velocity = Vector3.zero;
				return;
			}
		}
		Velocity = Vector3.ClampMagnitude (Velocity, maxVelocity);
    }

    private void UpdatePosition(float deltaTime){
		transform.position += Velocity * Time.fixedDeltaTime;
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
    }

    private void UpdateRotation(float deltaTime){
		//we may switch to this method soon
		//alignScript.interpolatedChangeInOrientation (Velocity);
    }

	public void setTarget(Vector3 position){
		targetPosition = position;
	}
}
