using UnityEngine;
using System.Collections;

public class Enemy : Character {
	
	//Variable declaration
	public Transform playerTransform;
	private Fighter player;

	public AnimationClip attackClip;
	public AnimationClip runClip;
	public AnimationClip idleClip;
	public AnimationClip dieClip;

	public float aggroRange;
	public bool hasAggro;
	
	// Use this for initialization
	void Start (){
		//health = 100;
		health = maxHealth;
		energy = maxEnergy;
		player = playerTransform.GetComponent<Fighter> ();
	}

	// Update is called once per frame
	void Update (){
		//Debug.Log (inRange());
		//Debug.Log (health);
		if (!isDead ()) {
			enemyAI ();
		} 
		else {
			dieMethod();
		}
	}

	public void enemyAI(){
		if(!hasAggro){
			if(inAwareRadius()){
				if(hasDirectView()){
					hasAggro = true;
				}
			}
		}
		else if(!inAttackRange () && hasAggro){
			chasePlayer();
			if(outAggroRange()){
				loseAggro();
			}
		} 
		else if(inAttackRange ()){
			attack();
		}	
	}

	public bool hasDirectView(){
		Vector3 playerPosition = player.transform.position;
		Vector3 enemyOrigin = new Vector3 (transform.position.x, transform.position.y + controller.height, transform.position.z);
		Vector3 direction = new Vector3(playerPosition.x, playerPosition.y + player.controller.height, playerPosition.z) - enemyOrigin;
		float distance = Vector3.Distance (player.transform.position, transform.position);

		RaycastHit[] obstaclesHit;
		obstaclesHit = Physics.RaycastAll(enemyOrigin, direction, distance);

		if(obstaclesHit.Length > 1)
			return false;
		else
			return true;
	}

	public bool inAwareRadius(){
		Vector3 targetDir = player.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);

		float percentView = (180 - angle) / 180;
		float viewRadiusPercent = 0.25f + 0.75f * percentView;

		return Vector3.Distance(transform.position, playerTransform.position) < (aggroRange * viewRadiusPercent);
	}

	public bool inAttackRange(){
		return Vector3.Distance(transform.position, playerTransform.position) < attackRange;
	}

	public bool inAggroRange(){
		return Vector3.Distance(transform.position, playerTransform.position) < aggroRange;
	}

	public bool outAggroRange(){
		return Vector3.Distance(transform.position, playerTransform.position) > (aggroRange * 1.5);
	}

	public void loseAggro(){
			hasAggro = false;
			animation.CrossFade (idleClip.name);
	}
	
	private void attack(){
		if (!player.isDead ()){
			animation.CrossFade (attackClip.name);

			if (animation [attackClip.name].time > animation [attackClip.name].length * impactTime && !impacted && animation [attackClip.name].time < 0.90 * animation [attackClip.name].length) {
				player.takeDamage (damage);
				impacted = true;
			}

			if (animation [attackClip.name].time > 0.90 * animation [attackClip.name].length){
				impacted = false;
			}
		}
	}

	private void chasePlayer(){
		transform.LookAt (playerTransform.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade (runClip.name);
	}

	private void dieMethod(){
		Destroy(controller);

		playerTransform.GetComponent<Fighter> ().enemy = null;
		
		animation.CrossFade (dieClip.name);
		
		if (animation[dieClip.name].time > animation[dieClip.name].length * 0.80){
			animation[dieClip.name].speed = 0;
		}
	}

	void OnMouseOver(){
		//Debug.Log ("Mouse is over");
		if (!isDead ()){
			playerTransform.GetComponent<Fighter> ().enemy = this;
		}
	}

	void OnMouseExit(){
		playerTransform.GetComponent<Fighter> ().enemy = null;
	}	
}
