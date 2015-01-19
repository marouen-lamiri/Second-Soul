using UnityEngine;
using System.Collections;
using System;

public class SorcererNetworkScript : CharacterNetworkScript {

	Fighter fighterScript;

	// Use this for initialization
	void Start () {
		//playerHealthBar = (PlayerHealthBar) GameObject.FindObjectOfType (typeof(PlayerHealthBar));
		setCharacterScript ();

	}
	
	// Update is called once per frame
	void Update () {
		watchCharacterHealth ();
		
	}

	protected override void setCharacterScript() {
		characterScript = (Sorcerer)gameObject.GetComponent<Sorcerer> ();
		Fighter fighter = (Fighter)GameObject.FindObjectOfType (typeof(Fighter))as Fighter;
		fighterScript = fighter.GetComponent<Fighter> ();	
	}
	

	// watch energy:
	[RPC]
	public void onEnergyLost(double energyValue) {
		networkView.RPC("setEnergy", RPCMode.Others, energyValue+"");
	}
	
	[RPC]
	void setEnergy(string energyValue) {
		fighterScript.energy = Convert.ToDouble (energyValue); // yes, fighter --> because the sorcerer doesn't have any energy, it's all held by the fighter.
	}
	

}
