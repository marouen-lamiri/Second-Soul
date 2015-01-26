using UnityEngine;
using System.Collections;

public class IceShardBehavior : ParticleBehavior {

	bool explode;
	float timeToDestroy;
	
	public IceShardSkill iceSkill;
	public Vector3 originalSpawn;
	Component[] fireballComponents;
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		originalSpawn = transform.position;
		explode = false;
		
		timeToDestroy = 10f;
		
		//fix:
		Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType(typeof(Sorcerer)) as Sorcerer;
		this.iceSkill = sorcerer.GetComponent<IceShardSkill>();
	}
	
	// Update is called once per frame
	void Update () {
		if (caster == null) {
			return;
		}
		if(Vector3.Distance(originalSpawn, transform.position) < iceSkill.travelDistance && !explode){
			float oldY = transform.position.y;
			transform.position += Time.deltaTime * iceSkill.speed * transform.forward;
			transform.position = new Vector3(transform.position.x, oldY, transform.position.z);
		}
		else{
			//fireballComponents [1].GetComponent<ParticleRenderer> ().enabled = true; // maybe network error
			//			this.GetComponent<CharacterController>().radius=7; // maybe network error
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
