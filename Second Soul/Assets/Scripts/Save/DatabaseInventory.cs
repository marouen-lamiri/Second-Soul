using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseInventory : MonoBehaviour {

	// Use this for initialization
	private int interval = 300;
	private int count;
	private Slot [,] inventorySlots;
	private List<Item> inventoryItems;
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
		Debug.Log ("Step 1");
		inventoryItems = inventory.getInventoryItems();
//		for (int i = 0; i < inventory.getStorageSizeWidth(); i++){
//			for(int j = 0; j < inventory.getStorageSizeHeight(); j++){
//				if(inventorySlots[i,j].occupied){
//					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "true" );
//					PlayerPrefs.SetInt ("Item position x" + i + "/" + j, inventoryItems[i].getX());
//					PlayerPrefs.SetInt ("Item position y" + i + "/" + j, inventoryItems[i].getY());
//					PlayerPrefs.SetString("Item type" + i + "/" + j, inventoryItems[i].getTypeAsString());
//				}
//				else{
//					PlayerPrefs.SetString("Inventory Slot at" + i + "/" + j+ " status", "false" );
//				}
//			}
//		}
		int counter = 0;
		for (int i = 0; i < inventoryItems.Count; i++){
			PlayerPrefs.SetInt ("Item position x" + i, inventoryItems[i].getX());
			PlayerPrefs.SetInt ("Item position y" + i, inventoryItems[i].getY());
			PlayerPrefs.SetString("Item type" + i, inventoryItems[i].getTypeAsString());
			Debug.Log (inventoryItems[i].getTypeAsString());
			counter++;
		}
		PlayerPrefs.SetInt ("Item Total", counter);
	}

	public void recreateItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Item type" + i) == "ManaPotion"){
			item = new ManaPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "HealthPotion"){
			item = new HealthPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Axe"){
			item = new Axe();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Ring"){
			item = new Ring();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Chest"){
			item = new Chest();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Boots"){
			item = new Boots();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Amulet"){
			item = new Amulet();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Item type" + i) == "Sword"){
			item = new Sword();
			inventory.addInventoryItem(x,y,item);
		}
	}
	
	public void readItems(){
		inventorySlots = inventory.getInventorySlots();
		inventoryItems = inventory.getInventoryItems();
		for(int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++){
			Debug.Log ("Hello");
			/*for(int j = 0; j < inventory.getStorageSizeHeight(); j++){*/
			int x = PlayerPrefs.GetInt("Item position x" + i);
			int y = PlayerPrefs.GetInt("Item position y" + i);
			recreateItem(x, y, i /*j*/);
		}
	}
}
