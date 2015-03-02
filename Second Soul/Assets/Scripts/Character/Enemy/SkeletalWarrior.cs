using UnityEngine;
using System.Collections;

public class SkeletalWarrior : Enemy {

	// Use this for initialization
	void Start () {
		enemyStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){
		enemyUpdate ();
	}

	protected override void initializePrimaryStats(){
		strengthPerLvl = 1;
		dexterityPerLvl = 1;
		endurancePerLvl = 1;
		
		strength = 10;
		dexterity = 10;
		endurance = 10;
	}

	public override void initializeSecondaryStats(){
		armor = 70;
		fireResistance = 10;
		coldResistance = 10;
		lightningtResistance = 10;
		
		accuracy = 0.8f;
		attackSpeed = 1.3f;
		
		criticalChance = 0.3f;
		criticalDamage = 2.5f;
		
		attackPower = 20f;
		
		maxHealth = 200;
		
		healthRegen = 0.5f;
	}
}
