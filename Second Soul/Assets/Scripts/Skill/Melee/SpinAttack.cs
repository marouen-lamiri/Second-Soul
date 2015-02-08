using UnityEngine;
using System.Collections;

public class SpinAttack : AreaMeleeSkill {

	GameObject spinAttackPrefab;
	private float range;
	// Use this for initialization
	void Start () {
		skillStart ();
		damageModifier = 3f;
		damageType = DamageType.Physical;
		energyCost = 20;
		range = caster.attackRange * 3;

		spinAttackPrefab = (GameObject)Resources.Load ("Assets/Prefabs/skills/Elementals/Prefabs/Wind/SpinAttack");
	}

	// Update is called once per frame
	void Update () {
	
	}

	
	public override void useSkill(){
		if(caster.GetType().IsSubclassOf(typeof(Player))){
			Player player = (Player)caster;
			player.stopMoving ();
		}
		castTime = caster.attackSpeed;
		skillLength = 1/castTime;
		damage = caster.getDamage() * damageModifier;
		caster.animateAttack();
		caster.skillDurationLeft = skillLength;
		animation [caster.attackClip.name].normalizedSpeed = skillLength;
		StartCoroutine(spin());
	}

	IEnumerator spin(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			Network.Instantiate(spinAttackPrefab, caster.transform.position, caster.transform.rotation, 4);
			dealDamage();
		}
	}

	public void dealDamage(){
		Collider[] hitColliders = Physics.OverlapSphere (caster.transform.position, range);
		for(int i = 0; i< hitColliders.Length; ++i){
			Character character = hitColliders[i].gameObject.GetComponent<Character>();
			if(character!=null && caster.GetType().IsSubclassOf(typeof(Player))&&(character.GetType().IsSubclassOf(typeof(Enemy)) || character.GetType()==typeof(Enemy))){
				character.takeDamage (damage, damageType);
			}
		}
	}

	public override void animateAttack(){
		if (fighterNetworkScript != null) {
			fighterNetworkScript.onAttackTriggered("activeSkill3");
		}	
	}
}
