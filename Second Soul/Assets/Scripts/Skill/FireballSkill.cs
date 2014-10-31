using UnityEngine;
using System.Collections;

public class FireballSkill : ProjectileSkill {

	public FireballBehavior fireballPrefab;

	float damageModifier;
	// Use this for initialization
	void Start () {
		spawnDistance = 2f;
		travelDistance = 10f;
		damageModifier = 1.5f;
		speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill (Vector3 target)
	{
		base.useSkill (target);
		
		castTime = 0.35f;
		skillLength = animation[caster.attackClip.name].length;
		damage = caster.damage * damageModifier;
		
		transform.LookAt (target);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		
		StartCoroutine(shootFireball(target));
	}
	
	IEnumerator shootFireball(Vector3 target){
		yield return new WaitForSeconds(skillLength * castTime);
		
		FireballBehavior fireball = Instantiate(fireballPrefab, caster.transform.position + spawnDistance * caster.transform.forward, caster.transform.rotation)as FireballBehavior;
		fireball.fireballSkill = this;
	}
}
