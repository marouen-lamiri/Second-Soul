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
		//transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		originalSpawn = transform.position;
		explode = false;
//		fireballComponents = this.GetComponentsInChildren<ParticleRenderer>();
//		fireballComponents [1].GetComponent<ParticleRenderer> ().enabled = false; 

		timeToDestroy = 10f;

		//fix:
		Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType(typeof(Sorcerer)) as Sorcerer;
		this.fireballSkill = sorcerer.GetComponent<FireballSkill>();
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
//			this.GetComponent<CharacterController>().radius=7; // maybe network error
			StartCoroutine(selfDestruct());
		}
	}

	void OnParticleCollision(GameObject obj){
		Debug.Log ("hadouken!");
	}

	void OnParticleCollision(Character obj){
		Debug.Log ("hadouken!222222");
	}

	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (timeToDestroy);
//		Network.Destroy (gameObject);
		//fireball network stuff has to be resolved eventually, but it does not cause any problems
		Destroy (gameObject);
	}
}
