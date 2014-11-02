using UnityEngine;
using System.Collections;

public abstract class Player : Character {

	//variable declaration	
	public bool attacking;
	//private Vector3 castPosition;
	
	protected ISkill activeSkill1;
	protected ISkill activeSkill2;
	
	// Use this for initialization
	void Start () {
	
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
						//attack ();
						activeSkill1.setCaster(this);
						activeSkill1.useSkill(target);
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
					}
					else{
						chaseTarget();
					}
				}
			}
		}
	}
	
	private Vector3 castPosition(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Physics.Raycast(ray, out hit, 1000);
		return new Vector3(hit.point.x, hit.point.y, hit.point.z);
		
		
	}	
}
