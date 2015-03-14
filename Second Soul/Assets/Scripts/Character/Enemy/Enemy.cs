using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {
	
	//Variable declaration
	protected int strength; // base damage, armor, critt damage
	protected int dexterity; // attack speed, crit chance, accuracy
	protected int endurance; // health, resistances, health regen
	
	protected int strengthPerLvl;
	protected int dexterityPerLvl;
	protected int endurancePerLvl;

	public int experienceWorth;
	public int experienceBase;
	public int prizeBase = 25;
	public int prizeLevelBonus = 2;
	
	public Sorcerer sorcerer;
	
	public float aggroRange;
	public bool hasAggro;
	
	public bool xpGiven;
	public bool lootGiven;
	public bool prizeGiven;
	public int prize;

	public int assigner;
	int id;

	ISkill activeSkill1;
	
	public float dropRate;
	
	CapsuleCollider collider;

	// scripts of same GameObject
	protected EnemyNetworkScript enemyNetworkScript;
	Wander wanderScript;
	Arrive arriveScript;
	
	// Use this for initialization
	void Start (){
		enemyStart ();
	}
	protected void enemyStart(){
		
		characterStart ();
		sphere.renderer.material.color = Color.red;
		grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		pathing = (PathFinding)GameObject.FindObjectOfType (typeof(PathFinding));
		experienceBase = 25;
		xpGiven = false;
		lootGiven = false;
		
		// networking: makes sure each enemy is properly instantiated even on another game instance that didn't run the EnemyFactory code.
		target = (Fighter) GameObject.FindObjectOfType (typeof (Fighter)); // for the enemies perspective target is always fighter
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));

		
		level = target.level;
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		health = maxHealth;
		energy = maxEnergy;
		
		activeSkill1 = (BasicMelee)GetComponent<BasicMelee>();

		
		this.transform.parent = GameObject.Find("Enemies").transform;

		// networking:
		enemyNetworkScript = (EnemyNetworkScript)GetComponent<EnemyNetworkScript> ();
		
		collider = GetComponent<CapsuleCollider>();
		wanderScript = GetComponent<Wander> ();
		arriveScript = GetComponent<Arrive> ();
	}

	public override bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= criticalChance * 100) {
			return true;
		}
		else {
			return false;
		}
	}

	public override double getDamage(){
		if (criticalHitCheck ()) {
			return attackPower*criticalDamage;
		}
		return attackPower;
	}

	public override double getDamageCanMiss(){
		if(!hitCheck()){
			return 0;
		}
		if (criticalHitCheck ()) {
			return attackPower*criticalDamage;
		}
		return attackPower;
	}

	protected abstract void initializePrimaryStats ();

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
	
	public abstract void initializeSecondaryStats ();
	
	public virtual void initializeSecondaryStatsBase(){		
		armorBase = 7;
		fireResBase = 1;
		coldResBase = 1;
		lightResBase = 1;
		
		accurBase = 0.001f;
		attSpeedBase = 0.02f;
		
		attPowerBase = 0.01f;
		
		critChanBase = 0.01f;
		critDmgBase = 0.02f;
		
		attPowerBase = 0.025f;
		
		hpBase = 5;
		
		hpRegBase = 0.01f;
	}

	// Update is called once per frame
	void FixedUpdate (){
		enemyUpdate ();
	}
	protected void enemyUpdate(){
		characterUpdate ();
		if (!isDead ()) {
			level = target.level;
			enemyAI ();
		} 
		else {
			dieMethod();
			giveLoot(dropRate, transform.position);
			calculateXPWorth();
			calculatePrize();
			prizeCurrency();
			giveXP();
			destroySelf();
		}
	}

	public void calculatePrize(){
		prize = (level + prizeLevelBonus) * prizeBase;
	}

	public void prizeCurrency(){
		if(!prizeGiven){
			Debug.Log ("enemy experience worth: " + experienceWorth);
			//UnityNotificationBar.UNotify("Gained " + experienceWorth + " Experience"); //although this might appear false in Mono-Develop, it actually works as an external asset
			if(target.playerEnabled){
				target.gainGold(prize);
			}
			if(sorcerer.playerEnabled){
				sorcerer.gainSouls(prize);
			}
		}
		prizeGiven = true;
	}

	public void enemyAI(){
		bool tst1 = inAttackRange (target.transform.position);
		bool tst2 = !attackLocked();
		////
		if(!hasAggro){
			if(inAwareRadius() && hasDirectView()){
				hasAggro = true;
			}
			else{
				idleLogic ();
			}
		}
		else if (cannotMove ()) {
			return;
		}
		else if(!inAttackRange () && hasAggro){
			chasingTarget = target.gameObject;
			startMoving(target.transform.position);
			if(outAggroRange()){
				loseAggro();
			}
		} 
		//else if(inAttackRange (target.transform.position) && !attackLocked()){
		else if(inAttackRange () && !actionLocked()){
			//meshAgent.Stop(true);
			stopMoving ();
			attackTarget();

			// networking: event listener to RPC the attack anim
			if(enemyNetworkScript != null) {
				enemyNetworkScript.onAttackTriggered("activeSkill2");
			} else {
				print("No enemyNetworkScript attached to enemy.");
			}		

		}	
	}

	protected virtual void attackTarget(){
		activeSkill1.setCaster(this);
		activeSkill1.useSkill();
	}

	protected virtual bool cannotMove(){
		return false;
	}

	protected virtual void idleLogic (){
		wanderScript.wanderInCircle();
		if(Vector3.Distance(wanderScript.wanderingObject.transform.position, transform.position)>arriveScript.arriveRadius){
			startMoving(wanderScript.wanderingObject.transform.position);
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
			Debug.Log ("enemy experience worth: " + experienceWorth);
			//UnityNotificationBar.UNotify("Gained " + experienceWorth + " Experience"); //although this might appear false in Mono-Develop, it actually works as an external asset
			if(target.playerEnabled){
				target.gainExperience(experienceWorth);
			}
			if(sorcerer.playerEnabled){
				sorcerer.gainExperience(experienceWorth);
			}
		}
		xpGiven = true;
	}
	
	void giveLoot(float dropRate, Vector3 position){
		if(!lootGiven && networkView.isMine){
			LootFactory.determineDrop(dropRate, position);
		}
		lootGiven = true;
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
		Destroy (collider);
		if (transform.FindChild ("Sphere") != null) {
			Destroy (transform.FindChild ("Sphere").gameObject);
		}
		Destroy(this);
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
		//gonna try to comment out these conditionals because they may be redundant and causing errors
		//if (!target.chasing) {
			target.target = null;
	//	}
		//if (!sorcerer.chasing) {
			sorcerer.target = null;
		//}
	}	
}
