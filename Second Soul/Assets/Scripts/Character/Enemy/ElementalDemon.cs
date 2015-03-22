using UnityEngine;
using System.Collections;

public class ElementalDemon : Enemy {

	public AnimationClip highPunchClip;
	public AnimationClip walkClip;
	public AnimationClip roarClip;
	public AnimationClip jumpClip;

	int maxRandom = 20;
	int minRandom = 0;
	int compareToValue = 10;

	ISkill activeSkill1;
	ISkill activeSkill2;
	ISkill activeSkill3;
	ISkill activeSkill4;
	protected int intelligence; // spell power, spell crit damage
	protected int wisdom; // cast speed/cooldown, spell crit chance
	protected int spirit; // total energy/regen
	
	protected int intelligencePerLvl;
	protected int wisdomPerLvl;
	protected int spiritPerLvl;

	protected int timeOut = 15;
	protected int timeOutMax = 15;

	Fighter fighter;

	// Use this for initialization
	void Start () {
		enemyStart ();
		fighter = (Fighter)GameObject.FindObjectOfType(typeof(Fighter));
		aggroRange = 20;
		speed = 4;
		attackRange = 2;
		activeSkill1 = GetComponent<BasicMelee> ();
		activeSkill3 = GetComponent<BerserkMode> ();
		activeSkill4 = GetComponent<CycloneSkill> ();
		activeSkill2 = GetComponent<SpinAttack> ();
	}
	
	// Update is called once per frame
	void FixedUpdate (){
		enemyUpdate ();
	}

	protected override void attackTarget(){
		attackAI();
	}

	private void attackAI(){
		if(determineAction()){
			activeSkill4.setCaster(this);
			activeSkill4.useSkill();
			animateHighPunch();

		}
		else if(determineAction()){
			activeSkill2.setCaster(this);
			activeSkill2.useSkill();
			animateJump();
		}
		else if(determineAction()){
			activeSkill3.setCaster(this);
			activeSkill3.useSkill();
			animateRoar();
		}
		else{
			activeSkill1.setCaster(this);
			activeSkill1.useSkill();
		}

	}

	private bool determineAction(){
		int randomValue = Random.Range (minRandom, maxRandom);
		if(randomValue == compareToValue){
			return true;
		}
		return false;
	}

	public void animateRoar(){
		if (roarClip == null) {
			return;
		}
		rigidbody.
		animation.CrossFade(roarClip.name);
	}

	public void animateHighPunch(){
		if (highPunchClip == null) {
			return;
		}
		animation[highPunchClip.name].speed = 0.9f;
		animation.CrossFade(highPunchClip.name);
	}

	public void animateJump(){
		if (jumpClip == null) {
			return;
		}
		animation[jumpClip.name].speed = 0.9f;
		animation.CrossFade(jumpClip.name);
	}

	public void animateWalk(){
		if (walkClip == null) {
			return;
		}
		animation[walkClip.name].speed = 0.9f;
		animation.CrossFade(walkClip.name);
	}

	public void animateIdle(){
		if (idleClip == null) {
			return;
		}
		animation[idleClip.name].speed = 0.9f;
		animation.CrossFade(idleClip.name);
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
		intelligencePerLvl = 5;
		wisdomPerLvl = 4;
		spiritPerLvl = 2;
		
		intelligence = 30;
		wisdom = 25;
		spirit = 15;

		strengthPerLvl = 5;
		dexterityPerLvl = 2;
		endurancePerLvl = 7;
		
		strength = 40;
		dexterity = 20;
		endurance = 100;
	}
	
	public override void initializeSecondaryStats(){
		
		armor = 200;
		fireResistance = 100;
		coldResistance = 50;
		lightningtResistance = 50;
		
		accuracy = 0.8f;
		castSpeed = .5f;
		
		spellCriticalChance = 0.3f;
		spellCriticalDamage = 2.5f;

		attackPower = 40f;
		spellPower = 40f;
		
		maxHealth = 10000;
		
		healthRegen = 0.5f;
		
		cdr = 0.15f;
		spellPower = 20f;
		
		maxEnergy = 10000;
		
		energyRegen = 0.5f;
	}
}
