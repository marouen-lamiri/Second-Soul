using UnityEngine;
using System.Collections;

public class Align : MonoBehaviour
{
	public float maxMagnitude;
	public float maxRadians;
    // Use this for initialization
    void Start() {
    }

	public void interpolatedChangeInOrientation(Vector3 targetOrientation){
		//	I see no reason to limit maxMagnitude and maxRadians, and no overlaod exists that excludes it,
		//so I am trying out mathf.infinity. If we have a reason to limit it (performance etc), we can switch out for the commented line
		//	Vector3 newDir = Vector3.RotateTowards(transform.forward,targetOrientation,maxRadians,maxMagnitude);
		Vector3 newDir = Vector3.RotateTowards(transform.forward,targetOrientation,Mathf.Infinity,Mathf.Infinity);
		newDir.y = 0;
		if(newDir.x !=0 || newDir.z != 0) 
			transform.rotation = Quaternion.LookRotation (newDir);
	}
}
