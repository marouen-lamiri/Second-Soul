using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
	public CharacterController controller;
	
	public int level; // only public for now to see level in inspector
	
	// Secondary stats... calculated from primary stats
	protected int armor;
	protected int fireResistance;
	protected int coldResistance;
	protected int lightningtResistance;
	
	protected float accuracy;
	protected float attackSpeed;
	
	protected float castSpeed;
	protected float cdr; // cooldown reduction
	
	protected float criticalChance;
	protected float criticalDamage;
	
	protected float attackPower;
	
	protected float spellCriticalChance;
	protected float spellCriticalDamage;
	
	protected float spellPower;
	
	public double health;
	public double maxHealth;
	
	public double energy;
	public double maxEnergy;
	
	protected float healthRegen;
	protected float energyRegen;
	
	// base are the static amount per primary stat to the secondary stat
	protected int armorBase;
	protected int fireResBase;
	protected int coldResBase;
	protected int lightResBase;
	
	protected float accurBase;
	protected float attSpeedBase;
	
	protected float castSpeedBase;
	protected float cdrBase;
	
	protected float critChanBase;
	protected float critDmgBase;
	
	protected float attPowerBase;
	
	protected float spCritChanBase;
	protected float spCritDmgBase;
	
	protected float spPowerBase;
	
	protected int hpBase;
	protected int enBase;
	
	protected float hpRegBase;
	protected float enRegBase;
	
	/*
	fighter:
	- berserker
	- knight
	- monk
	
	sorcerer:
	- mage
	- priest
	- druid
	
	enemy:
	- golem
	- imp
	
	
	*/
	
	
	//public NavMeshAgent meshAgent;
	private int currentWaypoint;
	
	public bool playerEnabled;
	
	public Character target;

	public float speed;
	public bool chasing;

	
	
	public float attackRange;
	public float damage;
	
	public float skillLength;
	public float skillDurationLeft;
	
	public float impactTime;
	public bool impacted;
	
	public Vector3 startPosition;
	
	public AnimationClip idleClip;
	public AnimationClip runClip;
	public AnimationClip attackClip;
	public AnimationClip dieClip;

	// Use this for initialization
	void Start () {
		//skillLength = animation[attackClip.name].length; // nothing happens in a parent start
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//public abstract void levelUp();
	
	public virtual void initializeSecondaryStats(){
		armor = 50;
		fireResistance = 10;
		coldResistance = 10;
		lightningtResistance = 10;
		
		accuracy = 0.8f;
		attackSpeed = 1f;
		
		castSpeed = 1f;
		cdr = 0f;
		
		criticalChance = 0.1f;
		criticalDamage = 1.5f;
		
		attackPower = 1f;
		
		spellCriticalChance = 0.1f;
		spellCriticalDamage = 1.5f;
		
		spellPower = 1f;
		
		maxHealth = 200;
		maxEnergy = 100;
		
		healthRegen = 0.5f;
		energyRegen = 0.5f;
	}
	
	public virtual void initializeSecondaryStatsBase(){		
		armorBase = 5;
		fireResBase = 1;
		coldResBase = 1;
		lightResBase = 1;
		
		accurBase = 0.001f;
		attSpeedBase = 0.01f;
		
		castSpeedBase = 0.01f;
		cdrBase = 0.01f;
		
		attPowerBase = 0.01f;
		
		critChanBase = 0.005f;
		critDmgBase = 0.01f;
		
		attPowerBase = 0.01f;
		
		spCritChanBase = 0.005f;
		spCritDmgBase = 0.01f;
		
		spPowerBase = 0.01f;
		
		hpBase = 5;
		enBase = 2;
		
		hpRegBase = 0.01f;
		enRegBase = 0.01f;
	}
	
	public void takeDamage(double damage){
		health -= damage;
		
		if (health <= 0) {
			health = 0;
		}
	}

	public void healCharacter (double heal){
		health += heal;

		if (health >= maxHealth) {
			health = maxHealth;
		}
	}

	public void rechargeCharacter (double recharge){
		energy += recharge;
		
		if (energy >= maxEnergy) {
			energy = maxEnergy;
		}
	}

	public virtual void loseEnergy(float energy){

	}
	
	public virtual void gainExperience(int experience){

	}

	public virtual bool isDead(){
		if (health <= 0) {
			return true;
		}
		return false;
	}
	
	public void chaseTarget(){
		chasing = true;
		animateRun();

		/*if(currentWaypoint >= path.vectorPath.Count){
			return;
		}*/
		
		//Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		//dir.y = 0;
		//Debug.Log (dir);
		//Debug.Log (currentWaypoint);
		//transform.LookAt (target.transform.position);
		//meshAgent.SetDestination(target.transform.position);
		//controller.SimpleMove (transform.forward * speed);
		//controller.SimpleMove (dir * speed);
		
		/*if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 2f){
			currentWaypoint++;
		}*/


	}
	
	public void attack(){
		transform.LookAt (target.transform.position);
		animateAttack();
		
		skillDurationLeft = skillLength;
		//Debug.Log (++attackcount);
		StartCoroutine(applyAttackDamage(target));
	}
	
	
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
	
	public bool inAttackRange(){
		return Vector3.Distance(target.transform.position, transform.position) <= attackRange;
	}
	
	IEnumerator applyAttackDamage(Character delayedTarget){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(damage);
		}
	}
	
	public void dieMethod(){
		//CancelInvoke("applyAttackDamage");
		//StopCoroutine(applyAttackDamage(target));
		animateDie();
		
		if (animation[dieClip.name].time > animation[dieClip.name].length * 0.80) {
			animation[dieClip.name].speed = 0;
		}
		
		//RESPAWN/ETC...?
	}
	
	//public void applyAttackDamage(){
		//target.takeDamage(damage);
	//}
	
	public float getInitialPositionX(){
		return startPosition.x;
	}

	public float getInitialPositionY(){
		return startPosition.y;
	}

	public float getInitialPositionZ(){
		return startPosition.z;
	}
	
	public void animateIdle(){
		animation.CrossFade(idleClip.name);
	}
	
	public void animateRun(){
		animation.CrossFade(runClip.name);
	}
	
	public void animateAttack(){
		animation.CrossFade (attackClip.name);
	}
	
	public void animateDie(){
		animation.CrossFade (dieClip.name);
	}
}
