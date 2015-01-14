using UnityEngine;
using System.Collections;
using System;

public class EnemyNetworkScript : MonoBehaviour {
	
	//EnemyHealthBar enemyHealthBar;
	double healthInPreviousFrame;
	Enemy enemyScript;
	//Pausing pausingObject;

	// Use this for initialization
	void Start () {
		//playerHealthBar = (PlayerHealthBar) GameObject.FindObjectOfType (typeof(PlayerHealthBar));
		enemyScript = (Enemy)gameObject.GetComponent<Enemy> ();
		//pausingObject = (Pausing) GameObject.FindObjectOfType (typeof(Pausing));
	}
	
	// Update is called once per frame
	void Update () {
		// Networking: setting up an eventListener for watching changes to player health:
		if(healthInPreviousFrame != enemyScript.health) {
			healthInPreviousFrame = enemyScript.health;
			onHealthPointsChanged(enemyScript.health);
		}
		
		
		// for networking:
		//fighterNetworkScript = (FighterNetworkScript)player.GetComponent<FighterNetworkScript> ();
		//for networking:
		//FighterNetworkScript fighterNetworkScript;
		
		
	}
	
	// watch pausing game:
//	[RPC]
//	public void onPauseGame() {
//		if(networkView.isMine){
//			networkView.RPC("togglePauseGame", RPCMode.Others);
//		}
//	}
//	
//	[RPC]
//	void togglePauseGame() {
//		//pausingObject.Pause ();
//	}
	
//	// watch energy:
//	[RPC]
//	public void onEnergyLost(double energyValue) {
//		if(networkView.isMine){
//			networkView.RPC("setEnergy", RPCMode.Others, energyValue+"");
//		}
//	}
//	
//	[RPC]
//	void setEnergy(string energyValue) {
//		enemyScript.energy = Convert.ToDouble (energyValue);
//	}
	
	
//	// watch onStatsDisplayed:
//	[RPC]
//	public void onStatsDisplayed() {
//		if(networkView.isMine){
//			networkView.RPC("toggleStatsDisplayed", RPCMode.Others);
//		}
//	}
//	
//	[RPC]
//	void toggleStatsDisplayed() {
//		DisplayPlayerStats statsDisplayScript = (DisplayPlayerStats) GameObject.FindObjectOfType(typeof(DisplayPlayerStats));
//		statsDisplayScript.boolChange ();
//	}
	
//	// watch respawn:
//	[RPC]
//	public void onRespawn() {
//		if (networkView.isMine) {
//			networkView.RPC ("setToRespawned", RPCMode.Others);
//		}
//	}
//	
//	[RPC]
//	void setToRespawned() {
//		GameOver gameOver = (GameOver) GameObject.FindObjectOfType (typeof(GameOver));
//		gameOver.Respawn ();
//	}
	
	// watch enemiy's attack movement:
	[RPC]
	public void onAttackTriggered(string attackName) {
		if (networkView.isMine) {
			networkView.RPC ("attackWithActiveSkill", RPCMode.Others, attackName + "");
		}
	}
	[RPC]
	void attackWithActiveSkill(string attackName) {
		if(attackName == "activeSkill1") {
			//			fighter.activeSkill1.caster.animateAttack();
			//			fighter.activeSkill1.setCaster(this);
			//			fighter.activeSkill1.useSkill(target);
			enemyScript.animateAttack();
		} else if (attackName == "activeSkill2") {
			//			fighter.activeSkill2.setCaster(this);
			//			fighter.activeSkill2.useSkill(castPosition()); // TODO consider making a helper that calls these two, within Player.cs
			enemyScript.animateAttack();
		} else {
			enemyScript.animateAttack();
		}
	}
	
	// watch player health:
	[RPC]
	private void onHealthPointsChanged(double healthValue) {
		networkView.RPC ("changeHealthPoints", RPCMode.Others, healthValue + "");
	}
	
	[RPC]
	void changeHealthPoints(string healthValue) {
		enemyScript.health = Convert.ToDouble(healthValue);
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
