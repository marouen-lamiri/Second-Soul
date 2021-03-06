﻿using UnityEngine;
using System.Collections;

public abstract class RangedSkill : MonoBehaviour, ISkill {

	public Character caster;
	public double damage;
	public float damageModifier;
	protected float energyCost;
	protected float castTime;
	
	protected float skillLength;
	
	protected SorcererNetworkScript sorcererNetworkScript;

	public DamageType damageType;
	// Use this for initialization
	void Start () {
		skillStart ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void skillStart(){
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
		setCaster (gameObject.GetComponent<Character> ());
	}

	public abstract void useSkill ();

	public abstract void rayCast ();

	protected virtual Vector3 AIRayCast (Vector3 targetPosition){
		SorcererAI ai = gameObject.GetComponent<SorcererAI> ();
		if (ai.checkAIPlayingStatus()) {
			return ai.checkNearestEnemy().transform.position;
		}
		return targetPosition;
	}

	protected virtual Vector3 EnemyRayCast (Vector3 targetCharacter){
		return targetCharacter;
	}

	public void setCaster(Character caster){
		this.caster = caster;
	}

	public abstract void animateAttack();

	public float getEnergyCost(){
		return energyCost;
	}
}
