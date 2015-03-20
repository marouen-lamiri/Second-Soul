using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

	string sceneName = "SkillTree";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName == sceneName){
			this.light.range = 30;
		}
		else{
			this.light.range = 0;
		}
	}
}
