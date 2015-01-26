using UnityEngine;
using System.Collections;

public class ParticleBehavior : MonoBehaviour {

	protected Character caster;
	public RangedSkill skill;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startBehaviour(Character caster, RangedSkill skill){
		this.caster = caster;
		this.skill = skill;
	}
}
