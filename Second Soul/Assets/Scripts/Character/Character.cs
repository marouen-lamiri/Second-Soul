using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {
	public CharacterController controller;
	protected Grid grid;
	protected PathFinding pathing;
	private List<Vector3> path;
	protected GameObject sphere;
	public int level; // only public for now to see level in inspector

	//want to be protected - putting to public to check in inspector
	// Secondary stats... calculated from primary stats
	public int armor;
	public int fireResistance;
	public int coldResistance;
	public int lightningtResistance;
	
	public float accuracy;
	public float attackSpeed;
	
	public float castSpeed;
	public float cdr; // cooldown reduction
	
	public float criticalChance;
	public float criticalDamage;
	
	public float attackPower;
	
	public float spellCriticalChance;
	public float spellCriticalDamage;
	
	public float spellPower;
	
	public double health;
	public double maxHealth;
	
	public double energy;
	public double maxEnergy;
	
	public float healthRegen;
	public float energyRegen;
	
	// base are the static amount per primary stat to the secondary stat
	public int armorBase;
	public int fireResBase;
	public int coldResBase;
	public int lightResBase;
	
	public float accurBase;
	public float attSpeedBase;
	
	public float castSpeedBase;
	public float cdrBase;
	
	public float critChanBase;
	public float critDmgBase;
	
	public float attPowerBase;
	
	public float spCritChanBase;
	public float spCritDmgBase;
	
	public float spPowerBase;
	
	public int hpBase;
	public int enBase;
	
	public float hpRegBase;
	public float enRegBase;
	//end of section that should be protected
	/*
	fighter:
	- berserker // primary: strength; secondary: dexterity
	- knight // primary: endurance; secondary: strength
	- monk // primary: dexterity; secondary: endurance
	
	sorcerer:
	- mage // primary: intelligence; secondary: wisdom
	- priest // primary: spirit; secondary: intelligence
	- druid // primary: wisdom; secondary: spirit
	
	enemy:
	- golem // strong, tanky, melee
	- imp // weak & numerous, squishy, ranged
	
	
	*/
	
	
	//public NavMeshAgent meshAgent;
	private int currentWaypoint;
	
	public bool playerEnabled;
	
	public Character target;
	public Vector3 nextPosition;

	public float speed;
	public bool chasing;

	
	
	public float attackRange;
	//we need to discuss how damage works
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
		characterStart ();
	}
	protected void characterStart(){
		sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.renderer.castShadows = false;
		sphere.renderer.receiveShadows = false;
		sphere.transform.parent = transform;
		//was going to scale to 0.1f, but scaling the map down didn't seem to work
		sphere.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		sphere.gameObject.layer = LayerMask.NameToLayer ("Minimap");
	}
	
	// Update is called once per frame
	void Update () {
		characterUpdate ();
	}
	protected void characterUpdate(){
		if (transform.FindChild ("Sphere") != null) {
			transform.FindChild ("Sphere").transform.position = new Vector3 (transform.position.x, 10.0f, transform.position.z);
		}
	}
	public void setPathing(PathFinding path){
		this.pathing = path;
	}

	public void setGrid(Grid grid){
		this.grid = grid;
	}

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
		
		attackPower = 10f;
		
		spellCriticalChance = 0.1f;
		spellCriticalDamage = 1.5f;
		
		spellPower = 10f;
		
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
		
		spCritChanBase = 0.005f;
		spCritDmgBase = 0.01f;
		
		spPowerBase = 0.01f;
		
		hpBase = 5;
		enBase = 2;
		
		hpRegBase = 0.01f;
		enRegBase = 0.01f;
	}
	
	public bool hitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= accuracy * 100) {
			return true;
		}
		else {
			return false;
		}
	}
	//to be overriden
	public virtual bool criticalHitCheck (){
		return false;
	}
	//to be overriden
	public virtual double getDamage (){
		return -1;
	}

	//we ned to tweak the values here
	public void takeDamage(double damage, DamageType type){
		if (type == DamageType.Physical) {
			damage -= armor/200;
		}
		else if (type == DamageType.Fire) {
			damage -= fireResistance/200;
		}
		else if (type == DamageType.Ice) {
			damage -= fireResistance/200;
		}
		else if (type == DamageType.Lightning){
			damage -= lightningtResistance/200;
		}
		if (damage < 0) {
			damage = 0;
		}
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

	public virtual bool loseEnergy(float energy){
		return false;
	}
	
	public virtual void gainExperience(int experience){

	}

	public virtual bool isDead(){
		if (health <= 0) {
			return true;
		}
		return false;
	}


	// when possible transform this to take (Vector3 pos, Vector3 targetPos) so this can be extensible to chasing another point like an item
	public void chaseTarget(Vector3 targetPosition){
		chasing = true;
		animateRun();
//		Debug.Log (pathing);
		Vector3 destination;
		List<Vector3> path = grid.worldFromNode(grid.path);
		if (Vector3.Distance (transform.position, targetPosition) > 2) {
			destination = path[1];
		} 
		else {
			destination = targetPosition;
		}
		Quaternion newRotation = Quaternion.LookRotation (destination - transform.position);
		newRotation.x = 0;
		newRotation.z = 0;
		
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 7);
		controller.SimpleMove (transform.forward * speed *4);
	}
	//trying to comment this out
//	public void attack(){
//		transform.LookAt (target.transform.position);
//		animateAttack();
//		
//		skillDurationLeft = skillLength;
//		//Debug.Log (++attackcount);
//		StartCoroutine(applyAttackDamage(target));
//	}

	public void clickPosition(Vector3 position){
		nextPosition = position;
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
	//this (below)exists in Character
	//is this duplicate necessary?
	//trying to comment it out
//	IEnumerator applyAttackDamage(Character delayedTarget){
//		yield return new WaitForSeconds(skillLength * impactTime);
//		if (delayedTarget != null){
//			delayedTarget.takeDamage(damage);
//		}
//	}
	
	public void dieMethod(){
		//CancelInvoke("applyAttackDamage");
		//StopCoroutine(applyAttackDamage(target));
		animateDie();
		
		if (animation[dieClip.name].time > animation[dieClip.name].length * 0.80) {
			animation[dieClip.name].speed = 0;
		}
		
		//RESPAWN/ETC...?
	}
	
	public float getInitialPositionX(){
		return startPosition.x;
	}

	public float getInitialPositionY(){
		return startPosition.y;
	}

	public float getInitialPositionZ(){
		return startPosition.z;
	}

	public void setInitialPosition(Vector3 initPos){
		startPosition = initPos;
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
