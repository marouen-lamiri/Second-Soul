using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour {
	public Rect position;

	//different item collections
	protected static List<Item> inventoryItems = new List<Item>();
	public static List<Item> equipItems = new List<Item>();
	
	//slots for different storage types
	protected static Slot[,] inventorySlots;
	public static List<EquipSlot> equipSlots = new List<EquipSlot>();
	
	// in number of slots
	protected int inventoryStorageWidth = 6;
	protected int inventoryStorageHeight = 4;

	protected static bool isInventoryOn;
	
	//used for targeting and picking up (hovering) item
	protected Item targetItem;
	protected bool itemPickedUp;
	
	//positional or offset pixel values
	protected int hoverOffset = 10;
	
	//slot pixel dimensions
	public int slotWidth;
	public int slotHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void initializeVariables(){
		isInventoryOn = false;
		targetItem = null;
		itemPickedUp = false;
	}	
	protected void drawHoverItem(){
		if (itemPickedUp) {
			GUI.DrawTexture (new Rect(Input.mousePosition.x - hoverOffset, Screen.height - Input.mousePosition.y - hoverOffset, targetItem.width * slotWidth, targetItem.height * slotHeight), targetItem.getImage());
		}
	}
	
	bool availableInventorySlots(int x, int y, Item item){
		if(x + item.width > inventoryStorageWidth){
			//Debug.Log("Out of X bounds");
			return false;
		}
		else if(y + item.height > inventoryStorageHeight){
			//Debug.Log("Out of Y bounds");
			return false;
		}
		
		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				if(inventorySlots[sX,sY].occupied){
					//Debug.Log("breaks" + x + " , " + y);
					return false;
				}
			}
		}
		
		return true;
	}

	public void addInventoryItem(int x, int y, Item item){
		//Debug.Log("comes"+ x + " , " + y);
		item.x = x;
		item.y = y;
		inventoryItems.Add(item);
		
		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				inventorySlots[sX,sY].occupied = true;
			}
		}
	}
	
	protected void detectGUIAction(){
		if(inWidthBoundaries() && inHeightBoundaries()){
			if(inInventoryBoundaries()){
				onInventoryItemHover();
			}
			else if(inEquippedBoundaries()){
				onEquippedItemHover();
			}
			if(targetItem != null){
				detectItemActions();
			}
			//detectMouseAction();
			//remove appropriate when navigaiton finalized
			NavClickToMove.busy = true;
			ClickToMove.busy = true;
		}
		else if(!itemPickedUp){
			//remove appropriate when navigaiton finalized
			NavClickToMove.busy = false;
			ClickToMove.busy = false;
		}
		
	}
	
	void onInventoryItemHover(){
		for(int i = 0; i < inventoryItems.Count; i++){
			if(!itemPickedUp && inventoryItems[i].position.Contains(mousePositionInInventory())){
				targetItem = inventoryItems[i];
				Debug.Log("on item hover " + targetItem);
				Debug.Log(targetItem.GetType());
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
				Debug.Log("on item hover " + targetItem);
				Debug.Log(targetItem.GetType());
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
		}
		else if(itemPickedUp && (Input.GetMouseButtonUp(0) )){//|| Input.GetMouseButtonDown(0))){ // try to get click to select
			Debug.Log ("you let go of item");
			if(inInventoryBoundaries()){
				dropInventoryItem();
			}
			else if(inEquippedBoundaries()){
				dropEquipItem();
			}
		}
		if(Input.GetMouseButtonDown(1)){ //Use item (consume/equip/etc..)
			Debug.Log ("Using item");
			if(inInventoryBoundaries()){
				useItem();
			}
			else if(inEquippedBoundaries()){
				//do nithing... or unequip?
			}
		}	
	}
	
	void pickUpInventoryItem(){
		removeInventoryItem(targetItem);
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
					if(availableInventorySlots(x, y, targetItem)){
						addInventoryItem(x, y, targetItem);
						resetTargetItem();
						return;
					}
				}
			}
		}
		addInventoryItem(targetItem.x, targetItem.y, targetItem);
		resetTargetItem();
	}
	
	void dropEquipItem(){
		foreach(EquipSlot slot in equipSlots){
			if(slot.position.Contains(mousePositionInInventory()) && validEquipSlot(slot, targetItem)){
				targetItem.useItem();
				resetTargetItem();
				return;
			}
		}
		int newX;
		int newY;
		firstAvailableInventorySlots(out newX, out newY, targetItem);
		addInventoryItem(newX, newY, targetItem);
		resetTargetItem();
	}
	
	void useItem(){
		targetItem.useItem();
		removeInventoryItem(targetItem);
		resetTargetItem();
	}
	
	void removeInventoryItem(Item item){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				inventorySlots[x,y].occupied = false;
			}
		}
		inventoryItems.Remove(item);
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
	
	protected void resetTargetItem(){
		itemPickedUp = false;
		targetItem = null;
		//remove appropriate when navigaiton finalized
		NavClickToMove.busy = false;
		ClickToMove.busy = false;
	}
	
 	public bool firstAvailableInventorySlots( out int startX, out int startY, Item item ){
		bool validPostion;
		for(int x = 0; x < inventoryStorageWidth ; x++){
			startX = x;
			for(int y = 0; y < inventoryStorageHeight ; y++){
				startY = y;
				validPostion = true;
				for(int x2 = x; x2 < x + item.width; x2++){
					for(int y2 = y; y2 < y + item.height; y2++){
						if(x2 < inventoryStorageWidth && y2 < inventoryStorageHeight){
							if(inventorySlots[x2,y2].occupied){
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
	
	protected bool inWidthBoundaries(){
		return (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width);
	}
	
	protected bool inHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
	
	protected bool inEquippedBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height/1.8f);
	}
	
	protected bool inInventoryBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y + position.height/1.8f && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
	
	protected bool validEquipSlot(EquipSlot slot, Item item){
		if(slot.type == "Weapon"){
			return (slot.type == targetItem.GetType().BaseType.ToString());
		}
		return (slot.type == targetItem.GetType().ToString());
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
}
