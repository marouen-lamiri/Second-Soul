﻿using UnityEngine;
using System.Collections;

public interface ISkill {

	void useSkill(Character target);
	void useSkill(Vector3 target);
	void setCaster(Character caster);
}
//this can go in a separate file, but I believe it makes sesne to loosely associate this with skills, as they are what cause damage
public enum DamageType{
	Physical,
	Fire,
	Ice,
	Lightning
}