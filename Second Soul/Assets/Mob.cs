using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {

	public float speed;
	public float range;
	
	public CharacterController controller;
	public Transform player;
	private PlayerCombat hero;

	public AnimationClip attackClip;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;

	public double health;
	public double maxHealth;
	public double damage;

	public float impactTime;

	public bool impacted;
	
	// Use this for initialization
	void Start () {
		//health = 100;
		maxHealth = health;
		hero = player.GetComponent<PlayerCombat> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (inRange());
		//Debug.Log (health);
		if (!isDead ()) {
			if (!inRange ()) {
				chase ();
			} 
			else {
				//animation.CrossFade (idle.name);
				attack();
			}
		} 
		else {
			dieMethod();
		}
	}
	
	public bool inRange(){
		return Vector3.Distance(transform.position, player.position)<range;
	}

	private void attack(){
		if (!hero.isDead ()) {
			animation.CrossFade (attackClip.name);

			if (animation [attackClip.name].time > animation [attackClip.name].length * impactTime && !impacted && animation [attackClip.name].time < 0.90 * animation [attackClip.name].length) {
				hero.getHit (damage);
				impacted = true;
			}

			if (animation [attackClip.name].time > 0.90 * animation [attackClip.name].length) {
				impacted = false;
			}
		}
	}

	private void chase(){
		transform.LookAt (player.position);
		controller.SimpleMove (transform.forward * speed);
		animation.CrossFade (run.name);
	}

	public void getHit(double damage){
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

	private void dieMethod(){
		Destroy(controller);

		player.GetComponent<PlayerCombat> ().enemy = null;
		
		animation.CrossFade (die.name);
		

		if (animation[die.name].time > animation[die.name].length * 0.80) {
			animation[die.name].speed = 0;
		}
	}

	void OnMouseOver(){
		//Debug.Log ("Mouse is over");
		if (!isDead ()) {
			player.GetComponent<PlayerCombat> ().enemy = this;
		}
	}

	void OnMouseExit(){
		player.GetComponent<PlayerCombat> ().enemy = null;
	}
}
