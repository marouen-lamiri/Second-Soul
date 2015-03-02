using UnityEngine;
using System.Collections;

public class Golem : Enemy {

	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
		enemyStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){
		enemyUpdate ();
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 3;
		dexterityPerLvl = 1;
		endurancePerLvl = 5;
		
		strength = 30;
		dexterity = 10;
		endurance = 50;
	}

	public override void initializeSecondaryStats(){
		armor = 150;
		fireResistance = 15;
		coldResistance = 15;
		lightningtResistance = 15;
		
		accuracy = 0.8f;
		attackSpeed = .7f;
		
		criticalChance = 0.3f;
		criticalDamage = 2.5f;
		
		attackPower = 40f;
		
		maxHealth = 600;
		
		healthRegen = 0.5f;
	}

	protected override bool cannotMove(){
		string sleepTag = "sleep";
		if (animator.GetCurrentAnimatorStateInfo (0).IsTag (sleepTag)) {
			return true;
		}
		return false;
	}

	protected override void idleLogic(){
		//DO NOTHING
	}
}
