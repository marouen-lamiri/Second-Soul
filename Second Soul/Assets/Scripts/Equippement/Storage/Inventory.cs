using UnityEngine;
using System.Collections.Generic;

public class Inventory : Storage 
{
	public Texture2D image;
	//public Rect position;
	public Texture2D white;
	public Texture2D red;
	public Texture2D blue;

	// in number of slots
	/*int storageWidth = 6;
	int storageHeight = 4;
	*/
	public int slotsOffsetX;
	public int slotsOffsetY;

	/*private Item temp;
	private Vector2 selected;
	private Vector2 secondSelected;*/ //out dated vars

	// Use this for initialization
	void Start () {
		initializeVariables(); // in parent Storage class
		setSlots ();
		addSampleItems ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("i")) {
			shownInventory();
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
		addInventoryItem(2, 0, new HealthPotion());
		addInventoryItem(3, 0, new ManaPotion());
		addInventoryItem(2, 1, new Ring());
		addInventoryItem(4, 0, new Axe());
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


	void drawInventory(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}

	/*void drawSlots(){
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				inventorySlots[x,y].draw(position.x, position.y);
			}
		}
	}*/

	void drawItems(){
		for(int i = 0; i < inventoryItems.Count; i++){
			inventoryItems[i].position = new Rect(6 + slotsOffsetX + position.x + inventoryItems[i].x * slotWidth, 6 + slotsOffsetY + position.y + inventoryItems[i].y * slotHeight,inventoryItems[i].width * slotWidth - 12,inventoryItems[i].height * slotHeight - 12);
			GUI.DrawTexture(inventoryItems[i].position, inventoryItems[i].getImage());
		}
	}

	
	/*bool availableSlots(int x, int y, Item item){
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
				if(inventorySlots[sX,sY].occupied){
					//Debug.Log("breaks" + x + " , " + y);
					return false;
				}
			}
		}
		
		return true;
	}

	void detectGUIAction(){
		if(inWidthBoundaries() && inHeightBoundaries()){
			onItemHover();
			if(targetItem != null){
				detectItemActions();
			}
			//detectMouseAction();
			NavClickToMove.busy = true;
		}
		else{
			NavClickToMove.busy = false;
		}
		
	}
	
	void onItemHover(){
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
	
	void detectItemActions(){
		if(Input.GetMouseButtonDown(0) && !itemPickedUp){
			Debug.Log("I'm left clicking");
			pickUpItem();	
		}
		else if(itemPickedUp && (Input.GetMouseButtonUp(0) )){//|| Input.GetMouseButtonDown(0))){ // try to get click to select
			Debug.Log ("you let go of item");
			dropItem();
		}
		if(Input.GetMouseButtonDown(1)){ //Use item (consume/equip/etc..)
			Debug.Log ("Using item");
			useItem();
		}	
	}
	
	void pickUpItem(){
		removeItem(targetItem);
		itemPickedUp = true;
	}
	
	void dropItem(){
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				Rect slot = new Rect(position.x + inventorySlots[x,y].position.x, position.y + inventorySlots[x,y].position.y, slotWidth, slotHeight);
				if(slot.Contains(mousePositionInInventory())){
					if(availableSlots(x, y, targetItem)){
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
	
	void useItem(){
		targetItem.useItem();
		removeItem(targetItem);
		resetTargetItem();
	}
	
	void removeItem(Item item){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				inventorySlots[x,y].occupied = false;
			}
		}
		inventoryItems.Remove(item);
	}
	
	protected bool inWidthBoundaries(){
		return (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width);
	}
	
	protected bool inHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}*/
	
}
