using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : Character {
	
	//Variable declaration
	public CharacterController controller;
	
	public Transform playerTransform;
	private Fighter player;

	public AnimationClip attackClip;
	public AnimationClip runClip;
	public AnimationClip idleClip;
	public AnimationClip dieClip;

	public float aggroRange;
	public bool hasAggro;
	
	// Use this for initialization
	void Start () {
		//health = 100;
		health = maxHealth;
		energy = maxEnergy;
		player = playerTransform.GetComponent<Fighter> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (inRange());
		//Debug.Log (health);
		if (!isDead ()) {
			if (!inAttackRange ()&& (inAggroRange()||hasAggro)) {

				hasAggro=true;
				chasePlayer ();
			} 
			else if(inAttackRange ()) {
				//animation.CrossFade (idle.name);
				attack();
			}
		} 
		else {
			dieMethod();
		}
	}
	
	public bool inAttackRange(){
		return Vector3.Distance(transform.position, playerTransform.position)<attackRange;
	}
	public bool inAggroRange(){
		return Vector3.Distance(transform.position, playerTransform.position)<aggroRange;
	}
	
	private void attack(){
		if (!player.isDead ()) {
			animation.CrossFade (attackClip.name);

			if (animation [attackClip.name].time > animation [attackClip.name].length * impactTime && !impacted && animation [attackClip.name].time < 0.90 * animation [attackClip.name].length) {
				player.takeDamage (damage);
				impacted = true;
			}

			if (animation [attackClip.name].time > 0.90 * animation [attackClip.name].length) {
				impacted = false;
			}
		}
	}

	private void chasePlayer(){
		transform.LookAt (playerTransform.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade (runClip.name);
	}

	private void dieMethod(){
		Destroy(controller);

		playerTransform.GetComponent<Fighter> ().enemy = null;
		
		animation.CrossFade (dieClip.name);
		
		if (animation[dieClip.name].time > animation[dieClip.name].length * 0.80) {
			animation[dieClip.name].speed = 0;
		}
	}

	void OnMouseOver(){
		//Debug.Log ("Mouse is over");
		if (!isDead ()) {
			playerTransform.GetComponent<Fighter> ().enemy = this;
		}
	}

	void OnMouseExit(){
		playerTransform.GetComponent<Fighter> ().enemy = null;
	}	
}
