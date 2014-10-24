using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter : Character {

	public Enemy enemy;
	
	public AnimationClip attackClip;
	public AnimationClip dieClip;

	// Use this for initialization
	void Start () {
		enemy = null;
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (enemy);
		//Debug.Log (health);
		
		if (!isDead ()) {
			if (Input.GetKey (KeyCode.Space) && enemy != null && inAttackRange ()) {
				animation.Play (attackClip.name);
				ClickToMove.attacking = true;
				transform.LookAt (enemy.transform.position);
			}

			if (animation [attackClip.name].time > 0.9 * animation [attackClip.name].length) {
				ClickToMove.attacking = false;
				impacted = false;
			}
			attack ();
		} 
		else {
			dieMethod();
		}
	}

	private void attack(){
		if(enemy != null && animation.IsPlaying(attackClip.name) && !impacted){
			if(animation[attackClip.name].time > (animation[attackClip.name].length * impactTime) && (animation[attackClip.name].time < 0.9 * animation[attackClip.name].length)){
				enemy.GetComponent<Enemy>().takeDamage(damage);
				impacted = true;
			}
		}
	}

	public void dieMethod(){
		animation.CrossFade (dieClip.name);
		
		if (animation[dieClip.name].time > animation[dieClip.name].length * 0.80) {
			animation[dieClip.name].speed = 0;
			gameOverScreen();
		}
		
		//RESPAWN/ETC...?
	}

	public bool gameOverScreen (){
		Application.LoadLevel("GameOver");
		return true;
	}

	public bool inAttackRange(){
		return Vector3.Distance(enemy.transform.position, transform.position) <= attackRange;
	}
}
