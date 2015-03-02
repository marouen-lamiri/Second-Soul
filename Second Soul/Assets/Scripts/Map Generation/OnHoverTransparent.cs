using UnityEngine;
using System.Collections;

public class OnHoverTransparent : MonoBehaviour {

	GameObject mainCamera;
	Player player;
	Color initialColor;

	// Use this for initialization
	void Start () {
		initialColor = transform.renderer.material.color;
		player = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		mainCamera = GameObject.Find("MainCamera") as GameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate() {

	}

	void OnMouseEnter(){
		transform.renderer.material.color = new Color(initialColor.r,initialColor.g,initialColor.b,0.5f);
	}
	
	void OnMouseExit(){
		transform.renderer.material.color = initialColor;
	}
}
