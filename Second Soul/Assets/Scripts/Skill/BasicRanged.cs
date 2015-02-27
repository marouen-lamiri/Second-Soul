using UnityEngine;
using System.Collections;

public class BasicRanged : BasicAttack {

	protected SorcererNetworkScript sorcererNetworkScript;

	// Use this for initialization
	void Start () {
		//skillStart ();
		sorcererNetworkScript = null;
	}
	
	public override void skillStart(){
		if (sorcererNetworkScript != null){
			sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
		}
		impactTime = 1/caster.castSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void animateAttack(){
		if (sorcererNetworkScript != null) {
			sorcererNetworkScript.onAttackTriggered("activeSkill1");
		}	
	}
}
