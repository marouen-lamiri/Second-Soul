using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public Texture2D image;
	public Rect position;

	public List<Item> items = new List<Item>();
	
	// in number of slots
	int storageWidth = 6;
	int storageHeight = 4;
	
	public Slot[,] slots;

	public int slotsOffsetX;
	public int slotsOffsetY;
	
	//slot pixel dimensions
	public int slotWidth = 40;
	public int slotHeight = 40;
	
	private int hoverOffset = 10;

	/*private Item temp;
	private Vector2 selected;
	private Vector2 secondSelected;*/ //out dated vars
	
	public static bool isInventoryOn = false;
	
	//new development variables
	private Item targetItem;
	private bool itemPickedUp;

	// Use this for initialization
	void Start () {
		setSlots ();
		testMethod ();
		targetItem = null;
		itemPickedUp = false;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("i")) {
			shownInventory();
		}
	}
	
	void setSlots(){
		slots = new Slot[storageWidth,storageHeight];
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				slots[x,y] = new Slot(new Rect(slotsOffsetX + slotWidth*x, slotsOffsetY + slotHeight*y, slotWidth, slotHeight));
			}
		}
	}
	
	void testMethod(){
		addItem(0,0,Items.getChest(0));
		addItem(2,0,Items.getHealthPotion(0));
		addItem(3,0,Items.getManaPotion(0));
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
			drawSlots ();
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

	void drawSlots(){
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				slots[x,y].draw(position.x, position.y);
			}
		}
	}

	void drawItems(){
		for(int i = 0; i < items.Count; i++){
			items[i].position = new Rect(8 + slotsOffsetX + position.x + items[i].x * slotWidth, 8 + slotsOffsetY + position.y + items[i].y * slotHeight,items[i].width * slotWidth - 16,items[i].height * slotHeight - 16);
			GUI.DrawTexture(items[i].position, items[i].image);
		}
	}

	void drawHoverItem(){
		if (itemPickedUp) {
			GUI.DrawTexture (new Rect(Input.mousePosition.x - hoverOffset, Screen.height - Input.mousePosition.y - hoverOffset, targetItem.width * slotWidth, targetItem.height * slotHeight), targetItem.image);
		}
	}
	
	bool availableSlots(int x, int y, Item item){
		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				if(slots[sX,sY].occupied){
					//Debug.Log("breaks" + x + " , " + y);
					return false;
				}
			}
		}
		
		if(x + item.width > storageWidth){
			//Debug.Log("Out of X bounds");
			return false;
		}
		else if(y + item.height > storageHeight){
			//Debug.Log("Out of Y bounds");
			return false;
		}
		return true;
	}

	void addItem(int x, int y, Item item){
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
		for(int i = 0; i < items.Count; i++){
			if(!itemPickedUp && items[i].position.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
				targetItem = items[i];
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
				Rect slot = new Rect(position.x + slots[x,y].position.x, position.y + slots[x,y].position.y, slotWidth, slotHeight);
				if(slot.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
					if(availableSlots(x, y, targetItem)){
						addItem(x, y, targetItem);
						resetTargetItem();
						return;
					}
				}
			}
		}
		addItem(targetItem.x, targetItem.y, targetItem);
		resetTargetItem();
	}
	
	void resetTargetItem(){
		itemPickedUp = false;
		targetItem = null;
	}
	
	void useItem(){
		targetItem.useItem();
		removeItem(targetItem);
		resetTargetItem();
	}
	
	void removeItem(Item item){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				slots[x,y].occupied = false;
			}
		}
		items.Remove(item);
	}

	private bool inWidthBoundaries(){
		return (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width);
	}
	
	private bool inHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
}
