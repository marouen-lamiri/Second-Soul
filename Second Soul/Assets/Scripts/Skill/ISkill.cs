using UnityEngine;
using System.Collections;

public interface ISkill {

	void useSkill(Character target);
	void useSkill(Vector3 target);
	void setCaster(Character caster);
}
