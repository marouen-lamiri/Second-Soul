using UnityEngine;
using System.Collections;

public class Charge : TargetedMeleeSkill {

	private float range;
	private float originalSpeed;
	private float chargeSpeed;
	private bool charging;

	// Use this for initialization
	void Start () {
		//this part seems to not work because it should only be called when the skill it called. and this may be multiple times
//		skillStart ();
//		damage = caster.attackPower * 1.5;
//		range = caster.attackRange * 15;
//		originalSpeed = caster.speed;
//		chargeSpeed = originalSpeed * 10;
//		charging = false;
	}
	public override void skillStart(){
		base.skillStart ();
		targetCharacter = caster.target;
		damage = caster.attackPower * 1.5;
		range = caster.attackRange * 15;
		originalSpeed = caster.speed;
		chargeSpeed = originalSpeed * 5;
		charging = true;
	}
	// Update is called once per frame
	void Update () {
		//I do not believe this to be perfect. The only alternative I can think of is a complete rework of the stats system.
		if (charging && caster.inAttackRange(targetCharacter.transform.position)) {
			caster.stopMoving();
			caster.speed = originalSpeed;
			charging=false;
			targetCharacter.takeDamage(damage,DamageType.Physical);
			caster.chasingTarget = null;
			caster.skillDurationLeft=0;
			animateAttack();
		}
	}

	public override void useSkill(){
		if (caster.target != null) {
			skillStart ();
			caster.skillDurationLeft = 5;
			caster.speed = chargeSpeed;
			caster.chasingTarget = caster.target.gameObject;
			caster.goalPosition = targetCharacter.transform.position;
			caster.startMoving(caster.goalPosition);
		}	
	}

	public override void animateAttack(){
		if (fighterNetworkScript != null) {
//			this would be skill3 but we have a limited set of animations available atm
			//fighterNetworkScript.onAttackTriggered("activeSkill3");
			fighterNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
