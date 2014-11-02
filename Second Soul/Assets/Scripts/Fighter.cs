using UnityEngine;
using System.Collections;

public class Fighter : Player {

	//Variable declaration	
	
	// Use this for initialization
	void Start () {
		initializePlayer();
		playerEnabled=true;
		health = maxHealth;
		energy = maxEnergy;
		
		target = null;
		startPosition = transform.position;
		activeSkill1 = (BasicMelee)controller.GetComponent<BasicMelee>();
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
	}
}
