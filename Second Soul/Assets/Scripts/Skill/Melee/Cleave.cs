using UnityEngine;
using System.Collections;

public class Cleave : AreaMeleeSkill {

	// Use this for initialization
	void Start () {
		skillStart ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public override void useSkill(){
		
	}


	public override void animateAttack(){
		if (fighterNetworkScript != null) {
			fighterNetworkScript.onAttackTriggered("activeSkill3");
		}	
	}
}
