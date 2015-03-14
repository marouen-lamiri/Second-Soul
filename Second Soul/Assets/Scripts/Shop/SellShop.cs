using UnityEngine;
using System.Collections;

public class SellShop : Shop {

	private int selectedItem = 6;
	private int size = 6;
	private Inventory inventory;
	public static bool close;
	private bool sellable;
	private int swordIndex = 0;
	private int axeIndex = 1;
	private int chestIndex = 2;
	private int bootsIndex = 3;
	private int amuletIndex = 4;
	private int ringIndex = 5;
	private int numberOfItems;

	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		inventory.takeItem(new Axe());
		sampleItems();
	}

	public override bool shopEnabled(){
		return checkStatus() && inBoundaries();
	}
	
	public bool checkStatus(){
		return close;
	}

	int nbrItems(Item item){
		int count = 0;
		for(int i = 0; i < inventory.getInventoryItems().Count; i++){
			if(inventory.getInventoryItems()[i].GetType() == item.GetType()){
				count++;
			}
		}
		return count;
	}

	bool containsItem (Item item){
		for(int i = 0; i < inventory.getInventoryItems().Count; i++){
			if(inventory.getInventoryItems()[i].GetType() == item.GetType()){
				return true;
			}
		}
		return false;
	}

	void removeItem (Item item){
		for(int i = 0; i < inventory.getInventoryItems().Count; i++){
			if(inventory.getInventoryItems()[i].GetType() == item.GetType()){
				inventory.getInventoryItems().Remove(inventory.getInventoryItems()[i]);
				return; //this is due to the fact that we only want to sell one item
			}
		}
	}

	Item retrieveItem(){
		if(selectedItem == swordIndex){
			return sell[0];
		}
		else if(selectedItem == axeIndex){
			return sell[1];
		}
		else if(selectedItem == chestIndex){
			return sell[2];
		}
		else if(selectedItem == bootsIndex){
			return sell[3];
		}
		else if(selectedItem == amuletIndex){
			return sell[4];
		}
		else{
			return sell[5];
		}
	}

	void sampleItems(){
		sell.Add (new Sword());
		sell.Add (new Axe());
		sell.Add (new Chest());
		sell.Add (new Boots());
		sell.Add (new Amulet());
		sell.Add (new Ring());
	}

	string checkEnough(){
		if(sellable){
			return sellButton + numberOfItems;
		}
		return sellError;
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
				if(containsItem(retrieveItem())){
					if(Network.isServer){
						Debug.Log ("Selling");
						removeItem(retrieveItem());
						Fighter.gold += retrieveItemPrice(retrieveItem());
					}
					else if(Network.isClient){
						removeItem(retrieveItem());
						Sorcerer.soulShards += retrieveItemPrice(retrieveItem());
					}
				}
				else{
					sellable = false;
				}
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(closeBoxStartPositionWidth, closeBoxStartPositionHeight, closeItemBoxWidth, closeItemBoxHeight), closeButton)){
				close = !close;
			}
			
			GUI.Label (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset/2, regularItemBoxWidth, regularItemBoxHeight), greetingMessage, standartSkin);
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 2, regularItemBoxWidth, regularItemBoxHeight), sell[swordIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = swordIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 3, regularItemBoxWidth, regularItemBoxHeight), sell[axeIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = axeIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 4, regularItemBoxWidth, regularItemBoxHeight), sell[chestIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = chestIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 5, regularItemBoxWidth, regularItemBoxHeight), sell[bootsIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = bootsIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 6, regularItemBoxWidth, regularItemBoxHeight), sell[amuletIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = amuletIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 7, regularItemBoxWidth, regularItemBoxHeight), sell[ringIndex].getString(), buttonStyle)){
				sellable = true;
				selectedItem = ringIndex;
				numberOfItems = nbrItems(retrieveItem());
			}
		}
	}
}
