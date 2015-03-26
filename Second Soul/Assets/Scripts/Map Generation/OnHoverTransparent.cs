using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnHoverTransparent : MonoBehaviour, ISorcererSubscriber {
	
	public LayerMask checkLayers;
	public LayerMask visibleLayer;
	public LayerMask invisibleLayer;
	GameObject mainCamera;
	GameObject lastObject;
	Fighter fighter;
	Sorcerer sorcerer;
	List<GameObject> invisibleList;

	// Use this for initialization
	void Start () {

		subscribeToSorcererInstancePublisher (); // jump into game
		fighter= GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer= GameObject.FindObjectOfType (typeof (Sorcerer))as Sorcerer;
		if(mainCamera == null){
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera") as GameObject;
		}
		invisibleList = new List<GameObject>();
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
			float distance = 1000;
			RaycastHit[] hits = Physics.RaycastAll (mainCamera.transform.position, player.transform.position, distance,checkLayers);
			foreach (GameObject gO in invisibleList){
				gO.layer = visibleLayer.value;
			}
			invisibleList = new List<GameObject>();
			foreach(RaycastHit hit in hits){
				hit.collider.gameObject.layer = invisibleLayer.value;
				invisibleList.Add(hit.collider.gameObject);
			}
//			foreach(GameObject gO in gameObjects){
//				if (gO == this.gameObject){
//					this.gameObject.layer = invisibleLayer.value;
//				}
//				else{
//					this.gameObject.layer = visibleLayer.value;
//					}
//			}
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
