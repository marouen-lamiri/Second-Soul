using UnityEngine;
using System.Collections;

public class Fighter : Player {
	//Variable declaration
	
	int strenght; // base damage, armor, critt damage
	int dexterity; // attack speed, crit chance, accuracy
	int endurance; // health, resistances, health regen
	
	
	//FighterNetworkScript fighterNetworkScript; // is "already serialized" in parent class Player.cs.

	// Use this for initialization
	void Start () {
		initializePlayer();
		initializeLevel();
		playerEnabled=true;
		health = maxHealth;
		energy = maxEnergy;
		
		target = null;
		startPosition = transform.position;
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();

		//networking:
		fighterNetworkScript = (FighterNetworkScript)GameObject.FindObjectOfType (typeof(FighterNetworkScript));
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (enemy);
		//Debug.Log (health);
		playerLogic();
		if(Input.GetKeyDown(KeyCode.E)){
			playerEnabled = !playerEnabled;
		}
	}

	public override void loseEnergy(float energy){
		this.energy -= energy;
		if (energy < 0) {
			energy = 0;
		}

		// networking event listener:
		fighterNetworkScript.onEnergyLost (this.energy);
	}
}
