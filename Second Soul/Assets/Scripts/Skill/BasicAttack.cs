using UnityEngine;
using System.Collections;

public abstract class BasicAttack : MonoBehaviour, ISkill {

	protected Character caster; // protected
	//public Character target;
	protected float damage;
	
	protected float impactTime;
	//public bool impacted;
	
	protected float skillLength;
	//public float skillDurationLeft;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void useSkill(Vector3 target, Character targetCharacter){
		//impactTime = 0.35f;//use attackspeed]
		if ((caster.target == null && caster.GetType().IsSubclassOf(typeof(Player))) || !caster.inAttackRange (target)) {
			Player player = (Player) caster;
			player.startMoving(target);
			if(caster.target!=null){
				player.chasing=true;
			}
			return;
		}
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}

		if (caster.GetType().IsSubclassOf(typeof(Fighter))|| caster.GetType().IsSubclassOf(typeof(Enemy))) {
			damage = caster.attackPower;
			impactTime = 1/caster.attackSpeed;
		}
		else {
			damage = caster.spellPower;
			impactTime = 1/caster.castSpeed;
		}
		skillLength = animation[caster.attackClip.name].length;
		transform.LookAt (target);
		caster.animateAttack();
		//it'll look wrong because of the animation time, but I want to make attack speed will work. I'm still trying to make it look better
		//caster.skillDurationLeft = skillLength;
		caster.skillDurationLeft = impactTime;
		animation [caster.attackClip.name].normalizedSpeed = 1/impactTime;
		StartCoroutine(applyAttackDamage(targetCharacter, DamageType.Physical));
	}
	
	/*
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
	*/
	
	IEnumerator applyAttackDamage(Character delayedTarget, DamageType type){
		yield return new WaitForSeconds(skillLength * impactTime);
		if (delayedTarget != null){
			delayedTarget.takeDamage(damage,type);
		}
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
}
