using UnityEngine;
using System.Collections;

public class OnHoverTransparent : MonoBehaviour, ISorcererSubscriber {
	
	public LayerMask layer;
	GameObject mainCamera;
	GameObject lastObject;
	Fighter fighter;
	Sorcerer sorcerer;
	Color initialColor;
	Material material;

	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game

		material = Instantiate (renderer.material) as Material;
//		material.CopyPropertiesFromMaterial (renderer.material);
		renderer.material = material;
		initialColor = material.color;
		fighter= GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer= GameObject.FindObjectOfType (typeof (Sorcerer))as Sorcerer;
		if(mainCamera == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(mainCamera == null || fighter == null || sorcerer == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;;
			fighter = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
			sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer= GameObject.FindObjectOfType (typeof (Sorcerer))as Sorcerer;
		}
		else{
			Player player = (fighter.playerEnabled)? (Player) fighter : sorcerer;
			if (Physics.Linecast (mainCamera.transform.position, player.transform.position, layer)) {
				material.color = new Color(initialColor.r,initialColor.g,initialColor.b,0f);
			}
			else{
				material.color = new Color(initialColor.r,initialColor.g,initialColor.b,1f);
			}
		}
		
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}
	
}
