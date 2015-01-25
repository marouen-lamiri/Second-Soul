using UnityEngine;
using System.Collections;

public class CycloneBehavior : ProjectileBehavior {

	bool explode;
	float timeToDestroy;
	
	public CycloneSkill cycloneSkill;
	public Vector3 originalSpawn;
	Component[] cycloneComponents;
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		originalSpawn = transform.position;
		explode = false; 
		
		timeToDestroy = 10f;

		Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType(typeof(Sorcerer)) as Sorcerer;
		this.cycloneSkill = sorcerer.GetComponent<CycloneSkill>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(originalSpawn, transform.position) < cycloneSkill.travelDistance && !explode){
			float oldY = transform.position.y;
			transform.position += Time.deltaTime * cycloneSkill.speed * transform.forward;
			transform.position = new Vector3(transform.position.x, oldY, transform.position.z);
		}
		else{
			StartCoroutine(selfDestruct());
		}
	}
	
	void OnTriggerEnter(Collider obj){
		if (!explode) {
			obj.GetComponent<Enemy> ().takeDamage(cycloneSkill.damage,cycloneSkill.damageType);
			explode = true;
		}
		else {
			obj.GetComponent<Enemy> ().takeDamage(cycloneSkill.AOEDamage,cycloneSkill.damageType);
		}
	}
	
	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (timeToDestroy);
		//fireball network stuff has to be resolved eventually, but it does not cause any problems
		Destroy (gameObject);
	}
}

