using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererAI : Sorcerer {

	public CharacterController controller;
	public ISkill skillArray;
	private Fighter fighter;
	private Sorcerer sorcerer;
	private Vector3 goalPosition;
	private Enemy nearestEnemy;
	private float timeOut;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		timeOut = 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(grid != null){
			movingAI();
			timeOut -= Time.deltaTime;
		}
	}

	void movingAI(){
		// if in one player mode, i.e. no client is connected, server playing alone, run the sorcerer AI code:
		if(Network.connections.Length == 0){ //if(Network.isClient != true){
			int randomTech = Random.Range(0,200);
			nearestEnemy = checkNearestEnemy();
			if(nearestEnemy != null && Vector3.Distance (fighter.transform.position, nearestEnemy.transform.position) < 5 && timeOut <= 0 &&  !(nearestEnemy.isDead()) && Vector3.Distance (fighter.transform.position, sorcerer.transform.position) < 10 ){
				sorcerer.stopMoving();
				if(randomTech <= 180 && timeOut <= 0){
//					sorcerer.activeSkill1.useSkill();
					timeOut = 2f;
				}
				if((randomTech == 193 || randomTech == 194) && fighter.energy > 20 && timeOut <= 0){
//					sorcerer.activeSkill2.useSkill();
					timeOut = 4f;
				}
				if((randomTech == 195 || randomTech == 196) && fighter.energy > 20 && timeOut <= 0){
//					sorcerer.activeSkill3.useSkill();
					timeOut = 4f;
				}
				if((randomTech == 197 || randomTech == 198) && fighter.energy >= 20 && timeOut <= 0){
//					sorcerer.activeSkill4.useSkill();
					timeOut = 4f;
				}
				if((randomTech == 199 || randomTech == 200) && fighter.energy >= 20 && timeOut <= 0){
//					sorcerer.activeSkill5.useSkill();
					timeOut = 4f;
				}
				if(fighter.health < fighter.maxHealth * 0.50 && fighter.energy >= 10 && (randomTech > 180 && randomTech < 192) && timeOut <= 0){
//					sorcerer.activeSkill6.useSkill();
					timeOut = 2f;
				}
			}
			else if(!sorcerer.moving && Vector3.Distance (fighter.transform.position, sorcerer.transform.position)>=10){
				//Determine the direction in which the sorcerer moves, he has to be close to the fighter
				direction = sorcerer.transform.position - fighter.transform.position;
				direction.Normalize();
				goalPosition = direction;
				sorcerer.startMoving(fighter.transform.position + goalPosition);
			}
			else if (Vector3.Distance(sorcerer.transform.position,goalPosition) < 2){
				sorcerer.stopMoving();
			}
		}
	}

	public Enemy checkNearestEnemy(){
		float nearestDistanceSqr = Mathf.Infinity;
		Enemy[] allEnemies =  GameObject.FindObjectsOfType<Enemy>();
		Enemy nearestObj = null;


		foreach (Enemy enemy in allEnemies){
			
			Vector3 objectPos = enemy.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;
			
			if (distanceSqr < nearestDistanceSqr) {
				nearestObj = enemy;
				nearestDistanceSqr = distanceSqr;
			}
		}
		//Debug.Log (nearestObj);
		return nearestObj;
	}
}
