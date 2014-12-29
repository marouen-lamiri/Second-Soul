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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//FIXME: cheat for avoiding generics
	public void useSkill(Vector3 target){
		Debug.Log ("wrong type passing: use skill");
	}
	
	public void useSkill(Character target){
		//impactTime = 0.35f;//use attackspeed
		impactTime = caster.attackSpeed;
		skillLength = animation[caster.attackClip.name].length;
		damage = caster.getDamage ();

		transform.LookAt (target.transform.position);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		if(caster
		StartCoroutine(applyAttackDamage(target, DamageType.Physical));
	}
	
	/*
	public bool attackLocked(){
		skillDurationLeft -= Time.deltaTime;
		return actionLocked ();
	}
	
	public bool actionLocked(){
		if (skillDurationLeft > 0){
			return true;
		}
		else{
			return false;
		}
	}
	*/
	
	IEnumerator applyAttackDamage(Character delayedTarget, DamageType type){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(damage);
		}
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
}
