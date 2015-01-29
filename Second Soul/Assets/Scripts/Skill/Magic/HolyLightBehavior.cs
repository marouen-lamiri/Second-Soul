using UnityEngine;
using System.Collections;

public class HolyLightBehavior : ParticleBehavior {
	
	float timeToDestroy;
	
	public CycloneSkill cycloneSkill;
	public Vector3 originalSpawn;
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		originalSpawn = transform.position;
		
		timeToDestroy = 10f;
		
		Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType(typeof(Sorcerer)) as Sorcerer;
		this.cycloneSkill = sorcerer.GetComponent<CycloneSkill>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToDestroy>0){
			timeToDestroy-=Time.deltaTime;
		}
		else{
			//StartCoroutine(selfDestruct());
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
		//fireball network stuff has to be resolved eventually, but it does not cause any problems
		Destroy (gameObject);
	}
}

