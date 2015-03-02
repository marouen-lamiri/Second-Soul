using UnityEngine;
using System.Collections;

public class SkeletalWarrior : Enemy {

	// Use this for initialization
	void Start () {
	
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
}
