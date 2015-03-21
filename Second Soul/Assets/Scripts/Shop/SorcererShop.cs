using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererShop : Shop {
	
	public static bool sorcererShop;
	private bool full;
	private int selectedItem = 4;
	private int size = 4;
	private Inventory inventory;
	private Player player;
	private bool buyable = true;
	private int healthIndex = 0;
	private int manaIndex = 1;
	private int amuletIndex = 2;
	private int ringIndex = 3;
	
	// Use this for initialization
	void Start () {
		//Testing purposes
		Sorcerer.soulShards = 300;
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		//Set the list of items that the sorcerer can buy
		sell.Add(new HealthPotion()); 
		sell.Add(new ManaPotion());
		sell.Add(new Amulet());
		sell.Add(new Ring());
	}
	
	public override bool shopEnabled(){
		return checkStatus() && inBoundaries();
	}

	public bool checkStatus(){
		return sorcererShop && Network.isClient;
	}
		
	string checkEnough(){
		if(buyable){
			return buyButton;
		}
		else if(full){
			return inventoryFull;
		}
		return notEnough;
	}
	
	void recreateItem(){
		if(selectedItem == healthIndex){
			sell[selectedItem] = new HealthPotion();
		}
		else if(selectedItem == manaIndex){
			sell[selectedItem] = new ManaPotion();
		}
		else if(selectedItem == amuletIndex){
			sell[selectedItem] = new Amulet();
		}
		else if(selectedItem == ringIndex){
			sell[selectedItem] = new Ring();
		}
	}
	
	void OnGUI(){
		if(checkStatus()){
			GUI.Box (new Rect(boxStartPositionWidth,boxStartPositionHeight,boxWidth,boxHeight), "", styleBox);
			if(selectedItem < size){
				GUI.Label (new Rect(ImageBoxLocationWidth, ImageBoxLocationHeight, ImageBoxWidth, ImageBoxHeight),sell[selectedItem].getImage(), standartSkin);
			}
			if(selectedItem < size){
				GUI.Label (new Rect(descriptionLocationWidth, descriptionLocationHeight, descriptionBoxWidth, descriptionBoxHeight), sell[selectedItem].getDescription(), standartSkin);
			}
			if(GUI.Button (new Rect(buyBoxStartPositionWidth, buyBoxStartPositionHeight, buyItemBoxWidth, buyItemBoxHeight), checkEnough())){
				if(Sorcerer.soulShards >= sell[selectedItem].getPrice()){
					if(inventory.takeItem(sell[selectedItem])){
						Sorcerer.soulShards = Sorcerer.soulShards - sell[selectedItem].getPrice();
						recreateItem();
					}
					else{
						full = true;
					}
				}
				else{
					buyable = false;
				}
			}
			if(GUI.Button (new Rect(closeBoxStartPositionWidth, closeBoxStartPositionHeight, closeItemBoxWidth, closeItemBoxHeight), closeButton)){
				sorcererShop = !sorcererShop;
			}
			
			GUI.Label (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset/2, regularItemBoxWidth, regularItemBoxHeight), greetingMessage, standartSkin);
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 2, regularItemBoxWidth, regularItemBoxHeight), sell[healthIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = healthIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 3, regularItemBoxWidth, regularItemBoxHeight), sell[manaIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = manaIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 4, regularItemBoxWidth, regularItemBoxHeight), sell[amuletIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = amuletIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 5, regularItemBoxWidth, regularItemBoxHeight), sell[ringIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = ringIndex;
			}
		}
	}
}
