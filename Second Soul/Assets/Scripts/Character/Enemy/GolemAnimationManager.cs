using UnityEngine;
using System.Collections;

public class GolemAnimationManager : MonoBehaviour {

	Animator animator;
	Golem golem;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		golem = GetComponentInParent<Golem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetFloat (AnimationParameters.attackSpeed.ToString(), golem.attackSpeed);
		animator.SetFloat (AnimationParameters.moveSpeed.ToString (), golem.speed);
		animator.SetBool (AnimationParameters.hasAggro.ToString (), golem.hasAggro);
		//animator.SetBool (AnimationParameters.isDead.ToString (), golem.isDead);
	}
}

enum AnimationParameters{

	attackSpeed,
	hasAggro,
	moveSpeed,
	isDead
}
