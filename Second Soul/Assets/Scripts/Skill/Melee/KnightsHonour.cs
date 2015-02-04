using UnityEngine;
using System.Collections;

public class KnightsHonour : MeleeSkill {

	bool duration = false;
	bool casting = true;
	int additionalArmor;
	public GameObject defenseBuffPrefab;
	GameObject defenseBuff;


	// Use this for initialization
	void Start () {
		skillStart ();
		energyCost = 10;
	}
	
	// Update is called once per frame
	void Update () {
		checkTimeSpent();
		if(defenseBuff != null){
			defenseBuff.transform.position = caster.transform.position + new Vector3 (0,5,0);
		}
		else if(defenseBuff == null && duration == false){
			caster.armor = caster.armor - additionalArmor;
			duration = true;
		}
	}

	public override void useSkill(){
		if (casting == true && caster.loseEnergy(energyCost)) {
			skillStart ();
			defenseBuff = Network.Instantiate (defenseBuffPrefab, caster.transform.position + new Vector3 (0,5,0), new Quaternion(), 4) as GameObject;
			additionalArmor = (int) (0.15 * caster.armor) +1;
			Debug.Log ("Armor increased by: " + additionalArmor);
			caster.armor = additionalArmor + caster.armor;
			Debug.Log ("Armor is: " + caster.armor);
			duration = false;
			casting = false;
		}
	}

	void checkTimeSpent(){
		if(defenseBuff != null){
			if(defenseBuff.activeInHierarchy == true){
				duration = false;
			}
			else{
				defenseBuff = null;
				duration = true;
				casting = true;
			}
		}
		else{
			casting = true;
		}
	}
	
	public override void animateAttack(){
		if (fighterNetworkScript != null) {
			fighterNetworkScript.onAttackTriggered("activeSkill3");
		}	
	}

	public override void rayCast(){
		
	}
}
