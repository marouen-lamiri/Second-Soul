using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

	GameObject magicCircle;
	GameObject magicCircle2;
	Fighter fPosition;
	Sorcerer sPosition;
	string circleTown = "CircleTown";
	string circleDungeon = "CircleDungeon";
	int previousSceneTown = 3;
	int previousSceneDungeon = 1;

	// Use this for initialization
	void Start () {
		fPosition = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sPosition = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		if(Application.levelCount - 1 == previousSceneTown){
			magicCircle = GameObject.Find(circleTown);
			fPosition.transform.position = magicCircle.transform.position + new Vector3(6,0,6);
			sPosition.transform.position = magicCircle.transform.position + new Vector3(5,0,5);
		}
		else{
			Debug.Log (Application.levelCount - 1);
			magicCircle2 = GameObject.Find(circleDungeon);
			fPosition.transform.position = magicCircle2.transform.position + new Vector3(6,0,6);
			sPosition.transform.position = magicCircle2.transform.position + new Vector3(5,0,5);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
