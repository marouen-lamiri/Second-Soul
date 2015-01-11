using UnityEngine;
using System.Collections;

public abstract class Player : Character {

	//variable declaration
	float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	
	public bool attacking;
	
	public int pickUpRange;

	public Inventory inventory;
	//private Vector3 castPosition;
	
	public ISkill activeSkill1; // protected
	public ISkill activeSkill2; // protected
	
	public ItemHolder lootItem;

	// networking:
	protected FighterNetworkScript fighterNetworkScript;
	protected SorcererNetworkScript sorcererNetworkScript;
	
	// Use this for initialization
	void Start () {
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
		//Debug.Log (inventory);
		//Debug.Log (GameObject.Find ("HUD/Equipment/Inventory"));
		//inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent("Inventory") as Inventory;
		//inventory.sayhi ();
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
		//Debug.Log(lootItem);
		if(lootItem != null){
			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))){
				pickUpItem();
			}
		}		
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
						if(fighterNetworkScript != null) {
							fighterNetworkScript.onAttackTriggered("activeSkill1");
						} else if (sorcererNetworkScript != null) {
							//sorcererNetworkScript.onAttackTriggered("activeSkill1");
						} else {
							print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
						}
					}
					else{
						chaseTarget(target.transform.position);
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
						// networking event listener:
						if(fighterNetworkScript != null) {
							fighterNetworkScript.onAttackTriggered("activeSkill2");
						} else if (sorcererNetworkScript != null) {
							//sorcererNetworkScript.onAttackTriggered("activeSkill2");
						} else {
							print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
						}		
					}
					else{
						chaseTarget(target.transform.position);
					}
				}
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
			chaseTarget(lootItem.transform.position);
		}
	}
	
	public bool inPickupRange(){
		return Vector3.Distance(lootItem.transform.position, transform.position) <= pickUpRange;
	}
	
	public Vector3 castPosition(){ // private
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Physics.Raycast(ray, out hit, 1000);
		return new Vector3(hit.point.x, hit.point.y, hit.point.z);
		
		
	}	
}
