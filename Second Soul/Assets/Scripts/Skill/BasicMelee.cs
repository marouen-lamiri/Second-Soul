using UnityEngine;
using System.Collections;

public class BasicMelee : BasicAttack {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public BasicMelee(float damage, Character target){
		this.damage = damage;
		this.target = target;
	}
}
