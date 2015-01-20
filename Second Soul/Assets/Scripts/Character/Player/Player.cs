using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : Character {

	//variable declaration
	float baseFactorXP = 1.5f;
	public int totalXP; // total experience --remove public
	public int nextLevelXP; // xp need for next level --remove public
	
	public bool attacking;
	private bool moving;
	private Vector3 goalPosition;

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
		if (moving) {
			moveToPosition();
		}
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
		//Debug.Log(lootItem);
		if(lootItem != null){
			if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))){
				pickUpItem();
			}
		}		
	}
	
	protected void attackLogic(){
		if(!attackLocked() && playerEnabled){
			RaycastHit[] hits;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);
			Character targetCharacter;
			Vector3 targetPosition;
			if (hits.Length>1 && hits [1].GetType ().IsSubclassOf (typeof(Enemy))) {
				targetPosition = hits [1].transform.position;
				targetCharacter = target;//the player's target, which in this case is an enemy
			}
			else {
				targetPosition = new Vector3(hits[0].point.x, hits[0].point.y, hits[0].point.z);
				targetCharacter = target;//the player's target, which in this case is null
				if(chasing == true){
					targetPosition=target.transform.position;
				}
			}
			if(targetCharacter==this){
				targetCharacter=null;
				//return;
			}
			if ((Input.GetButtonDown ("activeSkill1") || Input.GetButton ("activeSkill1")) && activeSkill1 != null){

				activeSkill1.setCaster(this);
				activeSkill1.useSkill(targetPosition, targetCharacter);

				// networking event listener:
				if(fighterNetworkScript != null) {
					fighterNetworkScript.onAttackTriggered("activeSkill1");
				} else if (sorcererNetworkScript != null) {
					sorcererNetworkScript.onAttackTriggered("activeSkill1");
				} else {
					print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
				}
			}
			else if ((Input.GetButtonDown ("activeSkill2") || Input.GetButton ("activeSkill2")) && activeSkill2 != null){
				activeSkill2.setCaster(this);
				activeSkill2.useSkill(targetPosition,targetCharacter);

				// networking event listener:
				if(fighterNetworkScript != null) {
					fighterNetworkScript.onAttackTriggered("activeSkill2");
				} else if (sorcererNetworkScript != null) {
					sorcererNetworkScript.onAttackTriggered("activeSkill2");
				} else {
					print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
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
			startMoving(lootItem.transform.position);
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

	private void moveToPosition(){
		//Player moving
		pathing.findPath(transform.position, goalPosition);
		List<Vector3> path = grid.worldFromNode(grid.path);
		Vector3 destination;
		Quaternion newRotation;
		if (path.Count > 1) {
			destination = path [1];
			newRotation = Quaternion.LookRotation (destination - transform.position);
		}
		else {
			destination = goalPosition;
			newRotation = Quaternion.LookRotation (goalPosition - transform.position);
		}

		newRotation.x = 0;
		newRotation.z = 0;
		
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 7);
		controller.SimpleMove (transform.forward * speed);
		
		animateRun();
		
		// networking: event listener to RPC the attack anim
		//			Fighter fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		CharacterNetworkScript playerNetworkScript = GetComponent<CharacterNetworkScript>();
		if(playerNetworkScript != null) {
			playerNetworkScript.onRunTriggered();
		} 
		else {
			print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
		}
		//Player not moving
		if(destination == goalPosition) {
			moving = false;
			animateIdle();

			if(playerNetworkScript != null) {
				playerNetworkScript.onIdleTriggered();
			} else {
				print("No fighterNetworkScript nor sorcererNetworkScript attached to player.");
			}
			
		}
	}
	public void startMoving(Vector3 position){
		moving = true;
		goalPosition = position;
	}

	public void stopMoving(){
		moving = false;
		chasing = false;
		goalPosition = transform.position;
	}
}
