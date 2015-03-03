using UnityEngine;
using System.Collections;

public class ApplicationQuit : MonoBehaviour {

	string escape = "escape";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (escape)) {
			Application.Quit();
		}
	}
}
