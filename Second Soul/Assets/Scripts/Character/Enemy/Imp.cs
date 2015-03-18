using UnityEngine;
using System.Collections;

public class Imp : Enemy {

	ISkill activeSkill2;
	protected int intelligence; // spell power, spell crit damage
	protected int wisdom; // cast speed/cooldown, spell crit chance
	protected int spirit; // total energy/regen
	
	protected int intelligencePerLvl;
	protected int wisdomPerLvl;
	protected int spiritPerLvl;
	// Use this for initialization
	void Start () {
		enemyStart ();
		activeSkill2 = GetComponent<FireballSkill> ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){
		enemyUpdate ();
	}

	protected override void attackTarget(){
		activeSkill2.setCaster(this);
		activeSkill2.useSkill();
	}

	public override bool loseEnergy(float energy){
		if (energy > this.energy) {
			return false;
		}
		this.energy -= energy;

//		maybe we need an enemy equivalent of this?
		// networking event listener:
		//fighterNetworkScript.onEnergyLost (this.energy);
		
		return true;
	}

	public override bool criticalHitCheck(){
		int randomRoll = Random.Range (1, 100);
		if (randomRoll <= spellCriticalChance * 100) {
			return true;
		}
		else {
			return false;
		}
	}

	public override double getDamage(){
		if (criticalHitCheck ()) {
			return spellPower*spellCriticalDamage;
		}
		return spellPower;
	}

	protected override void initializePrimaryStats(){
		intelligencePerLvl = 3;
		wisdomPerLvl = 2;
		spiritPerLvl = 1;
		
		intelligence = 20;
		wisdom = 15;
		spirit = 10;
	}

	public override void initializeSecondaryStats(){

		armor = 70;
		fireResistance = 10;
		coldResistance = 10;
		lightningtResistance = 10;
		
		accuracy = 0.8f;
		castSpeed = .5f;
		
		spellCriticalChance = 0.3f;
		spellCriticalDamage = 2.5f;
		
		spellPower = 20f;
		
		maxHealth = 200;
		
		healthRegen = 0.5f;

		cdr = 0.15f;
		spellPower = 20f;
		
		maxEnergy = 10000;
		
		energyRegen = 0.5f;
	}
}
