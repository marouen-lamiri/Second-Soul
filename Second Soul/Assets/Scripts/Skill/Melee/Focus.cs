using UnityEngine;
using System.Collections;

public class Focus : MeleeSkill {

	bool duration = false;
	bool casting = true;
	float addSpeed;
	GameObject spdBuffPrefab;
	GameObject spdBuff;
	
	// Use this for initialization
	void Start () {
		skillStart ();
		energyCost = 10;

		spdBuffPrefab = (GameObject)Resources.Load ("Prefabs/skills/Light/Speed buff", typeof(GameObject));

	}
	
	// Update is called once per frame
	void Update () {
		checkTimeSpent();
		if(spdBuff != null){
			spdBuff.transform.position = caster.transform.position + new Vector3 (0,5,0);
		}
		else if(spdBuff == null && duration == false){
			caster.attackSpeed = caster.attackSpeed - addSpeed;
			duration = true;
		}
	}
	
	public override void useSkill(){
		if (casting == true && caster.loseEnergy(energyCost)) {
			skillStart ();
			spdBuff = Network.Instantiate (spdBuffPrefab, caster.transform.position + new Vector3 (0,5,0), new Quaternion(), 4) as GameObject;
			addSpeed = (0.15f * caster.attackSpeed);
			//Debug.Log ("Speed increased by: " + addSpeed);
			caster.attackSpeed = addSpeed + caster.attackSpeed;
			//Debug.Log ("Speed is: " + caster.attackPower);
			duration = false;
			casting = false;
		}
	}
	
	void checkTimeSpent(){
		if(spdBuff != null){
			if(spdBuff.activeInHierarchy == true){
				duration = false;
			}
			else{
				spdBuff = null;
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
