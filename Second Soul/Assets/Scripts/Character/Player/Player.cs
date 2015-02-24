﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : Character {

	//variable declaration
	protected float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	
	public bool busyHUD; // global state variable to disable movements if HUD elements are open
	
	public bool attacking;

	public int pickUpRange;
	
	protected GameObject skillTreeGameObject;

	public Inventory inventory;
	public SkillTree skillTree;
	public ActionBar actionBar;
	
	public int usableSkillPoints;
	
	public ISkill activeSkill1;
	public ISkill activeSkill2;
	public ISkill activeSkill3;
	public ISkill activeSkill4;
	public ISkill activeSkill5;
	public ISkill activeSkill6;
	
	public ItemHolder lootItem;

	// networking:
	protected FighterNetworkScript fighterNetworkScript;
	protected SorcererNetworkScript sorcererNetworkScript;
	
	// Use this for initialization
	void Start () {
		playerStart ();
	}
	protected void playerStart(){
		busyHUD = false;
		characterStart ();
		
		// FIXME: THIS NEEDS TO BE ZERO AFTER TESTING, SKILL POINTS COME FROM LEVELING
		usableSkillPoints = 4;
		initializeActionBar();
		//temporarySkills();
		sphere.renderer.material.color = Color.blue;
		initiazlieNetwork();
	}
	// Update is called once per frame
	void FixedUpdate(){
		playerUpdate ();
	}
	protected void playerUpdate(){
		characterUpdate ();
		//Debug.Log (inventory);
	}
	
	protected virtual void initializeSkillTree(){
		skillTreeGameObject = GameObject.Find("Skill Tree");
	}
	
	protected void initializeActionBar(){
		if(playerEnabled){
			actionBar = (ActionBar) GameObject.FindObjectOfType (typeof (ActionBar));
			//actionBar.findPlayer(this.GetType());
			actionBar.setPlayer(this);
			actionBar.initializeBasicAttack();
		}
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
	
	protected void initiazlieNetwork(){
		fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
	}
	
	//REMOVE THIS ONCE ACTION BAR SKILL PLACING IS DYNAMIC
	/*private void temporarySkills(){
		unlockedSkills.Add(new SkillNode(typeof(BasicMelee), "Basic Melee", "...", new Rect(0,0,0,0), FireballModel.getImage()));
		unlockedSkills.Add(new SkillNode(typeof(BasicRanged), "Basic Range", "...", new Rect(0,0,0,0), FireballModel.getImage()));
		unlockedSkills.Add(new SkillNode(typeof(FireballSkill), "Fireball", "...", new Rect(0,0,0,0), FireballModel.getImage()));
	}*/
	
	protected void playerLogic () {
		if (!isDead()){
			Debug.Log( this.GetType() + " am i busy: " + busyHUD);
			// bool doesnt work...
			if(!busyHUD){
				attackLogic ();
				lootLogic();
			}
		}
		else{
			dieMethod();
		}
	}
	
	public override void gainExperience(int experience){
		totalXP += experience;
		Debug.Log (experience);
		if(hasLeveled()){
			levelUp();
			calculateLevel();
			calculateNextLevelXP();
		}
	}
	
	public void calculateLevel(){
		level = (int)(Mathf.Log ((float)totalXP/100f)/Mathf.Log(baseFactorXP));
	}

    public void calculateNextLevelXP()
    {
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
		if(!attackLocked() && playerEnabled && !busyHUD){
			if ((Input.GetButtonDown ("activeSkill1") || Input.GetButton ("activeSkill1")) && activeSkill1 != null){
				activeSkill1.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill2") || Input.GetButton ("activeSkill2")) && activeSkill2 != null){
				activeSkill2.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill3") || Input.GetButton ("activeSkill3")) && activeSkill3 != null){
				activeSkill3.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill4") || Input.GetButton ("activeSkill4")) && activeSkill4 != null){
				activeSkill4.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill5") || Input.GetButton ("activeSkill5")) && activeSkill5 != null){
				Debug.Log ("LEFT CLICK HAPPENS");
				activeSkill5.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill6") || Input.GetButton ("activeSkill6")) && activeSkill6 != null){
				activeSkill6.useSkill();
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
