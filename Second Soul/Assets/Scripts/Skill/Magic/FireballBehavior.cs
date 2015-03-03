using UnityEngine;
using System.Collections;

public class FireballBehavior : ParticleBehavior {

	float timeToDestroy;

	public FireballSkill fireballSkill;
	public Vector3 originalSpawn;
	Component[] fireballComponents;

	// Use this for initialization
	void Start () {
		originalSpawn = transform.position;

		timeToDestroy = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if (caster == null) {
			return;
		}
		if (fireballSkill == null) {
			fireballSkill = (FireballSkill)skill;
		}
		if(Vector3.Distance(originalSpawn, transform.position) < fireballSkill.travelDistance){
			float oldY = transform.position.y;
			transform.position += Time.deltaTime * fireballSkill.speed * transform.forward;
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
		if (caster.gameObject.GetComponent<Character> ().GetType ().IsSubclassOf (typeof(Player))){
			if( !character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (skill.damage, skill.damageType);
			}
		} 
		else {
			if( character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (skill.damage, skill.damageType);
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
