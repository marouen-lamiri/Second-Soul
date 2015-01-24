﻿using UnityEngine;
using System.Collections;

public abstract class BasicAttack : MonoBehaviour, ISkill {

	protected Character caster; // protected
	//public Character target;
	protected float damage;
	
	protected float impactTime;
	//public bool impacted;
	
	protected float skillLength;
	//public float skillDurationLeft;
	protected Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		skillStart ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public abstract void skillStart ();

	public void useSkill(){
		skillStart ();
		rayCast ();
		if ((caster.target == null && caster.GetType().IsSubclassOf(typeof(Player)) && !caster.attackLocked()) || !caster.inAttackRange (caster.target.transform.position)) {
			Player player = (Player) caster;
			player.startMoving(targetPosition);
			if(caster.target!=null){
				player.chasing=true;
			}
			return;
		}
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}

		skillLength = animation[caster.attackClip.name].length;
		transform.LookAt (caster.target.transform.position);
		caster.animateAttack();
		animateAttack ();
		//it'll look wrong because of the animation time, but I want to make attack speed will work. I'm still trying to make it look better
		//caster.skillDurationLeft = skillLength;
		caster.skillDurationLeft = impactTime;
		animation [caster.attackClip.name].normalizedSpeed = 1/impactTime;
		StartCoroutine(applyAttackDamage(caster.target, DamageType.Physical));
	}
	
	public void rayCast(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);
		for (int i = 0; i < hits.Length; ++i) {
			if(hits[i].collider.GetType().IsSubclassOf(typeof(Enemy)) || hits[i].collider.GetType() == typeof(Enemy)){
				targetPosition = hits [1].point;
				return;
			}
		}
		//this only happens if the for loop above fails to find an Enemy
		targetPosition = hits[0].point;
		if(caster.chasing == true){
			if(caster.target != null){//if you have a target
				targetPosition=caster.target.transform.position;
			}
			else{//if you don't have a target, then chasing is on when it should be off
				caster.chasing = false;
			}
		}
	}
	public abstract void animateAttack ();

	
	IEnumerator applyAttackDamage(Character delayedTarget, DamageType type){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(damage,type);
		}
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
}
