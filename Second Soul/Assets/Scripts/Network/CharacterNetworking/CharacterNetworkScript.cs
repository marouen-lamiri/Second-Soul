using UnityEngine;
using System.Collections;
using System;

public abstract class CharacterNetworkScript : MonoBehaviour {

	protected Character characterScript;
	protected double healthInPreviousFrame;
	protected double energyInPreviousFrame;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//watchCharacterhealth (); // has to be done in each update method of each subclasses
	}

	protected abstract void setCharacterScript ();

	protected void watchCharacterHealth() {
		// Networking: setting up an eventListener for watching changes to player health:
		if(healthInPreviousFrame != characterScript.health) {
			healthInPreviousFrame = characterScript.health;
			onHealthPointsChanged(characterScript.health);
		}
	}
	
	protected void watchCharacterEnergy(){
		// Networking: setting up an eventListener for watching changes to player health:
		if(energyInPreviousFrame != characterScript.energy) {
			energyInPreviousFrame = characterScript.energy;
			onEnergyPointsChanged(characterScript.energy);
		}
	}

	// watch pausing game:
	[RPC]
	public void onPauseGame() {
		try {
			networkView.RPC ("togglePauseGame", RPCMode.Others);
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	
	[RPC]
	void togglePauseGame() {
		Pausing pausingObject = (Pausing) GameObject.FindObjectOfType (typeof(Pausing))as Pausing;
		pausingObject.Pause ();
	}


	// watch attack movement:
	[RPC]
	public void onAttackTriggered(string attackName) {
		try {
			networkView.RPC ("attackWithActiveSkill", RPCMode.Others, attackName + "");
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	[RPC]
	void attackWithActiveSkill(string attackName) {
		if(attackName == "activeSkill1") {
			characterScript.animateAttack();
		} else if (attackName == "activeSkill2") {
			characterScript.animateAttack();
		} else {
			characterScript.animateAttack();
		}
	}
		
	// watch player health:
	[RPC]
	protected void onHealthPointsChanged(double healthValue) {
		try {
			networkView.RPC ("changeHealthPoints", RPCMode.Others, healthValue + "");
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	
	[RPC]
	void changeHealthPoints(string healthValue) {
		characterScript.health = Convert.ToDouble(healthValue);
	}

	// watch player energy:
	[RPC]
	protected void onEnergyPointsChanged(double energyValue) {
		try {
			networkView.RPC ("changeEnergyPoints", RPCMode.Others, energyValue + "");
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	
	[RPC]
	void changeEnergyPoints(string energyValue) {
		characterScript.energy = Convert.ToDouble(energyValue);
	}

	// watch player's idle anim:
	[RPC]
	public void onIdleTriggered() {
		try {
			networkView.RPC ("triggerIdleAnim", RPCMode.Others);
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	[RPC]
	void triggerIdleAnim() {
		characterScript.animateIdle ();
	}
	
	// watch player's run anim:
	[RPC]
	public void onRunTriggered() {
		try {
			networkView.RPC ("triggerRunAnim", RPCMode.Others);
		} catch (Exception ex) {
			//print (ex);
			print ("... error is normal if in one player mode.");
		}
	}
	[RPC]
	void triggerRunAnim() {
		characterScript.animateRun ();
	}
}
