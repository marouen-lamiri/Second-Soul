using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseInventory : MonoBehaviour {

	// Use this for initialization
	private int interval = 300;
	private int count;
	private Slot [,] inventorySlots;
	private List<Item> inventoryItems;
	private List<Item> equipItems;
	private Inventory inventory;
	private EquipSlot equipSlots;
	
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			Debug.Log ("Save Inventory!");
			saveItems();
			saveEquipItems();
			PlayerPrefs.Save();
			count = 0;
		}
		count++;
	}

	void saveItems(){
		Debug.Log ("Step 1");
		inventoryItems = inventory.getInventoryItems();
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

	void saveEquipItems(){
		equipItems = inventory.getEquipItems();
		int counter = 0;
		for (int i = 0; i < equipItems.Count; i++){
			PlayerPrefs.SetInt ("Equiped Item position x" + i, equipItems[i].getX());
			PlayerPrefs.SetInt ("Equiped Item position y" + i, equipItems[i].getY());
			PlayerPrefs.SetString("Equiped Item type" + i, equipItems[i].getTypeAsString());
			Debug.Log (equipItems[i].getTypeAsString());
			counter++;
		}
		PlayerPrefs.SetInt ("Equiped Item Total", counter);
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

	public void recreateEquipItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Equiped Item type" + i) == "Axe"){
			item = new Axe();
			//add item to axe slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Ring"){
			item = new Ring();
			//add item to ring slot);
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Chest"){
			item = new Chest();
			//add item to chest slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Boots"){
			item = new Boots();
			//add item to boots slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Amulet"){
			item = new Amulet();
			//add item to amulet slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Sword"){
			item = new Sword();
			//add item to sword slot
		}
	}
	
	public void readItems(){
		inventorySlots = inventory.getInventorySlots();
		inventoryItems = inventory.getInventoryItems();
		for(int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++){
			Debug.Log ("Hello");
			int x = PlayerPrefs.GetInt("Item position x" + i);
			int y = PlayerPrefs.GetInt("Item position y" + i);
			recreateItem(x, y, i);
		}
		for(int i = 0; i < PlayerPrefs.GetInt ("Equiped Item Total"); i++){
			int x = PlayerPrefs.GetInt("Equiped Item position x" + i);
			int y = PlayerPrefs.GetInt("Equiped Item position y" + i);
			recreateEquipItem(x, y, i);
		}
	}
}
