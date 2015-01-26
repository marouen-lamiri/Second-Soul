using UnityEngine;
using System.Collections;

public class AutoDestructParticleSystem : MonoBehaviour {

	// Use this for initialization
	// Auto destruct script that can be added to the root particle system
	// of a particle effect. It will destroy the gameobject and its children.
	void LateUpdate () 
	{
		if (!particleSystem.IsAlive())
			Object.Destroy (this.gameObject);	
	}
}
