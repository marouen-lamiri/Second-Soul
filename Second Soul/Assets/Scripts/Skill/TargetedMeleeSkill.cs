using UnityEngine;
using System.Collections;

public abstract class TargetedMeleeSkill : MeleeSkill {

	Vector3 targetPosition;
	// Use this for initialization
	void Start () {
		skillStart ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void rayCast(){
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		hits = Physics.RaycastAll(ray.origin,ray.direction, 1000);

		for (int i = 0; i < hits.Length; ++i) {
			if(hits[i].collider.GetType().IsSubclassOf(typeof(Enemy)) || hits[i].collider.GetType() == typeof(Enemy)){
				targetPosition = hits[i].point;
				return;
			}
		}

	}
}
