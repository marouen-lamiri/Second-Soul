using UnityEngine;
using System.Collections;

public class FireballSkill : ProjectileSkill {

	public FireballBehavior fireballPrefab;

	public float AOEDamage;
	float AOEDamageModifier;

	// Use this for initialization
	void Start () {
		spawnDistance = 2f;
		travelDistance = 10f;
		damageModifier = 2f;
		AOEDamageModifier = 0.5f;
		speed = 10f;
		damage = caster.getDamage() * damageModifier;
		AOEDamage = (float)damage * AOEDamageModifier;
		damageType = DamageType.Fire;

		energyCost = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill (Vector3 target)
	{
		base.useSkill (target);
		
		castTime = 0.35f;
		skillLength = animation[caster.attackClip.name].length;
		damage = caster.getDamage() * damageModifier;
		
		transform.LookAt (target);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		
		StartCoroutine(shootFireball(target));
	}
	
	IEnumerator shootFireball(Vector3 target){
		yield return new WaitForSeconds(skillLength * castTime);
		caster.loseEnergy (energyCost);
		FireballBehavior fireball = Instantiate(fireballPrefab, caster.transform.position + spawnDistance * caster.transform.forward, caster.transform.rotation)as FireballBehavior;
		fireball.fireballSkill = this;
	}
}
