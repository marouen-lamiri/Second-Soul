using UnityEngine;
using System.Collections.Generic;

public class Inventory : Storage 
{
	public Texture2D image;
	public GUIStyle currencyStyle;
	public Texture2D goldImage;
	public Texture2D soulImage;

	public int slotsOffsetX;
	public int slotsOffsetY;

	public int labelPositionHeight;
	public int labelPositionWidth;
	public int labelWidth = 25;
	public int labelHeight = 25;
	
	public int guiDepth = 1;
	
	DatabaseInventory database;

	// Use this for initialization
	void Awake(){
		//position.x = Screen.width - position.width;
		//position.y = Screen.height - position.height - Screen.height * 0.2f;
		//player = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		//player2 = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		//was this next line supposed to be commented out? it was, but it seems essential. Not having it broke loot pickup for me. Was this meant to be initialized elsewhere?
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
			toggleTab();
		}
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
	
	protected void toggleTab(){
		isInventoryOn = !isInventoryOn;
	}
	
	void OnGUI(){
		centeredStyle = GUI.skin.GetStyle("textarea");
		centeredStyle.fontSize = 16;
		centeredStyle.wordWrap = true;
		GUI.depth = guiDepth;
		if (isInventoryOn) {
			drawInventory ();
			//drawSlots ();
			drawItems ();
			detectGUIAction ();
			drawHoverItem();
			writeMoney();
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

	protected override void drawItems(){
		for(int i = 0; i < inventoryItems.Count; i++){
			inventoryItems[i].position = new Rect(6 + slotsOffsetX + position.x + inventoryItems[i].x * slotWidth, 
													6 + slotsOffsetY + position.y + inventoryItems[i].y * slotHeight, 
													inventoryItems[i].width * slotWidth - 12, 
													inventoryItems[i].height * slotHeight - 12);
			GUI.DrawTexture(inventoryItems[i].position, inventoryItems[i].getImage());
		}
	}

	void writeMoney(){
		if(Network.isServer){
			GUI.Label(new Rect(position.x + labelPositionWidth, position.y + labelPositionHeight, labelWidth, labelHeight), Fighter.gold + "", currencyStyle);
			GUI.Label(new Rect(position.x + labelPositionWidth + labelWidth, position.y + labelPositionHeight - labelHeight/8, labelWidth, labelHeight), goldImage);
		}
		else if(Network.isClient){
			GUI.Label(new Rect(position.x + labelPositionWidth, position.y + labelPositionHeight, labelWidth, labelHeight), Sorcerer.soulShards + "", currencyStyle);
			GUI.Label(new Rect(position.x + labelPositionWidth + labelWidth, position.y + labelPositionHeight - labelHeight/8, labelWidth, labelHeight), soulImage);
		}
	}
}
