﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterShop : Shop {



	private bool fighterShop;
	private int selectedItem = 8;
	private Inventory inventory;
	private Player player;
	
	// Use this for initialization
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		player = (Player) GameObject.FindObjectOfType (typeof(Player));
		//Set the list of items that the fighter can buy
		sell.Add(new HealthPotion()); 
		sell.Add (new ManaPotion());
		sell.Add (new Sword());
		sell.Add (new Axe());
		sell.Add(new Chest());
		sell.Add (new Boots());
		fighterShop = true;
	}

	public override bool shopEnabled(){
		return fighterShop && inBoundaries();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(fighterShop){
			GUI.Box (new Rect(boxStartPositionWidth,boxStartPositionHeight,boxWidth,boxHeight), "", styleBox);
			if(selectedItem < 6){
				GUI.Label (new Rect(ImageBoxLocationWidth, ImageBoxLocationHeight, ImageBoxWidth, ImageBoxHeight),sell[selectedItem].getImage(), standartSkin);
			}
			if(selectedItem < 6){
				GUI.Label (new Rect(descriptionLocationWidth, descriptionLocationHeight, descriptionBoxWidth, descriptionBoxHeight), sell[selectedItem].getDescription(), standartSkin);
			}
			if(GUI.Button (new Rect(buyBoxStartPositionWidth, buyBoxStartPositionHeight, buyItemBoxWidth, buyItemBoxHeight), buyButton)){
				inventory.takeItem(sell[selectedItem]);
			}
			if(GUI.Button (new Rect(closeBoxStartPositionWidth, closeBoxStartPositionHeight, closeItemBoxWidth, closeItemBoxHeight), closeButton)){
				fighterShop = !fighterShop;
			}
			
			
			GUI.Label (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset/2, regularItemBoxWidth, regularItemBoxHeight), greetingMessage, standartSkin);
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 2, regularItemBoxWidth, regularItemBoxHeight), sell[0].getString(), buttonStyle)){
				selectedItem = 0;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 3, regularItemBoxWidth, regularItemBoxHeight), sell[1].getString(), buttonStyle)){
				selectedItem = 1;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 4, regularItemBoxWidth, regularItemBoxHeight), sell[2].getString(), buttonStyle)){
				selectedItem = 2;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 5, regularItemBoxWidth, regularItemBoxHeight), sell[3].getString(), buttonStyle)){
				selectedItem = 3;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 6, regularItemBoxWidth, regularItemBoxHeight), sell[4].getString(), buttonStyle)){
				selectedItem = 4;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 7, regularItemBoxWidth, regularItemBoxHeight), sell[5].getString(), buttonStyle)){
				selectedItem = 5;
			}
		}	
	}
}
