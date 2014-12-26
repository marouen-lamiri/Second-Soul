using UnityEngine;
using System.Collections;

public abstract class Player : Character {

	/*
	*exponential function
	1-2: 2000
	2-3: 4000
	
	
	1-2
	totalExp: 1500
	totalExperienceToNextLevel: 2000
	
	2-3
	totalExp: 3000
	totalExperienceToNextLevel: 6000
	
	
	
	currentLevel: 2
	currentExp: 1000
	expToNextLvl: 4000
	
	
	*/

	//variable declaration
	float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	
	public bool attacking;
	//private Vector3 castPosition;
	
	public ISkill activeSkill1; // protected
	public ISkill activeSkill2; // protected

	// networking:
	protected FighterNetworkScript fighterNetworkScript;
	
	// Use this for initialization
	void Start () {
		fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
	}
	
	// Update is called once per frame
	void Update(){
	
	}
	
	protected void initializePlayer () {
		target = null;
		startPosition = transform.position;
	}
	
	protected void initializeLevel(){
		totalXP = 200;
		calculateLevel();
		calculateNextLevelXP();
	}
	
	protected void playerLogic () {
		if (!isDead()){
			attackLogic ();
		}
		else{
			dieMethod();
		}
	}
	
	public override void gainExperience(int experience){
		totalXP += experience;
		if(hasLeveled()){
			calculateLevel();
			calculateNextLevelXP();
		}
	}
	
	void calculateLevel(){
		level = (int)(Mathf.Log ((float)totalXP/100f)/Mathf.Log(baseFactorXP));
	}
	
	void calculateNextLevelXP(){
		nextLevelXP = (int)(Mathf.Pow(baseFactorXP,(level+1)))*100;
	}
	
	bool hasLeveled(){
		return totalXP >= nextLevelXP;
	}
	
	protected void attackLogic(){
		if(!attackLocked() && playerEnabled){
			chasing = false;
			if(target != null){
				if ((Input.GetButtonDown ("activeSkill1") || Input.GetButton ("activeSkill1")) && activeSkill1 != null){
					if (inAttackRange()){
						//meshAgent.Stop(true);
						//attack ();
						activeSkill1.setCaster(this);
						activeSkill1.useSkill(target);

						// networking event listener:
						fighterNetworkScript.onAttackTriggered("activeSkill1");
					}
					else{
						chaseTarget();
					}
				}
				if ((Input.GetButtonDown ("activeSkill2") || Input.GetButton ("activeSkill2")) && activeSkill2 != null){
					if (inAttackRange()){
						//attack ();
						//Debug.Log (activeSkill2.GetType());
						//locatePosition();
						activeSkill2.setCaster(this);
						activeSkill2.useSkill(castPosition());

						// networking event listener:
						fighterNetworkScript.onAttackTriggered("activeSkill2");
					}
					else{
						chaseTarget();
					}
				}
			}
		}
	}
	
	public Vector3 castPosition(){ // private
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Physics.Raycast(ray, out hit, 1000);
		return new Vector3(hit.point.x, hit.point.y, hit.point.z);
		
		
	}	
}
