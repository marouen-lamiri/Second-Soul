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
			Debug.Log ("Save Inventory!");
			saveItems();
			PlayerPrefs.Save();
			count = 0;
		}
		count++;
	}

	void saveItems(){
		Slot [,] inventorySlots = inventory.getInventorySlots();
		List<Item> inventoryItems = inventory.getInventoryItems();
		Debug.Log ("Before Loop, awaiting Item type!");
		for (int i = 0; i < inventory.getStorageSizeWidth(); i++){
			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
				if(inventorySlots[i,j].occupied){
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "true" );
					if((inventoryItems[i].getWidth() <= 1 && inventoryItems[i].getHeight() <= 1) 
					   && inventoryItems[i-1].getTypeAsString() != inventoryItems[i].getTypeAsString()){
						PlayerPrefs.SetInt ("Item position x" + i + j, inventoryItems[i].getX());
						PlayerPrefs.SetInt ("Item position y" + i + j, inventoryItems[i].getY());
						PlayerPrefs.SetString("Item type" + i + j, inventoryItems[i].getTypeAsString());
						Debug.Log ("Saving item of type: " + inventoryItems[i].getTypeAsString() + " at: " + i + " /" + j);
					}
					Debug.Log ("Item already saved of type: " + inventoryItems[i].getTypeAsString() + " at: " + i + " /" + j);
				}
				else{
					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "false" );
					Debug.Log ("During Loop, No Item type at: " + i + " /" + j);
				}
			}
		}
	}

	public void recreateItem(int x, int y, int i, int j){
		if(PlayerPrefs.GetString("Item type" + i + j) == "ManaPotion"){
			inventory.addInventoryItem(x,y,new ManaPotion());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "HealthPotion"){
			inventory.addInventoryItem(x,y,new HealthPotion());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Axe"){
			inventory.addInventoryItem(x,y,new Axe());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Ring"){
			inventory.addInventoryItem(x,y,new Ring());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Chest"){
			inventory.addInventoryItem(x,y,new Chest());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Boots"){
			inventory.addInventoryItem(x,y,new Boots());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Amulet"){
			inventory.addInventoryItem(x,y,new Amulet());
		}
		else if(PlayerPrefs.GetString("Item type" + i + j) == "Sword"){
			inventory.addInventoryItem(x,y,new Sword());
		}
	}
	
	public void readItems(){
		Slot [,] inventorySlots = inventory.getInventorySlots();
		List<Item> inventoryItems = inventory.getInventoryItems();
		for(int i = 0; i < inventory.getStorageSizeWidth(); i++){
			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
				if(PlayerPrefs.GetString("Inventory Slot at" + i + "/" + j + " status") == "true"){
					string itemType = (string)PlayerPrefs.GetString("Item type" + i + j);
					Debug.Log (itemType);
					int x = PlayerPrefs.GetInt("Item position x" + i + j);
					int y = PlayerPrefs.GetInt("Item position y" + i + j);
					recreateItem(x, y, i, j);
				}
			}
		}

	}
}
