using UnityEngine;
using System.Collections;

public abstract class ProjectileSkill : RangedSkill {

	protected float spawnDistance;
	public float travelDistance;
	public float speed;
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
		targetPosition = hits [0].point;
	}
}
