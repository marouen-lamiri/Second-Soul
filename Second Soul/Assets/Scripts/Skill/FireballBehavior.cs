using UnityEngine;
using System.Collections;

public class FireballBehavior : ProjectileBehavior {

	public FireballSkill fireballSkill;
	public Vector3 originalSpawn;
	// Use this for initialization
	void Start () {
		originalSpawn = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//if(collision)
		if(Vector3.Distance(originalSpawn, transform.position) < fireballSkill.travelDistance){
			transform.position += Time.deltaTime * fireballSkill.speed * transform.forward;
		}
		else{
			//explode
		}
	}
}
