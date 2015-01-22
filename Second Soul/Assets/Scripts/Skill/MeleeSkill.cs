using UnityEngine;
using System.Collections;

public abstract class MeleeSkill : MonoBehaviour, ISkill {

	Character caster;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public abstract void skillStart ();

	public void useSkill(Vector3 targetPosition, Character targetCharacter){
	
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
	public abstract void animateAttack();
}
