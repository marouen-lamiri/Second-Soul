using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererAI : MonoBehaviour {
	
	private Fighter fighter;
	private Sorcerer sorcerer;
	private Grid grid;
	private Vector3 goalPosition;
	private Enemy nearestEnemy;
	private Enemy lockedOn;
	private bool status;
	private float timeOut;
	private float timeOutReset = 4f;
	private float basicTimeOutReset = 0.5f;
	private Vector3 direction;
	private ISkill skill1;
	private string skill1Name = "IceShardSkill";
	private ISkill skill2;
	private string skill2Name = "FireballSkill"; 
	private ISkill skill3;
	private string skill3Name = "LightningStormSkill";
	private ISkill skill4;
	private string skill4Name = "CycloneSkill";
	private ISkill skill5;
	private string skill5Name = "BasicRanged";
	private ISkill skill6;
	private string skill6Name = "Heal";

	// Use this for initialization
	void Start () {
		startAI ();
	}

	void startAI(){
		if(checkAIPlayingStatus()){
			fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
			setAISkillSet();
			nearestEnemy = checkNearestEnemy();
			timeOut = basicTimeOutReset;
			status = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkWhoHadControl();
		//checks that there is a grid instance (for pathfinding), if not, retrieve the grid
		if(grid == null){
			grid = (Grid)GameObject.FindObjectOfType (typeof(Grid));
		}
		if(grid != null){
			movingAI();
			timeOut -= Time.deltaTime;
		}
	}

	void checkWhoHadControl(){
		//check if the last one playing was the AI or the player and if its the player, then set the AI before continuing
		if(checkAIPlayingStatus() && status){
			//reset the AI
			startAI ();
			//set the status of the player to false (no player is in the client side)
			status = false;
		}
		else {
			//set the status to true to the player (a player is connected to the client side)
			status = true;
		}
	}

	//Checks if a player is in game or not
	bool checkAIPlayingStatus(){
		//If the network is Client, don't give control of sorcerer to the AI
		if(Network.isClient != true){
			//If the network is Not Client and the number of Connections is more than 1, than don't give control to the AI
			if(Network.connections.GetLength(0) == 1){
				return false;
			}
			//If the network is Not Client and the number of Connections is equal to 1, than give control to the AI
			return true;
		}
		return false;
	}

	//Sets the skills that the sorcerer will be using in the game
	void setAISkillSet(){
		//Retrieve the skills
		skill1 = gameObject.GetComponent(skill1Name) as ISkill;
		skill2 = gameObject.GetComponent(skill2Name) as ISkill;
		skill3 = gameObject.GetComponent(skill3Name) as ISkill;
		skill4 = gameObject.GetComponent(skill4Name) as ISkill;
		skill5 = gameObject.GetComponent(skill5Name) as ISkill;
		skill6 = gameObject.GetComponent(skill6Name) as ISkill;

		//Set the skills in the skill slots
		sorcerer.activeSkill1 = skill1;
		sorcerer.activeSkill2 = skill2;
		sorcerer.activeSkill3 = skill3;
		sorcerer.activeSkill4 = skill4;
		sorcerer.activeSkill5 = skill5;
		sorcerer.activeSkill6 = skill6;

		//Set the skills to the sorcerer
		sorcerer.activeSkill1.setCaster(sorcerer);
		sorcerer.activeSkill2.setCaster(sorcerer);
		sorcerer.activeSkill3.setCaster(sorcerer);
		sorcerer.activeSkill4.setCaster(sorcerer);
		sorcerer.activeSkill5.setCaster(sorcerer);
		sorcerer.activeSkill6.setCaster(sorcerer);
	}

	//gives the orders to the AI
	void movingAI(){
		//checks if there is a player controlling the sorcerer
		if(checkAIPlayingStatus()){
			int randomTech = Random.Range(0,200);
			nearestEnemy = checkNearestEnemy();
			//checks if there is a nearby enemy in respect to the fighter, otherwise it will ignore any enemies
			if(nearestEnemy != null && Vector3.Distance (fighter.transform.position, nearestEnemy.transform.position) < 5 && timeOut <= 0 &&  !(nearestEnemy.isDead()) && Vector3.Distance (fighter.transform.position, sorcerer.transform.position) < 10 ){
				sorcerer.stopMoving();
				//will decide what to use based on probability, which can be tweeked
				//randomTech determines the next attack
				//This represents the default attack
				if(randomTech <= 180 && timeOut <= 0){
					Debug.Log ("Attack For God's sake");
					sorcerer.activeSkill5.useSkill();
					timeOut = basicTimeOutReset;
				}
				//skill chance of happeining is 2%
				if((randomTech == 193 || randomTech == 194) && fighter.energy > 20 && timeOut <= 0){
					sorcerer.activeSkill2.useSkill();
					timeOut = timeOutReset;
				}
				//skill chance of happeining is 2%
				if((randomTech == 195 || randomTech == 196) && fighter.energy > 20 && timeOut <= 0){
					sorcerer.activeSkill3.useSkill();
					timeOut = timeOutReset;
				}
				//skill chance of happeining is 2%
				if((randomTech == 197 || randomTech == 198) && fighter.energy >= 20 && timeOut <= 0){
					sorcerer.activeSkill4.useSkill();
					timeOut = timeOutReset;
				}
				//skill chance of happeining is 2%
				if((randomTech == 199 || randomTech == 200) && fighter.energy >= 20 && timeOut <= 0){
					sorcerer.activeSkill1.useSkill();
					timeOut = timeOutReset;
				}
				//skill chance of happeining is 2%
				if(fighter.health < fighter.maxHealth * 0.50 && fighter.energy >= 10 && (randomTech > 180 && randomTech < 192) && timeOut <= 0){
					sorcerer.activeSkill6.useSkill();
					timeOut = basicTimeOutReset;
				}
			}
			//if the fighter is too far, go to him, ignore everyone else
			else if(Vector3.Distance (fighter.transform.position, sorcerer.transform.position)>=5){
				//Determine the direction in which the sorcerer moves, he has to be close to the fighter
				direction = sorcerer.transform.position - fighter.transform.position;
				direction.Normalize();
				goalPosition = direction;
				sorcerer.startMoving(fighter.transform.position + goalPosition);
			}
			//if the fighter is nearby, stop
			else if (Vector3.Distance(sorcerer.transform.position,goalPosition) < 4){
				sorcerer.stopMoving();
			}
		}
	}

//	public void setActiveSkillsForAI(SkillNode skillNode1, SkillNode skillNode2,SkillNode skillNode3,SkillNode skillNode4, SkillNode skillNode5, SkillNode skillNode6){
//		//Set Skill 1
//		sorcerer.activeSkill1 = skillNode1;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode1.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill1 + " : " + skillNode1.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//		//set Skill 2
//		sorcerer.activeSkill1 = skillNode2;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode2.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill2 + " : " + skillNode2.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//		//Set Skill 3
//		sorcerer.activeSkill1 = skillNode3;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode3.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill3 + " : " + skillNode3.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//		//Set Skill 4
//		sorcerer.activeSkill1 = skillNode4;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode4.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill4 + " : " + skillNode4.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//		//Set Skill 5
//		sorcerer.activeSkill1 = skillNode5;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode5.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill5 + " : " + skillNode5.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//		//Set Skill 6
//		sorcerer.activeSkill1 = skillNode6;
//		sorcerer.activeSkill1 = sorcerer.gameObject.GetComponent(skillNode6.skillType) as ISkill;
//		Debug.Log(sorcerer.activeSkill6 + " : " + skillNode6.skillType);
//		sorcerer.activeSkill1.setCaster(sorcerer);
//	}

	//Determine if there is a closeby enemy
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
		return nearestObj;
	}
}
