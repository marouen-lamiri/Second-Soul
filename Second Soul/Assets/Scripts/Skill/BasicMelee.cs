using UnityEngine;
using System.Collections;

public class BasicMelee : BasicAttack {

	private FighterNetworkScript fighterNetworkScript;

	// Use this for initialization
	void Start () {
		//skillStart ();
		fighterNetworkScript = null;
	}
	
	public override void skillStart(){
		if(fighterNetworkScript != null){
			fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
		}
		skillLength = 1/caster.attackSpeed;
		impactTime = .38f;//this is subjective. the percentage of time in the animation in which it "looks" like damage was dealt
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
