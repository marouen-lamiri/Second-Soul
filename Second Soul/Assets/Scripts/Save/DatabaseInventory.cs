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
				if(inventorySlots[i,j].occupied){
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "true" );
					PlayerPrefs.SetInt ("Item position x", inventoryItems[i].getX());
					PlayerPrefs.SetInt ("Item position y", inventoryItems[i].getY());
					PlayerPrefs.SetString("Item type" + i, inventoryItems[i].getTypeAsString());
				}
				else{
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "false" );
				}
			}
		}
	}

	public void recreateItem(int x, int y, int i){
		if(PlayerPrefs.GetString("Item type") == "ManaPotion"){
			inventory.addInventoryItem(x,y,new ManaPotion());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "HealthPotion"){
			inventory.addInventoryItem(x,y,new HealthPotion());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Axe"){
			inventory.addInventoryItem(x,y,new Axe());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Ring"){
			inventory.addInventoryItem(x,y,new Ring());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Chest"){
			inventory.addInventoryItem(x,y,new Chest());
		}
		else if(PlayerPrefs.GetString("Item type") == "Boots"){
			inventory.addInventoryItem(x,y,new Boots());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Amulet"){
			inventory.addInventoryItem(x,y,new Amulet());
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Sword"){
			inventory.addInventoryItem(x,y,new Sword());
		}
	}
	
	public void readItems(){
		Slot [,] inventorySlots = inventory.getInventorySlots();
		List<Item> inventoryItems = inventory.getInventoryItems();
		for(int i = 0; i < inventory.getStorageSizeWidth(); i++){
			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
				if(PlayerPrefs.GetString("Inventory Slot at" + i + "/" + j + " status") == "true"){
					string itemType = (string)PlayerPrefs.GetString("Item type" + i);
					Debug.Log (itemType);
					int x = (int)PlayerPrefs.GetInt("Item position x");
					int y = (int)PlayerPrefs.GetInt("Item position y");
					recreateItem(x, y, i);

				}
			}
		}

	}
}
