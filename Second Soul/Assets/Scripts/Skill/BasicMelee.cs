using UnityEngine;
using System.Collections;

public class BasicMelee : BasicAttack {

	private FighterNetworkScript fighterNetworkScript;

	// Use this for initialization
	void Start () {
		//skillStart ();
	}
	
	public override void skillStart(){
		fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
		impactTime = 1/caster.attackSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void animateAttack(){
		if (fighterNetworkScript != null) {
			fighterNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
