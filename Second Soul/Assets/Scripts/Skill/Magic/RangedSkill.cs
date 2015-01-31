using UnityEngine;
using System.Collections;

public abstract class RangedSkill : MonoBehaviour, ISkill {

	public Character caster;
	public double damage;
	public float damageModifier;
	protected float energyCost;
	protected float castTime;
	
	protected float skillLength;
	
	protected SorcererNetworkScript sorcererNetworkScript;

	public DamageType damageType;
	// Use this for initialization
	void Start () {
		skillStart ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void skillStart(){
		sorcererNetworkScript = (SorcererNetworkScript)gameObject.GetComponent<SorcererNetworkScript> ();
	}

	public abstract void useSkill ();

	public abstract void rayCast ();
	
	public void setCaster(Character caster){
		this.caster = caster;
	}

	public abstract void animateAttack();

	public float getEnergyCost(){
		return energyCost;
	}
}
