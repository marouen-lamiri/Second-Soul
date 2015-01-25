using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : Character {

	//variable declaration
	float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	
	public bool attacking;

	public int pickUpRange;

	public Inventory inventory;
	public SkillTree skillTree;
	
	public List<ISkill> unlockedSkills;
	
	public ISkill activeSkill1; // protected
	public ISkill activeSkill2; // protected
	public ISkill activeSkill3; // protected
	public ISkill activeSkill4; // protected
	public ISkill activeSkill5; // protected
	
	public ItemHolder lootItem;

	// networking:
	protected FighterNetworkScript fighterNetworkScript;
	protected SorcererNetworkScript sorcererNetworkScript;
	
	// Use this for initialization
	void Start () {
		playerStart ();
	}
	protected void playerStart(){
		characterStart ();
		sphere.renderer.material.color = Color.blue;
		fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
	}
	// Update is called once per frame
	void Update(){
		playerUpdate ();
	}
	protected void playerUpdate(){
		characterUpdate ();
		//Debug.Log (inventory);
	}
	public abstract void levelUp();
	
	protected void initializePlayer () {
		target = null;
		lootItem = null;
		startPosition = transform.position;
	}
	
	protected void initializeLevel(){
		totalXP = 200;//current calculation of level will use this number to calculate that our level is 1 
		calculateLevel();
		calculateNextLevelXP();
	}
	
	protected void playerLogic () {
		if (!isDead()){
			attackLogic ();
			lootLogic();
		}
		else{
			dieMethod();
		}
	}
	
	public override void gainExperience(int experience){
		totalXP += experience;
		if(hasLeveled()){
			levelUp();
			calculateLevel();
			calculateNextLevelXP();
		}
	}
	
	void calculateLevel(){
		level = (int)(Mathf.Log ((float)totalXP/100f)/Mathf.Log(baseFactorXP));
	}
	
	void calculateNextLevelXP(){
		nextLevelXP = (int)((Mathf.Pow(baseFactorXP,(level+1)))*100);
	}
	
	bool hasLeveled(){
		Debug.Log(totalXP >= nextLevelXP);
		return totalXP >= nextLevelXP;
	}
	
	protected void lootLogic(){
		if(lootItem != null){
			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))){
				pickUpItem();
			}
		}		
	}
	
	protected void attackLogic(){
		if(!attackLocked() && playerEnabled){
			if ((Input.GetButtonDown ("activeSkill1") || Input.GetButton ("activeSkill1")) && activeSkill1 != null){

				activeSkill1.setCaster(this);
				activeSkill1.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill2") || Input.GetButton ("activeSkill2")) && activeSkill2 != null){
				activeSkill2.setCaster(this);
				activeSkill2.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill3") || Input.GetButton ("activeSkill3")) && activeSkill3 != null){
				activeSkill3.setCaster(this);
				activeSkill3.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill4") || Input.GetButton ("activeSkill4")) && activeSkill4 != null){
				activeSkill4.setCaster(this);
				activeSkill4.useSkill();
			}
		}
	}

	public void pickUpItem(){
		if(inPickupRange()){
			if(inventory.takeItem(lootItem.item)){
			lootItem.getPickedUp();
			}
		}
		else{
			startMoving(lootItem.transform.position);
		}
	}
	
	public bool inPickupRange(){
		return Vector3.Distance(lootItem.transform.position, transform.position) <= pickUpRange;
	}
}
