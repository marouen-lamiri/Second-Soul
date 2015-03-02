using UnityEngine;
using System.Collections;

public class Golem : Enemy {

	// Use this for initialization
	void Start () {
	
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

	protected override void idleLogic(){
		//DO NOTHING
	}
}
