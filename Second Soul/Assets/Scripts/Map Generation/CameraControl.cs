using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis("Vertical") * 1/4;
		float rotation = Input.GetAxis("Horizontal") * 1/2;
		if(translation != 0){
			transform.Translate(0, 0, translation);
		}
		if(rotation != 0){
			transform.Rotate(0, rotation, 0);
		}
	}
}
