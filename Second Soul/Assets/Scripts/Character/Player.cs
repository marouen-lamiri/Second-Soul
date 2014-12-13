using UnityEngine;
using System.Collections;

public abstract class Player : Character {

	//variable declaration	
	public bool attacking;
	//private Vector3 castPosition;
	
	public ISkill activeSkill1; // protected
	public  ISkill activeSkill2; // protected
	public Vector3 position;

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
	
	protected void playerLogic () {
		if (!isDead()){
			attackLogic ();
		}
		else{
			dieMethod();
		}
	}
	
	protected void attackLogic(){
		if(!attackLocked() && playerEnabled){
			chasing = false;
			if(target != null){
				if ((Input.GetButtonDown ("activeSkill1") || Input.GetButton ("activeSkill1")) && activeSkill1 != null){
					if (inAttackRange()){
						meshAgent.Stop(true);
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
