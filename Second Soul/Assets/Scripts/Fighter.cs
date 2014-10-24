using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour {

	public Enemy enemy;
	public  List<Enemy> enemies;
	public Enemy enemyP;
	
	public AnimationClip attackClip;
	public AnimationClip dieClip;

	public float attackRange;
	public Vector3 spawnPosition = new Vector3 (340, 0, 988);
	
	public double health;
	public double maxhealth;
	
	public double energy;
	public double maxenergy;
	
	public double damage;

	public double impactTime;
	public bool impacted;

	// Use this for initialization
	void Start () {
		enemy = null;
		maxhealth = health;
		Enemy temp = Instantiate(enemyP,spawnPosition,Quaternion.identity)as Enemy;
		temp.playerTransform = this.transform;
		enemies.Add (temp);
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

	public void takeDamage(double damage){
		health -= damage;
		
		if (health <= 0) {
			health = 0;
		}
	}

	public bool isDead(){
		if (health <= 0) {
			return true;
		}
		return false;
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
