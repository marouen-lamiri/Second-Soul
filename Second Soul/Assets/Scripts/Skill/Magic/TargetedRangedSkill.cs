using UnityEngine;
using System.Collections;

public abstract class TargetedRangedSkill : RangedSkill {
	
	protected Character targetCharacter;
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
			if(hit.GetComponent<Character>()!=null && (hit.GetComponent<Character>().GetType().IsSubclassOf(typeof(Enemy)) || hit.GetComponent<Character>().GetType() == typeof(Enemy))){
				targetCharacter = hit.GetComponent<Character>();
				return;
			}
		}
		targetCharacter = AIRayCast (targetCharacter);

	}

	protected Character AIRayCast (Character targetCharacter){
		SorcererAI ai = gameObject.GetComponent<SorcererAI> ();
		if (ai != null && ai.enabled == true) {
			return ai.checkNearestEnemy();
		}
		return targetCharacter;
	}
}
