﻿using UnityEngine;
using System.Collections;

public abstract class AreaRangedSkill : RangedSkill {

	protected Vector3 targetPosition;
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
		targetPosition = hits[0].point;
		if(this.caster.gameObject.tag == "Player")
			targetPosition = AIRayCast (targetPosition);
		else
			targetPosition = EnemyRayCast(targetPosition);
	}	
}
