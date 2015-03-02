using UnityEngine;
using System.Collections;

public class ImpAnimationManager : MonoBehaviour {

	Animator animator;
	Imp imp;
	BasicMelee attack;
	string attackString;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		imp = GetComponentInParent<Imp> ();
		attack = imp.GetComponent<BasicMelee> ();
		attackString = "Spell";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (imp == null) {
			return;
		}
		animator.SetBool (AnimationParameters.hasAggro.ToString (), imp.hasAggro);
		animator.SetBool (AnimationParameters.isDead.ToString (), imp.isDead());
		animator.SetBool (AnimationParameters.moving.ToString (), imp.moving);
		animator.SetBool (AnimationParameters.attacking.ToString(), imp.inAttackRange());
		//animator.animation [attackString].speed = imp.attackSpeed;
		//attack.skillLength = animator.animation [attackString].length;
	}
}
//This would be required if we need different animation information in the future
//enum AnimationParameters{
//	
//	hasAggro,
//	isDead,
//	moving,
//	attacking
//}
