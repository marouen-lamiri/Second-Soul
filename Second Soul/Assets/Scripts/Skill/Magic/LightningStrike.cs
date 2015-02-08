using UnityEngine;
using System.Collections;

public class LightningStrike : TargetedRangedSkill {
	
	GameObject lightningStrikePrefab;
	// Use this for initialization
	void Start () {
		skillStart ();
		damageModifier = 4f;
		damageType = DamageType.Lightning;
		energyCost = 20;

		lightningStrikePrefab = (GameObject) Resources.Load ("Assets/Prefabs/skills/Elementals/Prefabs/Thunder/Thunder");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void useSkill(){
		rayCast ();
		if (targetCharacter == null) {
			return;
		}
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;
		
		transform.LookAt (targetCharacter.transform.position);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = castTime;
		StartCoroutine(shootLightningStrike());
	}
	
	IEnumerator shootLightningStrike(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			Network.Instantiate(lightningStrikePrefab, targetCharacter.transform.position, caster.transform.rotation, 4);
			targetCharacter.takeDamage(damage,damageType);
		}
	}
	
	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
