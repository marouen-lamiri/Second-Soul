using UnityEngine;
using System.Collections;

public class FireballBehavior : ProjectileBehavior {

	bool explode;
	float timeToDestroy;

	public FireballSkill fireballSkill;
	public Vector3 originalSpawn;
	Component[] fireballComponents;

	// Use this for initialization
	void Start () {
		originalSpawn = transform.position;
		explode = false;
		fireballComponents = this.GetComponentsInChildren<ParticleRenderer>();
		//fireballComponents [1].GetComponent<ParticleRenderer> ().enabled = false; 

		timeToDestroy = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(originalSpawn, transform.position) < fireballSkill.travelDistance && !explode){
			float oldY = transform.position.y;
			transform.position += Time.deltaTime * fireballSkill.speed * transform.forward;
			transform.position = new Vector3(transform.position.x, oldY, transform.position.z);
		}
		else{
			//fireballComponents [1].GetComponent<ParticleRenderer> ().enabled = true; // maybe network error
			//this.GetComponent<CharacterController>().radius=7; // maybe network error
			StartCoroutine(selfDestruct());
		}
	}

	void OnTriggerEnter(Collider obj){
		if (!explode) {
			obj.GetComponent<Enemy> ().takeDamage(fireballSkill.damage,fireballSkill.damageType);
			explode = true;
		}
		else {
			obj.GetComponent<Enemy> ().takeDamage(fireballSkill.AOEDamage,fireballSkill.damageType);
		}
	}

	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (timeToDestroy);
		Destroy (this.gameObject);
	}
}
