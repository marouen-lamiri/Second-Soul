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
		speed = 15f;
		damage = caster.spellPower * damageModifier;
		AOEDamage = (float)damage * AOEDamageModifier;
		damageType = DamageType.Fire;

		energyCost = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void useSkill (Vector3 targetPosition, Character targetCharacter)
	{	
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.spellPower * damageModifier;
		
		transform.LookAt (targetPosition);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = castTime;
		StartCoroutine(shootFireball());
	}
	
	IEnumerator shootFireball(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			FireballBehavior fireball = Network.Instantiate(fireballPrefab, caster.transform.position + spawnDistance * caster.transform.forward, caster.transform.rotation, 4)as FireballBehavior;
		}
	}
}
