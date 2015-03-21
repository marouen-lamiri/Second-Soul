﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : Character {

	//variable declaration
	protected float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	private string townSceneName = "Town";
	//public bool busyHUD; // global state variable to disable movements if HUD elements are open

	public int pickUpRange;
	
	protected GameObject skillTreeGameObject;

	public Inventory inventory;
	public SkillTree skillTree;
	public ActionBar actionBar;
	public Shop shop;
	
	public int usableSkillPoints;
	
	public ISkill activeSkill1;
	public ISkill activeSkill2;
	public ISkill activeSkill3;
	public ISkill activeSkill4;
	public ISkill activeSkill5;
	public ISkill activeSkill6;
	
	public ItemHolder lootItem;
	public TreasureChest treasureChest;

	// networking:
	protected FighterNetworkScript fighterNetworkScript;
	protected SorcererNetworkScript sorcererNetworkScript;
	
	// Use this for initialization
	void Start () {
		playerStart ();
	}
	protected void playerStart(){
		//busyHUD = false;
		// FIXME: THIS NEEDS TO BE ZERO AFTER TESTING, SKILL POINTS COME FROM LEVELING
		usableSkillPoints = 4;
		characterStart ();
		initializeActionBar();
		initializeInventory();
		initializeShop();
		initiazlieNetwork();
		// Mini map float ball color
		sphere.renderer.material.color = Color.blue;
		//temporarySkills();
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
			actionBar.setPlayer(this);
			actionBar.initializeBasicAttack();
		}
	}

	protected void initializeShop(){
		if(playerEnabled && Application.loadedLevelName == townSceneName){
			shop = (Shop) GameObject.FindObjectOfType (typeof (Shop));
		}
	}
	
	protected void initializeInventory(){
		if(playerEnabled){
			inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
			inventory.setPlayer(this);
		}
	}
	
	public abstract void levelUp();
	
	protected void initializePlayer () {
		target = null;
		lootItem = null;
		treasureChest = null;
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
	
	protected void playerLogic () {
		if (!isDead()){
			//Debug.Log( this.GetType() + " am i busy: " + busyHUD());
			if(!busyHUD()){
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
		//Debug.Log (experience);
		if(hasLeveled()){
			levelUp();
			calculateLevel();
			calculateNextLevelXP();
		}
	}
	
	public void calculateLevel(){
		level = (int)(Mathf.Log ((float)totalXP/100f)/Mathf.Log(baseFactorXP));
	}

    public void calculateNextLevelXP(){
		nextLevelXP = (int)((Mathf.Pow(baseFactorXP,(level+1)))*100);
	}
	
	protected bool hasLeveled(){
		Debug.Log("has leveled: " + (totalXP >= nextLevelXP));
		return totalXP >= nextLevelXP;
	}
	
	protected void attackLogic(){
		if(!attackLocked() && playerEnabled && !busyHUD()){
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
				activeSkill5.useSkill();
			}
			else if ((Input.GetButtonDown ("activeSkill6") || Input.GetButton ("activeSkill6")) && activeSkill6 != null){
				activeSkill6.useSkill();
			}
		}
	}
	
	protected void lootLogic(){
		// handles picking up dropped items
		if(lootItem != null){
			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))){
				pickUpItem();
			}
		}
		// handles opening chests
		if(treasureChest != null){
			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))){
				openChest();
			}
		}
	}

	public void pickUpItem(){
		if(inPickupRange(lootItem.transform.position)){
			if(inventory.takeItem(lootItem.item)){
				lootItem.getPickedUp();
			}
		}
		else{
			startMoving(lootItem.transform.position);
		}
	}
	
	public void openChest(){
		Debug.Log(treasureChest + " I GET HER");
		if(inPickupRange(treasureChest.transform.position)){
			treasureChest.openChest();
		}
		else{
			startMoving(treasureChest.transform.position);
		}
	}
	
	public bool inPickupRange(Vector3 objectPos){
		return Vector3.Distance(objectPos, transform.position) <= pickUpRange;
	}
	
	//ADD CONDITIONS FOR ANY NEW OBJECTS THAT WOULD MAKE PLAY BUSY
	public bool busyHUD(){
		if(playerEnabled){
			return (actionBar.inBoundaries() || skillTree.inBoundaries() || inventory.inBoundaries() || inventory.isItemPickedUp());
		}
		return false;
	}
}
