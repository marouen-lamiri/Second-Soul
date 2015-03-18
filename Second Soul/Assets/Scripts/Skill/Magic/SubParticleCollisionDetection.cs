using UnityEngine;
using System.Collections;

public class SubParticleCollisionDetection : MonoBehaviour {

	Character caster;
	System.Type casterType;
	RangedSkill skill;
	// Use this for initialization
	void Start () {
		skill = gameObject.transform.parent.gameObject.GetComponent<ParticleBehavior> ().skill;
		caster = skill.caster;
		casterType = caster.gameObject.GetComponent<Character> ().GetType ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject obj){
		Character character = obj.GetComponent<Character> ();
		if (character == null) {
			return;
		}
		//		this means that if the caster is a player, and the skill hit an enemy
		if (casterType.IsSubclassOf (typeof(Player))){
			if( !character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (skill.damage, skill.damageType);
			}
		} 
		else {
			if( character.GetType ().IsSubclassOf (typeof(Player))){
				character.takeDamage (skill.damage, skill.damageType);
			}
		}
	}
}
