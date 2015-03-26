using UnityEngine;
using System.Collections;

public abstract class BasicAttack : MonoBehaviour, ISkill {

	public Character caster; // protected
	
	protected float impactTime;
	protected Character delayedTarget;
	
	public float skillLength;
	//public float skillDurationLeft;
	protected Vector3 targetPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}

	public abstract void skillStart ();

	public void useSkill(){
		skillStart ();
		rayCast ();
		delayedTarget = caster.target;
		if ((caster.target == null && caster.GetType().IsSubclassOf(typeof(Player)) && !caster.attackLocked()) || (caster.target != null && !caster.inAttackRange (caster.target.transform.position))) {
			Player player = (Player) caster;
			player.startMoving(targetPosition);
			return;
		}
		else if(caster.inAttackRange (caster.target.transform.position) && caster.moving){
			caster.stopMoving();
			caster.attacking = true;
		}

		transform.LookAt (caster.target.transform.position);
		caster.animateAttack();
		animateAttack ();
		//it'll look wrong because of the animation time, but I want to make attack speed will work. I'm still trying to make it look better
		//caster.skillDurationLeft = skillLength;
		if (caster.attackClip != null) {
			animation [caster.attackClip.name].normalizedSpeed = 1 / impactTime;
		}
		if (caster.attackClip != null) {
			skillLength = animation [caster.attackClip.name].length;
		}
		
		caster.skillDurationLeft = skillLength;
		StartCoroutine(applyAttackDamage(caster.target, DamageType.Physical));
	}
	public bool canFinishAttack(){
		if (delayedTarget != null && caster.inAttackRange (delayedTarget.transform.position)) {
			return true;
		}
		return false;
	}

	public void finishAttack(){//this is for when you click an enemy and then you expect to attack it when you make it there without clikcing again
		if (caster.attackClip != null) {
			animation [caster.attackClip.name].normalizedSpeed = 1 / impactTime;
		}
		if (caster.attackClip != null) {
			skillLength = animation [caster.attackClip.name].length;
		}
		caster.stopMoving();
		caster.attacking = true;

		if (caster.attackClip != null) {
			animation [caster.attackClip.name].normalizedSpeed = 1 / impactTime;
		}
		if (caster.attackClip != null) {
			skillLength = animation [caster.attackClip.name].length;
		}

		caster.skillDurationLeft = skillLength;
		StartCoroutine(applyAttackDamage(delayedTarget, DamageType.Physical));
	}
	
	public void rayCast(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		int rayDistance = 1000;
		hits = Physics.RaycastAll(ray.origin,ray.direction, rayDistance);
		string floor = "Floor";
		for (int i = 0; i < hits.Length; ++i) {
			GameObject hit = hits[i].collider.gameObject;
			if(hit.GetComponent<Character>()!=null && (hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Enemy)))){
				targetPosition = hit.transform.position;
				return;
			}
			else if(hit.CompareTag(floor) && (caster.grid.nodeFromWorld(hits[i].point).walkable || hits.Length==1)){
				targetPosition = hits[i].point;
			}
		}
		//this only happens if the for loop above fails to find an Enemy

		if(caster.moving == true){
			if(caster.target != null){//if you have a target
				targetPosition=caster.target.transform.position;
			}
			else{//if you don't have a target, then chasing is on when it should be off
				caster.moving = false;
			}
		}
	}
	public abstract void animateAttack ();
	
	IEnumerator applyAttackDamage(Character delayedTarget, DamageType type){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(caster.getDamageCanMiss(),type);
		}
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
}
