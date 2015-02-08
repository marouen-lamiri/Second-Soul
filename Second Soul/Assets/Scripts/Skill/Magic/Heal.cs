using UnityEngine;
using System.Collections;

public class Heal : TargetedRangedSkill {

	GameObject healPrefab;
	// Use this for initialization
	void Start () {
		damageModifier = 2.5f;
		damageType = DamageType.Physical;
		energyCost = 20;

		healPrefab = (GameObject)Resources.Load ("Prefabs/skills/Light/Healing", typeof(GameObject));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void useSkill(){
		rayCast ();
		if (targetCharacter != null) {
			if (caster.GetType ().IsSubclassOf (typeof(Player))) {
				Player player = (Player)caster;
				player.stopMoving ();
			}
			castTime = caster.castSpeed;
			skillLength = 1 / castTime;
			damage = caster.getDamage() * damageModifier;

			transform.LookAt (targetCharacter.transform.position);
			caster.animateAttack ();
			caster.skillDurationLeft = skillLength;
			animation [caster.attackClip.name].normalizedSpeed = castTime;
			StartCoroutine (shootHeal ());
		}
	}
		
	IEnumerator shootHeal(){
		yield return new WaitForSeconds(skillLength);
		if(caster.loseEnergy (energyCost)){
			Network.Instantiate(healPrefab, targetCharacter.transform.position, caster.transform.rotation, 4);
			targetCharacter.healCharacter(damage);

		}
	}

	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
	//heal works differently from the other skills, since it heals an ally, rather than deals damage to an enemy, therefore rayCast must be overridden
	public override void rayCast(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);
		
		for (int i = 0; i < hits.Length; ++i) {
			GameObject hit = hits[i].collider.gameObject;
			if(hit.GetComponent<Character>()!=null && hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Fighter))){
				targetCharacter = hit.GetComponent<Fighter>();
				return;
			}
			else if( hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Sorcerer))){
				targetCharacter = hit.GetComponent<Sorcerer>().getFighter();
				return;
			}
		}
	}
}
