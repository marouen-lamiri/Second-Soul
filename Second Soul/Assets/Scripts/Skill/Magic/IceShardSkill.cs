using UnityEngine;
using System.Collections;

public class IceShardSkill : ProjectileSkill {

	IceShardBehavior icePrefab;

	
	// Use this for initialization
	void Start () {
		skillStart ();
		//I do this because caster is not being set for some reason
		caster = gameObject.GetComponent<Character> ();
		spawnDistance = 2f;
		travelDistance = 10f;
		damageModifier = 4f;
		speed = 20f;
		damageType = DamageType.Ice;
		
		energyCost = 20;

		icePrefab = (IceShardBehavior) Resources.Load ("Assets/Prefabs/skills/Elementals/Prefabs/Ice/IceShard");
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
		castTime = caster.castSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;
		
		transform.LookAt (targetPosition);
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = castTime;
		StartCoroutine(shootIceShard());
	}
	
	IEnumerator shootIceShard(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			IceShardBehavior iceShard = Network.Instantiate(icePrefab, caster.transform.position + new Vector3(0,1,0) + (spawnDistance * caster.transform.forward), caster.transform.rotation, 4)as IceShardBehavior;
			iceShard.startBehaviour (caster, this);
		}
	}
	
	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
