using UnityEngine;
using System.Collections;

public class FireballBehavior : ParticleBehavior {

	float timeToDestroy;

	public FireballSkill fireballSkill;
	public Vector3 originalSpawn;
	Component[] fireballComponents;
	float travelDistance;
	float speed;

	// Use this for initialization
	void Start () {
		originalSpawn = transform.position;

		timeToDestroy = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if (fireballSkill == null && caster != null) {
			fireballSkill = (FireballSkill)skill;
		}
		if (caster == null && (travelDistance == null || speed == null)) {
			return;
		}
		else{
			travelDistance = fireballSkill.travelDistance;
			speed = fireballSkill.speed;
		}
		if(Vector3.Distance(originalSpawn, transform.position) < travelDistance){
			float oldY = transform.position.y;
			transform.position += Time.deltaTime * speed * transform.forward;
			transform.position = new Vector3(transform.position.x, oldY, transform.position.z);
		}
		else{
			StartCoroutine(selfDestruct());
		}
	}

	void OnParticleCollision(GameObject obj){
		Character character = obj.GetComponent<Character> ();
		if (character == null) {
			return;
		}
//		this means that if the caster is a player, and the skill hit an enemy
		if (casterType.IsSubclassOf (typeof(Player))){
			if( !character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (damage, damageType);
			}
		} 
		else {
			if( character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (damage, damageType);
			}
		}
	}

	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (timeToDestroy);
//		Network.Destroy (gameObject);
		//fireball network stuff has to be resolved eventually, but it does not cause any problems
		Destroy (gameObject);
	}
}
