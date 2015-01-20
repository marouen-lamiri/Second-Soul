using UnityEngine;
using System.Collections;

public abstract class RangedSkill : MonoBehaviour, ISkill {

	protected Character caster;
	public double damage;
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

	public virtual void useSkill(Vector3 target, Character character){
		
	}
	
	public void setCaster(Character caster){
		this.caster = caster;
	}

}
