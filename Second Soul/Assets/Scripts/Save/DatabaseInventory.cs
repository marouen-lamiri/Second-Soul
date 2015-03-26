using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseInventory : MonoBehaviour {

	// Use this for initialization
	private int interval = 3000;
	private int count;
	private Slot [,] inventorySlots;
	private List<Item> inventoryItems;
	private List<Item> equipItems;
	private Inventory inventory;
	private EquipSlot slots; 
	private Player player;
	
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		player = (Player) GameObject.FindObjectOfType (typeof(Player));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			//save
			//Debug.Log ("Save Inventory!");
			saveItems();
			saveEquipItems();
			PlayerPrefs.Save();
			count = 0;
		}
		count++;
	}

	void saveItems(){
		//Debug.Log ("Step 1");
		if(Network.isServer){//if (player.GetType () != typeof (Sorcerer)) {
			inventoryItems = inventory.getInventoryItems ();
			int counter = 0;
			for (int i = 0; i < inventoryItems.Count; i++) {
				PlayerPrefs.SetInt ("Fighter Item position x" + i, inventoryItems [i].getX ());
				PlayerPrefs.SetInt ("Fighter Item position y" + i, inventoryItems [i].getY ());
				PlayerPrefs.SetString ("Fighter Item type" + i, inventoryItems [i].GetType().ToString());
				//Debug.Log (inventoryItems [i].getTypeAsString ());
				counter++;
			}
			PlayerPrefs.SetInt ("Fighter Item Total", counter);
		}
//		else{
//			inventoryItems = inventory.getInventoryItems ();
//			int counter = 0;
//			for (int i = 0; i < inventoryItems.Count; i++) {
//				PlayerPrefs.SetInt ("Sorcerer Item position x" + i, inventoryItems [i].getX ());
//				PlayerPrefs.SetInt ("Sorcerer Item position y" + i, inventoryItems [i].getY ());
//				PlayerPrefs.SetString ("Sorcerer Item type" + i, inventoryItems [i].getTypeAsString ());
//				Debug.Log (inventoryItems [i].getTypeAsString ());
//				counter++;
//			}
//			PlayerPrefs.SetInt ("Sorcerer Item Total", counter);
//		}
	}

	void saveEquipItems(){
		equipItems = inventory.getEquipItems();
		int counter = 0;
		for (int i = 0; i < equipItems.Count; i++){
			PlayerPrefs.SetInt ("Equiped Item position x" + i, equipItems[i].getX());
			PlayerPrefs.SetInt ("Equiped Item position y" + i, equipItems[i].getY());
			PlayerPrefs.SetString("Equiped Item type" + i, equipItems[i].GetType().ToString());
			//Debug.Log (equipItems[i].getTypeAsString());
			counter++;
		}
		PlayerPrefs.SetInt ("Equiped Item Total", counter);
	}

	public void recreateFighterItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Fighter Item type" + i) == "ManaPotion"){
			item = new ManaPotion();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "HealthPotion"){
			item = new HealthPotion();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "Axe"){
			item = new Axe();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString( "Fighter Item type" + i) == "Ring"){
			item = new Ring();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Fighter Item type" + i) == "Chest"){
			item = new Chest();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Fighter Item type" + i) == "Boots"){
			item = new Boots();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "Amulet"){
			item = new Amulet();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString( "Fighter Item type" + i) == "Sword"){
			item = new Sword();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
	}

	public void recreateSorcererItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "ManaPotion"){
			item = new ManaPotion();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "HealthPotion"){
			item = new HealthPotion();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "Axe"){
			item = new Axe();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Ring"){
			item = new Ring();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Chest"){
			item = new Chest();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Boots"){
			item = new Boots();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "Amulet"){
			item = new Amulet();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
		else if(PlayerPrefs.GetString( "Sorcerer Item type" + i) == "Sword"){
			item = new Sword();
			inventory.addItem(x,y,item, inventorySlots, inventoryItems);
		}
	}


	public void recreateEquipItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Equiped Item type" + i) == "Axe"){
			item = new Axe();
			item.useItem(player);
			//add item to axe slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Ring"){
			item = new Ring();
			item.useItem(player);
			//add item to ring slot);
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Chest"){
			item = new Chest();
			item.useItem(player);
			//add item to chest slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Boots"){
			item = new Boots();
			item.useItem(player);
			//add item to boots slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Amulet"){
			item = new Amulet();
			item.useItem(player);
			//add item to amulet slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Sword"){
			item = new Sword();
			item.useItem(player);
			//add item to sword slot
		}
	}
	
	public void readItems(){
		inventorySlots = inventory.getInventorySlots();
		inventoryItems = inventory.getInventoryItems();
		if(Network.isServer){//if(player.GetType() != typeof (Sorcerer)){
			for(int i = 0; i < PlayerPrefs.GetInt("Fighter Item Total"); i++){
				int x = PlayerPrefs.GetInt("Fighter Item position x" + i);
				int y = PlayerPrefs.GetInt("FIghter Item position y" + i);
				recreateFighterItem(x,y,i);
			}
		}
		else{
			for(int i = 0; i < PlayerPrefs.GetInt("Sorcerer Item Total"); i++){
				int x = PlayerPrefs.GetInt("Sorcerer Item position x" + i);
				int y = PlayerPrefs.GetInt("Sorcerer Item position y" + i);
				recreateSorcererItem(x,y,i);
			}
		}
		for(int i = 0; i < PlayerPrefs.GetInt ("Equiped Item Total"); i++){ 
			//Debug.Log ("hello2");
			int x = PlayerPrefs.GetInt("Equiped Item position x" + i);
			int y = PlayerPrefs.GetInt("Equiped Item position y" + i);
			recreateEquipItem(x, y, i);
		}
	}
}
