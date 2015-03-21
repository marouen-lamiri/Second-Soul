using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterShop : Shop {
	
	public static bool fighterShop;
	private bool buyable = true;
	private bool full = false;
	private int size = 7;
	private int selectedItem = 7;
	private Inventory inventory;
	private Player player;
	private int healthIndex = 0;
	private int manaIndex = 1;
	private int swordIndex = 2;
	private int axeIndex = 3;
	private int chestIndex = 4;
	private int bootsIndex = 5;
	
	// Use this for initialization
	void Start () {
		//for testing purposes
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		//Set the list of items that the fighter can buy
		sell.Add(new HealthPotion()); 
		sell.Add (new ManaPotion());
		sell.Add (new Sword());
		sell.Add (new Axe());
		sell.Add(new Chest());
		sell.Add (new Boots());
	}

	public override bool shopEnabled(){
		return checkStatus() && inBoundaries();
	}

	public bool checkStatus(){
		return fighterShop && Network.isServer;
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
		else if(selectedItem == swordIndex){
			sell[selectedItem] = new Sword();
		}
		else if(selectedItem == axeIndex){
			sell[selectedItem] = new Axe();
		}
		else if(selectedItem == chestIndex){
			sell[selectedItem] = new Chest();
		}
		else if(selectedItem == bootsIndex){
			sell[selectedItem] = new Boots();
		}
	}

	// Update is called once per frame
	void OnGUI () {
		if(checkStatus()){
			GUI.Box (new Rect(boxStartPositionWidth,boxStartPositionHeight,boxWidth,boxHeight), "", styleBox);
			if(selectedItem < size){
				GUI.Label (new Rect(ImageBoxLocationWidth, ImageBoxLocationHeight, ImageBoxWidth, ImageBoxHeight),sell[selectedItem].getImage(), standartSkin);
			}
			if(selectedItem < size){
				GUI.Label (new Rect(descriptionLocationWidth, descriptionLocationHeight, descriptionBoxWidth, descriptionBoxHeight), sell[selectedItem].getDescription(), standartSkin);
			}
			if(GUI.Button (new Rect(buyBoxStartPositionWidth, buyBoxStartPositionHeight, buyItemBoxWidth, buyItemBoxHeight), checkEnough())){
				if(Fighter.gold >= sell[selectedItem].getPrice()){
					if(inventory.takeItem(sell[selectedItem])){
						Fighter.gold = Fighter.gold - sell[selectedItem].getPrice();
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
				fighterShop = !fighterShop;
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
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 4, regularItemBoxWidth, regularItemBoxHeight), sell[swordIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = swordIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 5, regularItemBoxWidth, regularItemBoxHeight), sell[axeIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = axeIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 6, regularItemBoxWidth, regularItemBoxHeight), sell[chestIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = chestIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 7, regularItemBoxWidth, regularItemBoxHeight), sell[bootsIndex].getString(), buttonStyle)){
				full = false;
				buyable = true;
				selectedItem = bootsIndex;
			}
		}	
	}
}
