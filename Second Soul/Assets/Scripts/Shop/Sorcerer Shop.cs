using UnityEngine;
using System.Collections;

public class SorcererShop : Shop {

	private bool sorcererShop;
	private int selectedItem = 4;
	private Inventory inventory;
	private Player player;
	
	// Use this for initialization
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		player = (Player) GameObject.FindObjectOfType (typeof(Player));
		//Set the list of items that the sorcerer can buy
		sell.Add(new HealthPotion()); 
		sell.Add(new ManaPotion());
		sell.Add(new Amulet());
		sell.Add(new Ring());
	}

	public override bool shopEnabled(){
		return sorcererShop && inBoundaries();
	}

	void OnGUI(){
		if(sorcererShop){
			GUI.Box (new Rect(boxStartPositionWidth,boxStartPositionHeight,boxWidth,boxHeight), "", styleBox);
			if(selectedItem < 4){
				GUI.Label (new Rect(ImageBoxLocationWidth, ImageBoxLocationHeight, ImageBoxWidth, ImageBoxHeight), sell[selectedItem].getImage(), standartSkin);
			}
			if(selectedItem < 4){
				GUI.Label (new Rect(descriptionLocationWidth, descriptionLocationHeight, descriptionBoxWidth, descriptionBoxHeight), sell[selectedItem].getDescription(), standartSkin);
			}
			if(GUI.Button (new Rect(buyBoxStartPositionWidth, buyBoxStartPositionHeight, buyItemBoxWidth, buyItemBoxHeight), buyButton)){
				inventory.takeItem(sell[selectedItem]);
			}
			if(GUI.Button (new Rect(closeBoxStartPositionWidth, closeBoxStartPositionHeight, closeItemBoxWidth, closeItemBoxHeight), closeButton)){
				sorcererShop = !sorcererShop;
			}
			
			
			GUI.Label (new Rect(regularItemPositionWidth, regularItemPositionHeight + offset/2, regularItemBoxWidth, regularItemBoxHeight), greetingMessage, standartSkin);
			Debug.Log (sell[0]);
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

		}	
	}
}
