using UnityEngine;
using System.Collections;

public class OnHoverTransparent : MonoBehaviour {
	
	public LayerMask layer;
	GameObject mainCamera;
	GameObject lastObject;
	Player player;
	Color initialColor;
	
	// Use this for initialization
	void Start () {
		initialColor = transform.renderer.material.color;
		player = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		if(mainCamera == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(mainCamera == null || player == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;;
			player = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		}
		else{
			if (Physics.Linecast (mainCamera.transform.position, player.transform.position, layer)) {
				transform.renderer.material.color = new Color(initialColor.r,initialColor.g,initialColor.b,0.5f);
			}
			else{
				transform.renderer.material.color = new Color(initialColor.r,initialColor.g,initialColor.b,1f);
			}
		}
		
	}
}
