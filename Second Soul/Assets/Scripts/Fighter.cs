using UnityEngine;
using System.Collections;

public class Fighter : Character {

	//Variable declaration
	public bool attacking;
	

	// Use this for initialization
	void Start () {
		target = null;
		health = maxHealth;
		energy = maxEnergy;
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (enemy);
		//Debug.Log (health);
		
		if (!isDead()){
			if(!attackLocked()){
				chasing = false;
				if ((Input.GetMouseButtonDown (0) || Input.GetMouseButton (0)) && target != null){
					if (inAttackRange()){
						attack ();
					}
					else{
						chaseTarget();
					}
				}
			}
		}
		else{
			dieMethod();
		}
	}

	public bool inAttackRange(){
		return Vector3.Distance(target.transform.position, transform.position) <= attackRange;
	}
}
