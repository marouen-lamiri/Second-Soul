﻿using UnityEngine;
using System.Collections;

public class Enemy : Character {
	
	//Variable declaration
	protected int strength; // base damage, armor, critt damage
	protected int dexterity; // attack speed, crit chance, accuracy
	protected int endurance; // health, resistances, health regen
	
	protected int strengthPerLvl;
	protected int dexterityPerLvl;
	protected int endurancePerLvl;

	public int experienceWorth;
	public int experienceBase;
	
	public Sorcerer sorcerer;
	public float aggroRange;
	public bool hasAggro;
	
	public bool xpGiven;
	
	ISkill activeSkill1;
	
	
	// Use this for initialization
	void Start (){
		experienceBase = 25;
		xpGiven = false;

		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();

		level = target.level;
		health = maxHealth;
		energy = maxEnergy;
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();
	}

	protected virtual void initializePrimaryStats(){
		strengthPerLvl = 1;
		dexterityPerLvl = 1;
		endurancePerLvl = 1;
		
		strength = 10;
		dexterity = 10;
		endurance = 10;
	}

	public void calculateSecondaryStats(){
		armor += strength * armorBase;
		fireResistance += endurance * fireResBase;
		coldResistance += endurance * coldResBase;
		lightningtResistance += endurance * lightResBase;
		
		accuracy += dexterity * accurBase;
		attackSpeed += dexterity * attSpeedBase;
		
		criticalChance += dexterity * critChanBase;
		criticalDamage += strength * critDmgBase;
		
		attackPower += strength * attPowerBase;
		
		maxHealth += endurance * hpBase;
		healthRegen += endurance * hpRegBase;
		
		health = maxHealth;
	}

	// Update is called once per frame
	void Update (){
		//Debug.Log (inRange());
		//Debug.Log (health);
		if (!isDead ()) {
			enemyAI ();
		} 
		else {
			dieMethod();
			giveXP();
			destroySelf();
			Destroy(transform.FindChild("Sphere").gameObject);
		}
	}
	
	void LateUpdate() {
		level = target.level;
		calculateXPWorth();
	}

	public void enemyAI(){
		if(!hasAggro){
			if(inAwareRadius()){
				if(hasDirectView()){
					hasAggro = true;
				}
			}
		}
		else if(!inAttackRange () && hasAggro){
			chaseTarget();
			if(outAggroRange()){
				loseAggro();
			}
		} 
		else if(inAttackRange () && !attackLocked()){
			//meshAgent.Stop(true);
			activeSkill1.setCaster(this);
			activeSkill1.useSkill(target);
		}	
	}
	public bool hasDirectView(){
		Vector3 playerPosition = target.transform.position;
		Vector3 enemyOrigin = new Vector3 (transform.position.x, transform.position.y + controller.height, transform.position.z);
		Vector3 direction = new Vector3(playerPosition.x, playerPosition.y + target.controller.height, playerPosition.z) - enemyOrigin;
		float distance = Vector3.Distance (target.transform.position, transform.position);

		RaycastHit[] obstaclesHit;
		obstaclesHit = Physics.RaycastAll(enemyOrigin, direction, distance);

		if(obstaclesHit.Length > 1)
			return false;
		else
			return true;
	}

	public bool inAwareRadius(){
		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);

		float percentView = (180 - angle) / 180;
		float viewRadiusPercent = 0.25f + 0.75f * percentView;

		return Vector3.Distance(transform.position, target.transform.position) < (aggroRange * viewRadiusPercent);
	}

	public bool inAggroRange(){
		return Vector3.Distance(transform.position, target.transform.position) < aggroRange;
	}

	public bool outAggroRange(){
		return Vector3.Distance(transform.position, target.transform.position) > (aggroRange * 1.5);
	}

	public void loseAggro(){
		//meshAgent.Stop(true);
		hasAggro = false;
		animateIdle();
	}
	
	void calculateXPWorth(){
		experienceWorth = level * experienceBase;
	}
	
	void giveXP(){
		if(!xpGiven){
			Debug.Log (experienceWorth);
			target.gainExperience(experienceWorth/2);//divided by 2 because of xp split. we can always adjust the experienceWorth if necessary
			sorcerer.gainExperience(experienceWorth/2);
		}
		xpGiven = true;
	}
	
	/*private void attack(){
		if (!target.isDead ()){
			animateAttack();

			if (animation [attackClip.name].time > animation [attackClip.name].length * impactTime && !impacted && animation [attackClip.name].time < 0.90 * animation [attackClip.name].length) {
				target.takeDamage (damage);
				impacted = true;
			}

			if (animation [attackClip.name].time > 0.90 * animation [attackClip.name].length){
				impacted = false;
			}
		}
	}*/

	public void destroySelf(){
		Destroy(controller);
		Destroy (this.GetComponent<CapsuleCollider>());
		target.target = null;
		sorcerer.target = null;
	}
	
	void OnMouseDrag(){
		//Debug.Log ("Mouse is over");
		if (!isDead ()){
			target.target = this;
			sorcerer.target = this;
		}
	}
	
	void OnMouseUp(){
		target.target = null;
		sorcerer.target = null;
	}	

	void OnMouseOver(){
		//Debug.Log ("Mouse is over");
		if (!isDead () && target.target == null){
			target.target = this;
			sorcerer.target = this;
		}
	}

	void OnMouseExit(){
		target.target = null;
		sorcerer.target = null;
	}	
}
