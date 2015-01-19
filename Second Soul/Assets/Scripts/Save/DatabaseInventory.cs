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
	private EquipSlot slots; 
<<<<<<< HEAD
=======
	private Player player;
>>>>>>> a832b619c6f4144446dd81950edcb02bed4bdde9
	
	void Start () {
		inventory = (Inventory) GameObject.FindObjectOfType (typeof (Inventory));
		player = (Player) GameObject.FindObjectOfType (typeof(Player));
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
<<<<<<< HEAD
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
=======
		if(Network.isServer){//if (player.GetType () != typeof (Sorcerer)) {
			inventoryItems = inventory.getInventoryItems ();
			int counter = 0;
			for (int i = 0; i < inventoryItems.Count; i++) {
				PlayerPrefs.SetInt ("Fighter Item position x" + i, inventoryItems [i].getX ());
				PlayerPrefs.SetInt ("Fighter Item position y" + i, inventoryItems [i].getY ());
				PlayerPrefs.SetString ("Fighter Item type" + i, inventoryItems [i].getTypeAsString ());
				Debug.Log (inventoryItems [i].getTypeAsString ());
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
>>>>>>> a832b619c6f4144446dd81950edcb02bed4bdde9
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

<<<<<<< HEAD
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
=======
	public void recreateFighterItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Fighter Item type" + i) == "ManaPotion"){
			item = new ManaPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "HealthPotion"){
			item = new HealthPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "Axe"){
			item = new Axe();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString( "Fighter Item type" + i) == "Ring"){
			item = new Ring();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Fighter Item type" + i) == "Chest"){
			item = new Chest();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Fighter Item type" + i) == "Boots"){
			item = new Boots();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Fighter Item type"  + i) == "Amulet"){
			item = new Amulet();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString( "Fighter Item type" + i) == "Sword"){
>>>>>>> a832b619c6f4144446dd81950edcb02bed4bdde9
			item = new Sword();
			inventory.addInventoryItem(x,y,item);
		}
	}

<<<<<<< HEAD
=======
	public void recreateSorcererItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "ManaPotion"){
			item = new ManaPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "HealthPotion"){
			item = new HealthPotion();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "Axe"){
			item = new Axe();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Ring"){
			item = new Ring();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Chest"){
			item = new Chest();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type" + i) == "Boots"){
			item = new Boots();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString("Sorcerer Item type"  + i) == "Amulet"){
			item = new Amulet();
			inventory.addInventoryItem(x,y,item);
		}
		else if(PlayerPrefs.GetString( "Sorcerer Item type" + i) == "Sword"){
			item = new Sword();
			inventory.addInventoryItem(x,y,item);
		}
	}


>>>>>>> a832b619c6f4144446dd81950edcb02bed4bdde9
	public void recreateEquipItem(int x, int y, int i){
		Item item;
		if(PlayerPrefs.GetString("Equiped Item type" + i) == "Axe"){
			item = new Axe();
			item.useItem();
			//add item to axe slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Ring"){
			item = new Ring();
			item.useItem();
			//add item to ring slot);
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Chest"){
			item = new Chest();
			item.useItem();
			//add item to chest slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Boots"){
			item = new Boots();
			item.useItem();
			//add item to boots slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Amulet"){
			item = new Amulet();
			item.useItem();
			//add item to amulet slot
		}
		else if(PlayerPrefs.GetString("Equiped Item type" + i) == "Sword"){
			item = new Sword();
			item.useItem();
			//add item to sword slot
		}
	}
	
	public void readItems(){
		inventorySlots = inventory.getInventorySlots();
		inventoryItems = inventory.getInventoryItems();
<<<<<<< HEAD
		for(int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++){
			int x = PlayerPrefs.GetInt("Item position x" + i);
			int y = PlayerPrefs.GetInt("Item position y" + i);
			recreateItem(x, y, i);
		}
		for(int i = 0; i < PlayerPrefs.GetInt ("Equiped Item Total"); i++){ 
			Debug.Log ("hello2");
			int x = PlayerPrefs.GetInt("Equiped Item position x" + i);
			int y = PlayerPrefs.GetInt("Equiped Item position y" + i);
			recreateEquipItem(x, y, i);
		}
=======
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
			Debug.Log ("hello2");
			int x = PlayerPrefs.GetInt("Equiped Item position x" + i);
			int y = PlayerPrefs.GetInt("Equiped Item position y" + i);
			recreateEquipItem(x, y, i);
		}
>>>>>>> a832b619c6f4144446dd81950edcb02bed4bdde9
	}
}
