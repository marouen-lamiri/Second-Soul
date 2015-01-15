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
		networkView.RPC("togglePauseGame", RPCMode.Others);
	}
	
	[RPC]
	void togglePauseGame() {
		Pausing pausingObject = (Pausing) GameObject.FindObjectOfType (typeof(Pausing))as Pausing;
		pausingObject.Pause ();
	}


	// watch attack movement:
	[RPC]
	public void onAttackTriggered(string attackName) {
		networkView.RPC ("attackWithActiveSkill", RPCMode.Others, attackName + "");
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
		networkView.RPC ("changeHealthPoints", RPCMode.Others, healthValue + "");
	}
	
	[RPC]
	void changeHealthPoints(string healthValue) {
		characterScript.health = Convert.ToDouble(healthValue);
	}

	// watch player energy:
	[RPC]
	protected void onEnergyPointsChanged(double energyValue) {
		networkView.RPC ("changeEnergyPoints", RPCMode.Others, energyValue + "");
	}
	
	[RPC]
	void changeEnergyPoints(string energyValue) {
		characterScript.energy = Convert.ToDouble(energyValue);
	}

	// watch player's idle anim:
	[RPC]
	public void onIdleTriggered() {
		networkView.RPC ("triggerIdleAnim", RPCMode.Others);
	}
	[RPC]
	void triggerIdleAnim() {
		characterScript.animateIdle ();
	}
	
	// watch player's run anim:
	[RPC]
	public void onRunTriggered() {
		networkView.RPC ("triggerRunAnim", RPCMode.Others);
	}
	[RPC]
	void triggerRunAnim() {
		characterScript.animateRun ();
	}
}
