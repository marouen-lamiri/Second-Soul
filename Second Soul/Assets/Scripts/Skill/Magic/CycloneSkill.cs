using UnityEngine;
using System.Collections;

public class CycloneSkill : AreaRangedSkill {

	public CycloneBehavior cyclonePrefab;


	// Use this for initialization
	void Start () {
		skillStart ();
		damageModifier = 2f;
		damageType = DamageType.Physical;
		energyCost = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill(){
		rayCast ();
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;
		
		transform.LookAt (targetPosition);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = castTime;
		StartCoroutine(shootCyclone());
	}

	IEnumerator shootCyclone(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			CycloneBehavior cyclone = Network.Instantiate(cyclonePrefab, targetPosition, caster.transform.rotation, 4)as CycloneBehavior;
			cyclone.startBehaviour(caster,this);
		}
	}

	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
