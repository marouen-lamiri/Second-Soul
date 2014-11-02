using UnityEngine;
using System.Collections;
using System;

public class FighterNetworkScript : MonoBehaviour {

	PlayerHealthBar playerHealthBar;
	double healthInPreviousFrame;
	Fighter fighter;

	
	// Use this for initialization
	void Start () {
		//playerHealthBar = (PlayerHealthBar) GameObject.FindObjectOfType (typeof(PlayerHealthBar));
		fighter = (Fighter)gameObject.GetComponent<Fighter> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Networking: setting up an eventListener for watching changes to player health:
		if(healthInPreviousFrame != fighter.health) {
			healthInPreviousFrame = fighter.health;
			onHealthPointsChanged(fighter.health);
		}

		
		// for networking:
		//fighterNetworkScript = (FighterNetworkScript)player.GetComponent<FighterNetworkScript> ();
		//for networking:
		//FighterNetworkScript fighterNetworkScript;


	}

	// watch respawn:
	[RPC]
	public void onRespawn() {
		networkView.RPC("setToRespawned", RPCMode.Others);
	}
	
	[RPC]
	void setToRespawned() {
		GameOver gameOver = (GameOver) GameObject.FindObjectOfType (typeof(GameOver));
		gameOver.Respawn ();
	}

	// watch player's attack movement:
	[RPC]
	public void onAttackTriggered(string attackName) {
		networkView.RPC("attackWithActiveSkill", RPCMode.Others, attackName+"");
	}
	[RPC]
	void attackWithActiveSkill(string attackName) {
		if(attackName == "activeSkill1") {
			//			fighter.activeSkill1.caster.animateAttack();
			//			fighter.activeSkill1.setCaster(this);
			//			fighter.activeSkill1.useSkill(target);
			fighter.animateAttack();
		} else if (attackName == "activeSkill2") {
			//			fighter.activeSkill2.setCaster(this);
			//			fighter.activeSkill2.useSkill(castPosition()); // TODO consider making a helper that calls these two, within Player.cs
			fighter.animateAttack();
		}
	}

	// watch player health:
	[RPC]
	private void onHealthPointsChanged(double healthValue) {
		networkView.RPC("changeHealthPoints", RPCMode.Others, healthValue+"");
	}

	[RPC]
	void changeHealthPoints(string healthValue) {
		fighter.health = Convert.ToDouble(healthValue);
	}

//	// use networkView.isMine 
//	
//	// this is spectating code, can go in both server and client cube/sphere code:
//	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
//	{
//		if (stream.isWriting) {
//			Vector3 pos = transform.position;
//			stream.Serialize (ref pos);
//		}
//		else {
//			Vector3 receivedPosition = Vector3.zero;
//			stream.Serialize(ref receivedPosition);
//			transform.position = receivedPosition;
//		}
//	}

}
