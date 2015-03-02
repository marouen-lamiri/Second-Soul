using UnityEngine;
using System.Collections;

public class GolemAnimationManager : MonoBehaviour {

	Animator animator;
	Golem golem;
	BasicMelee attack;
	string attackOne;
	string attackTwo;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		golem = GetComponentInParent<Golem> ();
		attack = golem.GetComponent<BasicMelee> ();
		attackOne = "Attack1";
		attackTwo = "Attack2";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (golem == null) {
			return;
		}
		animator.SetBool (AnimationParameters.hasAggro.ToString (), golem.hasAggro);
		animator.SetBool (AnimationParameters.isDead.ToString (), golem.isDead());
		animator.SetBool (AnimationParameters.moving.ToString (), golem.moving);
		animator.SetBool (AnimationParameters.attacking.ToString(), golem.inAttackRange());
		//animator.animation [attackOne].speed = golem.attackSpeed;
		//animator.animation [attackTwo].speed = golem.attackSpeed;
	//	attack.skillLength = animator.animation [attackOne].length;
	}
}

enum AnimationParameters{

	hasAggro,
	isDead,
	moving,
	attacking
}
