using UnityEngine;
using System.Collections;

public class Charge : TargetedMeleeSkill {

	private float range;
	private float originalSpeed;
	private float chargeSpeed;
	private bool charging;
	private float damageModifier;

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
		damageModifier = 2f;
		range = caster.attackRange * 5;
		originalSpeed = caster.speed;
		chargeSpeed = originalSpeed * 5;
		energyCost = 10;
		charging = true;
	}
	// Update is called once per frame
	void Update () {
		damage = caster.getDamage() * damageModifier;
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
		if (caster.target != null && caster.loseEnergy(energyCost)) {
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
