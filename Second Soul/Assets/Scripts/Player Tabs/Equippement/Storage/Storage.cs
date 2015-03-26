using UnityEngine;
using System.Collections.Generic;

public abstract class Storage : MonoBehaviour {
	protected Player player;
	protected Player player2;

	public Rect position;

	//Gui Messages
	protected string priceMessage = "\nThe Price in Store: ";
	protected string statsMessage = "\nBelow are the stats it effects:\n";
	protected int width = 200;
	protected int height = 150;

	//different item collections
	protected static List<Item> inventoryItems = new List<Item>();
	public static List<Item> equipItems = new List<Item>();
	public static List<Item> stashItems = new List<Item>();
	
	//slots for different storage types
	protected static Slot[,] inventorySlots;
	public static List<EquipSlot> equipSlots = new List<EquipSlot>();
	public static Slot[,] stashSlots;
	
	// in number of slots
	protected int inventoryStorageWidth = 6;
	protected int inventoryStorageHeight = 4;
	
	// in number of slots
	protected int stashStorageWidth = 6;
	protected int stashStorageHeight = 9;

	protected static bool isInventoryOn;
	protected static bool isStashOn;
	
	//used for targeting and picking up (hovering) item
	protected Item targetItem;
	protected bool itemPickedUp;
	
	//positional or offset pixel values
	protected int hoverOffset = 10;
	
	//slot pixel dimensions
	public int slotWidth;
	public int slotHeight;
	protected GUIStyle centeredStyle;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void initializeVariables(){
		isStashOn = false;
		isInventoryOn = false;
		targetItem = null;
		itemPickedUp = false;
	}
	
	protected abstract void drawItems();	
		
	protected void drawHoverItem(){
		if (itemPickedUp) {
			GUI.DrawTexture (new Rect(Input.mousePosition.x - hoverOffset, Screen.height - Input.mousePosition.y - hoverOffset, targetItem.width * slotWidth, targetItem.height * slotHeight), targetItem.getImage());
		}
	}
	
	bool availableSlots(int x, int y, Item item, Slot[,] slots, int storageWidth, int storageHeight){
		if(x + item.width > storageWidth){
			//Debug.Log("Out of X bounds");
			return false;
		}
		else if(y + item.height > storageHeight){
			//Debug.Log("Out of Y bounds");
			return false;
		}
		
		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				if(slots[sX,sY].occupied){
					//Debug.Log("breaks" + x + " , " + y);
					return false;
				}
			}
		}
		
		return true;
	}

	public void addItem(int x, int y, Item item, Slot[,] slots, List<Item> items){
		//Debug.Log("comes"+ x + " , " + y);
		item.x = x;
		item.y = y;
		items.Add(item);
		
		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				slots[sX,sY].occupied = true;
			}
		}
	}
	
	protected void detectGUIAction(){
		if(inInventoryBoundaries()){
			onInventoryItemHover();
		}
		else if(inEquippedBoundaries()){
			onEquippedItemHover();
		}
		else if(inStashBoundaries()){
			onStashItemHover();
		}
		if(targetItem != null){
			detectItemActions();
		}
		//detectMouseAction();
	}
	
	void onStashItemHover(){
		for(int i = 0; i < stashItems.Count; i++){
			if(!itemPickedUp && stashItems[i].position.Contains(mousePositionInInventory())){
				targetItem = stashItems[i];
				drawItemHoverTooltip(targetItem);
				//Debug.Log("on stash item hover " + targetItem);
				return;
			}
			else if(!itemPickedUp){
				targetItem = null;
			}
		}
	}
	
	void onInventoryItemHover(){
		for(int i = 0; i < inventoryItems.Count; i++){
			if(!itemPickedUp && inventoryItems[i].position.Contains(mousePositionInInventory())){
				targetItem = inventoryItems[i];
				drawItemHoverTooltip(targetItem);
				//Debug.Log("on inventory item hover " + targetItem);
				return;
			}
			else if(!itemPickedUp){
				targetItem = null;
			}
		}
	}
	
	void onEquippedItemHover(){
		foreach(Item item in equipItems){
			if(!itemPickedUp && item.position.Contains(mousePositionInInventory())){
				targetItem = item;
				Debug.Log("on equipped item hover " + targetItem);
				return;
			}
			else if(!itemPickedUp){
				targetItem = null;
			}
		}
	}
	
	void detectItemActions(){
		if(Input.GetMouseButtonDown(0) && !itemPickedUp){
			Debug.Log("I'm left clicking");
			if(inInventoryBoundaries()){
				pickUpInventoryItem();
			}
			else if(inEquippedBoundaries()){
				pickUpEquippedItem();
			}
			else if(inStashBoundaries()){
				pickUpStashItem();
			}
		}
		else if(itemPickedUp && (Input.GetMouseButtonUp(0) )){//|| Input.GetMouseButtonDown(0))){ // try to get click to select
			Debug.Log ("you let go of item");
			if(inInventoryBoundaries()){
				dropInventoryItem();
			}
			else if(inEquippedBoundaries()){
				dropEquipItem();
			}
			else if(inStashBoundaries()){
				dropStashItem();
			}
		}
		if(Input.GetMouseButtonDown(1)){ //Use item (consume/equip/etc..)
			Debug.Log ("Using item");
			if(inInventoryBoundaries()){
				useInventoryItem();
			}
			else if(inEquippedBoundaries()){
				//do nothing... or unequip?
			}
			else if(inStashBoundaries()){
				useStashItem();
			}
		}	
	}
	
	void pickUpStashItem(){
		removeItem(targetItem, stashSlots, stashItems);
		itemPickedUp = true;
	}
	
	void pickUpInventoryItem(){
		removeItem(targetItem, inventorySlots, inventoryItems);
		itemPickedUp = true;
	}
	
	void pickUpEquippedItem(){
		removeEquipItem(targetItem);
		itemPickedUp = true;
	}
	
	void dropInventoryItem(){
		for(int x = 0; x < inventoryStorageWidth ; x++){
			for(int y = 0; y < inventoryStorageHeight ; y++){
				Rect slot = new Rect(position.x + inventorySlots[x,y].position.x, position.y + inventorySlots[x,y].position.y, slotWidth, slotHeight);
				if(slot.Contains(mousePositionInInventory())){
					if(availableSlots(x, y, targetItem, inventorySlots, inventoryStorageWidth, inventoryStorageHeight)){
						addItem(x, y, targetItem, inventorySlots, inventoryItems);
						resetTargetItem();
						return;
					}
				}
			}
		}
		int newX;
		int newY;
		firstAvailableSlots(out newX, out newY, targetItem, inventorySlots, inventoryStorageWidth, inventoryStorageHeight);
		addItem(newX, newY, targetItem, inventorySlots, inventoryItems);
		resetTargetItem();
	}
	
	void dropStashItem(){
		for(int x = 0; x < stashStorageWidth ; x++){
			for(int y = 0; y < stashStorageHeight ; y++){
				Rect slot = new Rect(0 + stashSlots[x,y].position.x, position.y + stashSlots[x,y].position.y, slotWidth, slotHeight);
				if(slot.Contains(mousePositionInInventory())){
					if(availableSlots(x, y, targetItem, stashSlots, stashStorageWidth, stashStorageHeight)){
						addItem(x, y, targetItem, stashSlots, stashItems);
						resetTargetItem();
						return;
					}
				}
			}
		}
		int newX;
		int newY;
		firstAvailableSlots(out newX, out newY, targetItem, stashSlots, stashStorageWidth, stashStorageHeight);
		addItem(newX, newY, targetItem, stashSlots, stashItems);
		resetTargetItem();
	}
	
	void dropEquipItem(){
		foreach(EquipSlot slot in equipSlots){
			if(slot.position.Contains(mousePositionInInventory()) && validEquipSlot(slot, targetItem)){
				targetItem.useItem(player);
				resetTargetItem();
				return;
			}
		}
		int newX;
		int newY;
		firstAvailableSlots(out newX, out newY, targetItem, inventorySlots, inventoryStorageWidth, inventoryStorageHeight);
		addItem(newX, newY, targetItem, inventorySlots, inventoryItems);
		resetTargetItem();
	}
	
	void useInventoryItem(){
		if(isStashOn){
			removeItem(targetItem, inventorySlots, inventoryItems);
			int newX;
			int newY;
			firstAvailableSlots(out newX, out newY, targetItem, stashSlots, stashStorageWidth, stashStorageHeight);
			addItem(newX, newY, targetItem, stashSlots, stashItems);
			resetTargetItem();
		}
		else{
			targetItem.useItem(player);
			removeItem(targetItem, inventorySlots, inventoryItems);
			resetTargetItem();
		}
	}
	
	void useStashItem(){
		removeItem(targetItem, stashSlots, stashItems);
		int newX;
		int newY;
		firstAvailableSlots(out newX, out newY, targetItem, inventorySlots, inventoryStorageWidth, inventoryStorageHeight);
		addItem(newX, newY, targetItem, inventorySlots, inventoryItems);
		resetTargetItem();
	}
	
	void removeItem(Item item, Slot[,] slots, List<Item> items){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				slots[x,y].occupied = false;
			}
		}
		items.Remove(item);
	}
	
	void removeEquipItem(Item item){
		equipItems.Remove(item);
		foreach(EquipSlot slot in equipSlots){
			if(slot.item == item){
				slot.item = null;
				return; 
			}
		}
	}

	protected void drawItemHoverTooltip(Item item){
		GUI.Box (new Rect(item.position.x - width + slotWidth, item.position.y + slotHeight, width, height), item.getDescription() + priceMessage + item.getPrice(), centeredStyle);
	}
	
	protected void resetTargetItem(){
		itemPickedUp = false;
		targetItem = null;
	}
	
	public bool firstAvailableSlots(out int startX, out int startY, Item item, Slot[,] slots, int storageWidth, int storageHeight){
		bool validPostion;
		for(int x = 0; x < storageWidth ; x++){
			startX = x;
			for(int y = 0; y < storageHeight ; y++){
				startY = y;
				validPostion = true;
				for(int x2 = x; x2 < x + item.width; x2++){
					for(int y2 = y; y2 < y + item.height; y2++){
						if(x2 < storageWidth && y2 < storageHeight){
							if(slots[x2,y2].occupied){
								validPostion = false;
							}
						}
						else{
							validPostion = false;
						}
					}
				}
				if(validPostion){
					return true;
				}
			}
		}
		//exagerated out of bound values if couldn't find a slot (most likely full inventory)
		startX = 9999;
		startY = 9999;
		return false;
	}
	
	protected Vector2 mousePositionInInventory(){
		return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}
	
	public bool isItemPickedUp(){
		return itemPickedUp;
	}
	
	public bool inBoundaries(){
		//lock player movement within HUD bounds
		//Debug.Log("inv on: " + isInventoryOn);
		return (inInventoryWidthBoundaries() && inInventoryHeightBoundaries() && isInventoryOn);
	}
	
	public bool inStashBoundaries(){
		//lock player movement within HUD bounds
		//Debug.Log("inv on: " + isInventoryOn);
		return (inStashWidthBoundaries() && inStashHeightBoundaries() && isStashOn);
	}
	
	protected bool inInventoryWidthBoundaries(){
		return (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width);
	}
	
	protected bool inInventoryHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
	
	//this only works since inventory is same size as stash, try to refactor stash to function independently of inventory
	protected bool inStashWidthBoundaries(){
		return (Input.mousePosition.x > 0 && Input.mousePosition.x < 0 + position.width);
	}
	
	protected bool inStashHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
	
	protected bool inEquippedBoundaries(){
		return (inInventoryWidthBoundaries() && Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height/1.8f);
	}
	
	protected bool inInventoryBoundaries(){
		return (inInventoryWidthBoundaries() && Screen.height - Input.mousePosition.y > position.y + position.height/1.8f && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
	
	public static bool validEquipSlot(EquipSlot slot, Item item){
		return (item.GetType().IsSubclassOf(slot.type) || item.GetType() == slot.type);
		/*if(slot.type == typeof(Weapon)){
			return (slot.type == targetItem.GetType().BaseType);
		}
		return (slot.type == targetItem.GetType());*/
	}

	public int getStorageSizeWidth(){
		return inventoryStorageWidth;
	}

	public int getStorageSizeHeight(){
		return inventoryStorageHeight;
	}

	public Slot[,] getInventorySlots(){
		Slot[,] copy = inventorySlots;
		return copy;
	}

	public List<Item> getInventoryItems(){
		List<Item> copy = inventoryItems;
		return copy;
	}

	public List<Item> getEquipItems(){
		List<Item> copy = equipItems;
		return copy;
	}
	
	public void setPlayer(Player p){
		player = p;
	}
}
