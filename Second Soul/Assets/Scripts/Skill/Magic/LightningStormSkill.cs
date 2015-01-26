using UnityEngine;
using System.Collections;

public class LightningStormSkill : AreaRangedSkill {

	public LightningStormBehavior lightningStormPrefab;
	// Use this for initialization
	void Start () {
		skillStart ();
		damageModifier = 3f;
		damageType = DamageType.Lightning;
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
		StartCoroutine(shootLightningStorm());
	}

	IEnumerator shootLightningStorm(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			LightningStormBehavior lightningStorm = Network.Instantiate(lightningStormPrefab, targetPosition, caster.transform.rotation, 4)as LightningStormBehavior;
			lightningStorm.startBehaviour(caster,this);
		}
	}

	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
