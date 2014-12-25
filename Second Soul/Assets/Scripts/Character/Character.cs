using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
	public CharacterController controller;
	
	//public NavMeshAgent meshAgent;
	private int currentWaypoint;
	
	public bool playerEnabled;
	
	public Character target;

	public float speed;
	public bool chasing;

	public double health;
	public double maxHealth;
	
	public double energy;
	public double maxEnergy;
	
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
		skillLength = animation[attackClip.name].length;
	}
	
	// Update is called once per frame
	void Update () {
	
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
