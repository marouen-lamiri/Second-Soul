using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SorcererShop : Shop {
	
	private bool sorcererShop;
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

	public override void clicked(){
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distance)){
				if (hit.transform.name == shopDoor){
					sorcererShop = true;
				}
			}
		}
	}
		
	string checkEnough(){
		if(buyable){
			return buyButton;
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
		clicked();
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
					Sorcerer.soulShards = Sorcerer.soulShards - sell[selectedItem].getPrice();
					inventory.takeItem(sell[selectedItem]);
					recreateItem();
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
				buyable = true;
				selectedItem = healthIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 3, regularItemBoxWidth, regularItemBoxHeight), sell[manaIndex].getString(), buttonStyle)){
				buyable = true;
				selectedItem = manaIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 4, regularItemBoxWidth, regularItemBoxHeight), sell[amuletIndex].getString(), buttonStyle)){
				buyable = true;
				selectedItem = amuletIndex;
			}
			if(GUI.Button (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset * 5, regularItemBoxWidth, regularItemBoxHeight), sell[ringIndex].getString(), buttonStyle)){
				buyable = true;
				selectedItem = ringIndex;
			}
		}
	}
}
