using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseInventory : MonoBehaviour {

	// Use this for initialization
	int interval = 300;
	int count;
	private Inventory inventory;
	
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			Debug.Log ("Save Fighter's Primary Stats!");
			saveItems();
			count = 0;
		}
		count++;
	}

	void compareSlotPosToItemPos(){

	}

	void saveItems(){
		Slot [,] inventorySlots = inventory.getInventorySlots();
		List<Item> inventoryItems = inventory.getInventoryItems();
		for (int i = 0; i < inventory.getStorageSizeWidth(); i++){
			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
				Debug.Log (inventorySlots);
				if(inventorySlots[i,j].occupied){
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "true" );
					PlayerPrefs.SetInt ("Item position x", inventoryItems[i].getX());
					PlayerPrefs.SetInt ("Item position y", inventoryItems[i].getY());
					PlayerPrefs.SetString("Item type", inventoryItems[i].getTypeAsString());
				}
				else{
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "false" );
				}
			}
		}
	}
	
	public void readItems(){
		Slot [,] inventorySlots = inventory.getInventorySlots();
		List<Item> inventoryItems = inventory.getInventoryItems();
		for(int i = 0; i < inventory.getStorageSizeWidth(); i++){
			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
				if(PlayerPrefs.GetString("Inventory Slot at" + i + "/" + j+ " status") == "true"){
					Debug.Log ("Load");
					string itemType = (string)PlayerPrefs.GetString("Item type");
					int x = (int)PlayerPrefs.GetInt("Item position x");
					int y = (int)PlayerPrefs.GetInt("Item position y");
					if(itemType == "ManaPotion"){
						inventory.addInventoryItem(x,y,new ManaPotion());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "HealthPotion"){
						inventory.addInventoryItem(x,y,new HealthPotion());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Axe"){
						inventory.addInventoryItem(x,y,new Axe());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Ring"){
						inventory.addInventoryItem(x,y,new Ring());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Chest"){
						inventory.addInventoryItem(x,y,new Chest());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Boots"){
						inventory.addInventoryItem(x,y,new Boots());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Amulet"){
						inventory.addInventoryItem(x,y,new Amulet());
						inventorySlots[i,j].occupied = true;
					}
					else if(itemType == "Sword"){
						inventory.addInventoryItem(x,y,new Sword());
						inventorySlots[i,j].occupied = true;
					}
					else{
						continue;
					}

				}
			}
		}

	}
}
