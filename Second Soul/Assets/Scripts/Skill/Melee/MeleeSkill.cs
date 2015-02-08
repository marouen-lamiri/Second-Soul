using UnityEngine;
using System.Collections;

public abstract class MeleeSkill : MonoBehaviour, ISkill {

	protected Character caster;

	public double damage;
	public float damageModifier;
	protected float energyCost;	
	protected float skillLength;
	protected float castTime;
	protected FighterNetworkScript fighterNetworkScript;
	protected DamageType damageType;
	// Use this for initialization
	void Start () {
		skillStart ();
	}

	public virtual void skillStart(){
		fighterNetworkScript = (FighterNetworkScript)gameObject.GetComponent<FighterNetworkScript> ();
		setCaster (gameObject.GetComponent<Character> ());
	}

	// Update is called once per frame
	void Update () {
	
	}

	public abstract void useSkill ();
	
	public void setCaster(Character caster){
		this.caster = caster;
	}
	
	public abstract void animateAttack();

	public abstract void rayCast();
}
