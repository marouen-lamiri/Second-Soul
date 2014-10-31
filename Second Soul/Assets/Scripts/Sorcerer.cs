using UnityEngine;
using System.Collections;

public class Sorcerer : Player {

	//Variable declaration
	public Fighter fighter;
	
	// Use this for initialization
	void Start () {
		initializePlayer();
		activeSkill1 = (BasicRanged)controller.GetComponent<BasicRanged>();
		activeSkill2 = (FireballSkill)controller.GetComponent<FireballSkill>();
		target = null;
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		playerEnabled = !fighter.playerEnabled;
		playerLogic ();
	}
	public override bool isDead(){
		return fighter.isDead ();
	}
}
