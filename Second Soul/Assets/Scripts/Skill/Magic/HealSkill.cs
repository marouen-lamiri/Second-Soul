using UnityEngine;
using System.Collections;

public class HealSkill : TargetedRangedSkill {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void useSkill(){

	}

	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
