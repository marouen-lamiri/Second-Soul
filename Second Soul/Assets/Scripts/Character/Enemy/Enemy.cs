using UnityEngine;
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
	public bool lootGiven;

	public int assigner;
	int id;

	ISkill activeSkill1;
	
	public float dropRate;

	// networking:
	protected EnemyNetworkScript enemyNetworkScript;

	
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
		
		initializePrimaryStats();
		initializeSecondaryStatsBase();
		initializeSecondaryStats();
		calculateSecondaryStats();
		
		level = target.level;
		health = maxHealth;
		energy = maxEnergy;
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();

		// networking: makes sure each enemy is properly instantiated even on another game instance that didn't run the EnemyFactory code.
		target = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		this.target = target;
		this.sorcerer = sorcerer;
		this.transform.parent = GameObject.Find("Enemies").transform;

		// networking:
		enemyNetworkScript = (EnemyNetworkScript)gameObject.GetComponent<EnemyNetworkScript> ();


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
		enemyUpdate ();
	}
	protected void enemyUpdate(){
		characterUpdate ();
		if (!isDead ()) {
			enemyAI ();
		} 
		else {
			dieMethod();
			giveLoot(dropRate, transform.position);
			giveXP();
			destroySelf();
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
		else if(!inAttackRange (target.transform.position) && hasAggro){
			chasingTarget = target.gameObject;
			startMoving(target.transform.position);
			if(outAggroRange()){
				loseAggro();
			}
		} 
		else if(inAttackRange (target.transform.position) && !attackLocked()){
			//meshAgent.Stop(true);
			stopMoving ();
			activeSkill1.setCaster(this);
			activeSkill1.useSkill();

			// networking: event listener to RPC the attack anim
			if(enemyNetworkScript != null) {
				enemyNetworkScript.onAttackTriggered("activeSkill2");
			} else {
				print("No enemyNetworkScript attached to enemy.");
			}		

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
		Destroy (this.GetComponent<CapsuleCollider>());
		if (transform.FindChild ("Sphere") != null) {
			Destroy (transform.FindChild ("Sphere").gameObject);
		}
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
