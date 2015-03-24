using UnityEngine;
using System.Collections;

public class FireballSkill : ProjectileSkill {

	FireballBehavior fireballPrefab;

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

		fireballPrefab = (FireballBehavior)Resources.Load ("Prefabs/skills/Fire/Fireball",typeof(FireballBehavior));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill ()
	{	
		caster.stopMoving();
		if (caster.GetType ().IsSubclassOf (typeof(Enemy))) {
			targetPosition = caster.target.transform.position;
		}
		else if(caster.GetType().IsSubclassOf(typeof(Player))){
			rayCast ();

		}
		caster.attacking = true;
		damage = caster.spellPower * damageModifier;
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;

		caster.transform.LookAt (targetPosition);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		if (caster.attackClip != null) {
			animation [caster.attackClip.name].normalizedSpeed = castTime;
		}
		StartCoroutine(shootFireball());
	}

	IEnumerator shootFireball(){
		yield return new WaitForSeconds(skillLength);
		if(caster!=null && !caster.isDead() && caster.loseEnergy (energyCost)){
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
