using UnityEngine;
using System.Collections;

public abstract class BasicAttack : MonoBehaviour, ISkill {

	public Character target;
	public float damage;
	
	public float impactTime;
	public bool impacted;
	
	public float skillLength;
	public float skillDurationLeft;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void useSkill(){
		transform.LookAt (target.transform.position);
		target.target.animateAttack();
		skillDurationLeft = skillLength;
		
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
	
	IEnumerator applyAttackDamage(Character delayedTarget){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(damage);
		}
	}
	
}
