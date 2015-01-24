using UnityEngine;
using System.Collections;

public interface ISkill {

	void useSkill();
	void setCaster(Character caster);
	void animateAttack();
	void skillStart();
	void rayCast();
}
//this can go in a separate file, but I believe it makes sesne to loosely associate this with skills, as they are what cause damage
public enum DamageType{
	Physical,
	Fire,
	Ice,
	Lightning
}