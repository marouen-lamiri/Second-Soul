﻿using UnityEngine;
using System.Collections;
using System;

public class FighterNetworkScript : CharacterNetworkScript {


	// Use this for initialization
	void Start () {
		setCharacterScript ();
		characterScript = (Fighter)gameObject.GetComponent<Fighter> ();
	}
	
	// Update is called once per frame
	void Update () {
		watchCharacterHealth ();
		watchCharacterEnergy ();
	}

	protected override void setCharacterScript() {
		characterScript = (Fighter)gameObject.GetComponent<Fighter> ();
	}


	// watch energy:
	[RPC]
	public void onEnergyLost(double energyValue) {
		networkView.RPC("setEnergy", RPCMode.Others, energyValue+"");
	}
	
	[RPC]
	void setEnergy(string energyValue) {
		characterScript.energy = Convert.ToDouble (energyValue);
	}


	// watch onStatsDisplayed:
	[RPC]
	public void onStatsDisplayed() {
		try {
			networkView.RPC ("toggleStatsDisplayed", RPCMode.Others);
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	
	[RPC]
	void toggleStatsDisplayed() {
		DisplayPlayerStats statsDisplayScript = (DisplayPlayerStats) GameObject.FindObjectOfType(typeof(DisplayPlayerStats));
		statsDisplayScript.boolChange ();
	}

	// watch respawn:
	[RPC]
	public void onRespawn() {
		networkView.RPC ("setToRespawned", RPCMode.Others);
	}
	
	[RPC]
	void setToRespawned() {
		GameOver gameOver = (GameOver) GameObject.FindObjectOfType (typeof(GameOver));
		gameOver.Respawn ();
	}


}
