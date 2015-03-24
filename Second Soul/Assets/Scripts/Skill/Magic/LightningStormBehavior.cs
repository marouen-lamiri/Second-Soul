using UnityEngine;
using System.Collections;

public class LightningStormBehavior : ParticleBehavior, ISorcererSubscriber {

	float timeToDestroy;
	
	public LightningStormSkill lightningStormSkill;
	public Vector3 originalSpawn;

	private Sorcerer sorcerer;
	
	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		originalSpawn = transform.position;
		
		timeToDestroy = 10f;
		
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // Sorcerer sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer)) as Sorcerer;
		this.lightningStormSkill = sorcerer.GetComponent<LightningStormSkill>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToDestroy>0){
			timeToDestroy-=Time.deltaTime;
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
		//fireball network stuff has to be resolved eventually, but it does not cause any problems
		Destroy (gameObject);
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

}
