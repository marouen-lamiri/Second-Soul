using UnityEngine;
using System.Collections;

public class BerserkMode : TargetedMeleeSkill {

	bool duration = false;
	bool casting = true;
	int addAtt;
	GameObject attBuffPrefab;
	GameObject attBuff;
	
	// Use this for initialization
	void Start () {
		skillStart ();
		energyCost = 10;

		attBuffPrefab = (GameObject)Resources.Load ("Assets/Prefabs/skills/Elementals/Prefabs/Light/Attack buff");
	}
	
	// Update is called once per frame
	void Update () {
		checkTimeSpent();
		if(attBuff != null){
			attBuff.transform.position = caster.transform.position + new Vector3 (0,5,0);
		}
		else if(attBuff == null && duration == false){
			caster.attackPower = caster.attackPower - addAtt;
			duration = true;
		}
	}
	
	public override void useSkill(){
		if (casting == true && caster.loseEnergy(energyCost)) {
			skillStart ();
			attBuff = Network.Instantiate (attBuffPrefab, caster.transform.position + new Vector3 (0,5,0), new Quaternion(), 4) as GameObject;
			addAtt = (int) (0.15 * caster.attackPower) +1;
			Debug.Log ("Armor increased by: " + addAtt);
			caster.attackPower = addAtt + caster.attackPower;
			Debug.Log ("Armor is: " + caster.attackPower);
			duration = false;
			casting = false;
		}
	}
	
	void checkTimeSpent(){
		if(attBuff != null){
			if(attBuff.activeInHierarchy == true){
				duration = false;
			}
			else{
				attBuff = null;
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
