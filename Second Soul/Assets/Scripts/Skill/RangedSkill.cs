using UnityEngine;
using System.Collections;

public abstract class RangedSkill : MonoBehaviour, ISkill {

	protected Character caster;

	public float damageModifier;
	protected float energyCost;
	protected float castTime;
	
	protected float skillLength;

	public DamageType damageType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//FIXME: cheat for avoiding generics
	public void useSkill(Character target){
		Debug.Log ("wrong type passing: use skill");
	}
	
	public virtual void useSkill(Vector3 target){
		
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}

}
