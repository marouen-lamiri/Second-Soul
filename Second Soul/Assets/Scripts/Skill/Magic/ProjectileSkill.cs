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
		for (int i = 0; i < hits.Length; ++i) {
			GameObject hit = hits[i].collider.gameObject;
			if(hit.CompareTag("Floor")){
				targetPosition = hits [i].point;
			}
			if(hit.GetComponent<Character>()!=null && (hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Enemy)) || hit.GetComponent<Character>().GetType() == typeof(Enemy))){
				targetPosition = hit.transform.position;
				break;
			}
		}
		targetPosition = AIRayCast (targetPosition);
	}
}
