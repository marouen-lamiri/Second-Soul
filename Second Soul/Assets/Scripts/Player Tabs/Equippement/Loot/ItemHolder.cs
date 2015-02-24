﻿using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour {

	public Player player;

	public Item item;
	// Use this for initialization
	void Start () {
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		//item = (Item) System.Activator.CreateInstance(System.Type.GetType("Chest"));
		//item = itemRand();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void getPickedUp(){
		UnityNotificationBar.UNotify("Obtained " + item.GetType()); //although this might appear false in Mono-Develop, it actually works as an external asset
		Destroy (gameObject);
		//Network.Destroy(gameObject);
	}
	
	void OnMouseDrag(){
		player.lootItem = this;
	}
	
	void OnMouseUp(){
		player.lootItem = null;
	}	
	
	void OnMouseOver(){
		player.lootItem = this;
	}
	
	void OnMouseExit(){
		player.lootItem = null;
	}
}