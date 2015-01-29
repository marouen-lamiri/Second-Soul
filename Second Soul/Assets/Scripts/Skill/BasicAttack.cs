using UnityEngine;
using System.Collections;

public abstract class BasicAttack : MonoBehaviour, ISkill {

	protected Character caster; // protected
	
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
			GameObject hit = hits[i].collider.gameObject;
			if(hit.GetComponent<Character>()!=null && (hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Enemy)) || hit.GetComponent<Character>().GetType() == typeof(Enemy))){
				targetPosition = hit.transform.position;
				return;
			}
			else if(hit.CompareTag("Floor")){
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
