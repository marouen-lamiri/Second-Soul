using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {

	public Mob enemy;

	public AnimationClip attack;
	public AnimationClip die;

	public float range;

	public double health;
	public double maxhealth;
	public double damage;
	public double energy;
	public double maxenergy;

	public double impactTime;
	public bool impacted;

	// Use this for initialization
	void Start () {
		enemy = null;
		maxhealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (enemy);
		Debug.Log (health);
		if (!isDead ()) {
			if (Input.GetKey (KeyCode.Space) && enemy != null && inRange ()) {
				animation.Play (attack.name);
				ClickToMove.attacking = true;
				transform.LookAt (enemy.transform.position);
			}

			if (animation [attack.name].time > 0.9 * animation [attack.name].length) {
				ClickToMove.attacking = false;
				impacted = false;
			}
			impact ();
		} 
		else {
			dieMethod();
		}
	}

	private void impact(){
		if(enemy != null && animation.IsPlaying(attack.name) && !impacted){
			if(animation[attack.name].time > (animation[attack.name].length * impactTime) && (animation[attack.name].time < 0.9 * animation[attack.name].length)){
				enemy.GetComponent<Mob>().getHit(damage);
				impacted = true;
			}
		}
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

	public void dieMethod(){
		animation.CrossFade (die.name);
		
		if (animation[die.name].time > animation[die.name].length * 0.80) {
			animation[die.name].speed = 0;
			gameOverScreen();
		}
		
		//RESPAWN/ETC...?
	}

	public void gameOverScreen (){
		Application.LoadLevel("GameOver");
	}

	public bool inRange(){
		return Vector3.Distance(enemy.transform.position, transform.position) <= range;
	}
}
