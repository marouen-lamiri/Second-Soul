﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnHoverTransparent : MonoBehaviour, ISorcererSubscriber {
	
	public LayerMask checkLayers;
	public LayerMask visibleLayer;
	public LayerMask invisibleLayer;
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
			float distance = 1000;
			RaycastHit[] hits = Physics.RaycastAll (mainCamera.transform.position, player.transform.position, distance,checkLayers);
			List<GameObject> gameObjects = new List<GameObject>();
			foreach(RaycastHit hit in hits){
				gameObjects.Add(hit.collider.gameObject);
			}
			foreach(GameObject gO in gameObjects){
				if (gO == this.gameObject){
					this.gameObject.layer = invisibleLayer ;
				}
				else{
					this.gameObject.layer = visibleLayer;
					}
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
