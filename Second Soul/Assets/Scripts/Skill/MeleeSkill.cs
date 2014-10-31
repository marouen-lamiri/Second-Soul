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
	
	public void useSkill(Character target){
	
	}
	//FIXME: cheat for avoiding generics
	public void useSkill(Vector3 target){
		Debug.Log ("wrong type passing: use skill");	
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
}
