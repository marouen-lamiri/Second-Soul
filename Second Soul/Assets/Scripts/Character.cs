using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public float speed;

	public double health;
	public double maxHealth;
	
	public double energy;
	public double maxEnergy;
	
	public float attackRange;
	public double damage;
	
	public double impactTime;
	public bool impacted;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
