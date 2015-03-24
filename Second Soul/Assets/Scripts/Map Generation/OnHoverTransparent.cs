using UnityEngine;
using System.Collections;

public class OnHoverTransparent : MonoBehaviour {
	
	public LayerMask layer;
	public static int counter;
	public Material invisibleMaterial;
	public Material wallMaterial;
	GameObject mainCamera;
	GameObject lastObject;
	Fighter fighter;
	Sorcerer sorcerer;
	Color initialColor;
	Material material;
	// Use this for initialization
	void Start () {
		material = Instantiate (renderer.material) as Material;
//		material.CopyPropertiesFromMaterial (renderer.material);
		renderer.material = material;
		initialColor = material.color;
		fighter= GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		sorcerer= GameObject.FindObjectOfType(typeof(Sorcerer))as Sorcerer;
		if(mainCamera == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(mainCamera == null || fighter == null || sorcerer == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;;
			fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
			sorcerer= GameObject.FindObjectOfType(typeof(Sorcerer))as Sorcerer;
		}
		else{
			Player player = (fighter.playerEnabled)? (Player) fighter : sorcerer;
			Debug.DrawLine(player.transform.position, mainCamera.transform.position);
			RaycastHit hit; 
			if (Physics.Linecast (player.transform.position, mainCamera.transform.position, out hit,layer) &&
			    hit.collider.gameObject == this.gameObject) {
				this.renderer.material.color = new Color(initialColor.r,initialColor.g,initialColor.b,0f);
				//counter++;
				//this.gameObject.renderer.material = invisibleMaterial;
			}
			else{
				this.renderer.material.color = new Color(initialColor.r,initialColor.g,initialColor.b,1f);
				//this.gameObject.renderer.material = wallMaterial;
			}
		}
		
	}
}
