using UnityEngine;
using System.Collections.Generic;

public class Inventory : Storage 
{
	public Texture2D image;

	public int slotsOffsetX;
	public int slotsOffsetY;
	DatabaseInventory database;

	// Use this for initialization
	void Awake(){
		//position.x = Screen.width - position.width;
		//position.y = Screen.height - position.height - Screen.height * 0.2f;
		player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		player2 = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		//player.inventory = this;
	}
	
	void Start () {
		initializeVariables(); // in parent Storage class
		setSlots ();
		//addSampleItems ();
		database = (DatabaseInventory)Inventory.FindObjectOfType(typeof(DatabaseInventory));
		database.readItems();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("i")) {
			shownInventory();
		}
	}

	public void sayhi(){
		Debug.Log ("works");
	}
	
	void setSlots(){
		inventorySlots = new Slot[inventoryStorageWidth,inventoryStorageHeight];
		for(int x = 0; x < inventoryStorageWidth ; x++){
			for(int y = 0; y < inventoryStorageHeight ; y++){
				inventorySlots[x,y] = new Slot(new Rect(slotsOffsetX + slotWidth*x, slotsOffsetY + slotHeight*y, slotWidth, slotHeight));
			}
		}
	}
	
	void addSampleItems(){
		addInventoryItem(0, 0, new Chest());
		addInventoryItem(2, 2, new HealthPotion());
		addInventoryItem(2, 3, new ManaPotion());
		addInventoryItem(3, 3, new Ring());
		addInventoryItem(4, 2, new Axe());
	}
	
	void shownInventory(){
		if (isInventoryOn) {
			isInventoryOn = false;
		}
		else {
			isInventoryOn = true;
		}
	}
	
	void OnGUI(){
		if (isInventoryOn) {
			drawInventory ();
			//drawSlots ();
			drawItems ();
			detectGUIAction ();
			drawHoverItem();
			//Debug.Log("id: " + GUIUtility.hotControl);
			//Debug.Log (itemPickedUp);
		}
	}
	
	public bool takeItem(Item item){
		int newX;
		int newY;
		if (!firstAvailableInventorySlots (out newX, out newY, item)) {
			return false;
		}
		addInventoryItem(newX, newY, item);
		return true;
	}


	void drawInventory(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}

	void drawItems(){
		for(int i = 0; i < inventoryItems.Count; i++){
			inventoryItems[i].position = new Rect(6 + slotsOffsetX + position.x + inventoryItems[i].x * slotWidth, 6 + slotsOffsetY + position.y + inventoryItems[i].y * slotHeight,inventoryItems[i].width * slotWidth - 12,inventoryItems[i].height * slotHeight - 12);
			GUI.DrawTexture(inventoryItems[i].position, inventoryItems[i].getImage());
		}
	}
}
