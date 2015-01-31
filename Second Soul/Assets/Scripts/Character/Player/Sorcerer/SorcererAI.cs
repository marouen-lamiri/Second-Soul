using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererAI : MonoBehaviour {

	public CharacterController controller;
	private Fighter fighter;
	private Sorcerer sorcerer;
	private Vector3 goalPosition;
	private Enemy nearestEnemy;

	// Use this for initialization
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movingAI();
	}

	void movingAI(){
		if(Network.isClient != true){
			int randomTech = Random.Range(1,50);
			nearestEnemy = checkNearestEnemy();
			if(Vector3.Distance (fighter.transform.position, nearestEnemy.transform.position) < 5 && !(nearestEnemy.isDead()) && Vector3.Distance (fighter.transform.position, sorcerer.transform.position) < 5 ){
				sorcerer.stopMoving();
				if(randomTech <= 46){sorcerer.activeSkill1.useSkill();}
				if(randomTech == 47 && fighter.energy > 20){sorcerer.activeSkill2.useSkill();}
				if(randomTech == 48 && fighter.energy > 20){sorcerer.activeSkill3.useSkill();}
				if(randomTech == 49 && fighter.energy > 20){sorcerer.activeSkill4.useSkill();}
				if(randomTech == 50 && fighter.energy > 20){sorcerer.activeSkill5.useSkill();}
				if(fighter.health < fighter.maxHealth * 0.50 && fighter.energy >= 10){sorcerer.activeSkill6.useSkill();}

			}
			else if(!sorcerer.moving && Vector3.Distance (fighter.transform.position, sorcerer.transform.position)>=7){
				goalPosition = new Vector3(Random.Range (-5,5), 0, Random.Range (-5,5));
				sorcerer.startMoving(fighter.transform.position + goalPosition);
			}
			else if (sorcerer.transform.position == goalPosition){
				sorcerer.stopMoving();
			}
		}
	}

	Enemy checkNearestEnemy(){
		float nearestDistanceSqr = Mathf.Infinity;
		GameObject[] allEnemies =  GameObject.FindGameObjectsWithTag("Enemy");
		Enemy[] taggedEnemy = new Enemy[allEnemies.Length];
		Enemy nearestObj = null;

		for (int i = 0;i < allEnemies.Length;i++) {
			taggedEnemy[i] = allEnemies[i].GetComponent("Enemy") as Enemy;
		}

		foreach (Enemy enemy in taggedEnemy){
			
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
