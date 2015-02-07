using UnityEngine;
using System.Collections;

public class FireballSkill : ProjectileSkill {

	public FireballBehavior fireballPrefab;

	public float AOEDamage;
	float AOEDamageModifier;

	// Use this for initialization
	void Start () {
		skillStart ();
		spawnDistance = 2f;
		travelDistance = 10f;
		damageModifier = 2f;
		AOEDamageModifier = 0.5f;
		speed = 15f;
		
		AOEDamage = (float)damage * AOEDamageModifier;
		damageType = DamageType.Fire;

		energyCost = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill ()
	{	
		rayCast ();
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}
		damage = caster.spellPower * damageModifier;
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;
		
		caster.transform.LookAt (targetPosition);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = castTime;
		StartCoroutine(shootFireball());
	}

	IEnumerator shootFireball(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			FireballBehavior fireball = Network.Instantiate(fireballPrefab, caster.transform.position + new Vector3(0,1,0) + (spawnDistance * caster.transform.forward), caster.transform.rotation, 4)as FireballBehavior;
			fireball.startBehaviour (caster, this);
		}
	}

	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
