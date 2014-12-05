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

	public int slotX;
	public int slotY;
	
	//slot pixel dimensions
	public int slotWidth = 40;
	public int slotHeight = 40;

	private Item temp;
	private Vector2 selected;
	private Vector2 secondSelected;
	private bool isInventoryOn = false;
	
	//new development variables
	private Item targetItem;
	private bool itemPickedUp;

	// Use this for initialization
	void Start () {
		setSlots ();
		testMethod ();
		itemPickedUp = false;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("i")) {
			shownInventory();
		}
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
			drawTempItem();
			//Debug.Log("id: " + GUIUtility.hotControl);
			Debug.Log (itemPickedUp);
		}
	}

	
	void drawInventory(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}

	void setSlots(){
		slots = new Slot[storageWidth,storageHeight];
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				slots[x,y] = new Slot(new Rect(slotX + slotWidth*x, slotY + slotHeight*y, slotWidth, slotHeight));
			}
		}
	}
	
	void drawSlots(){
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				slots[x,y].draw(position.x, position.y);
			}
		}
	}

	void testMethod(){
		addItem(0,0,Items.getArmor(0));
		addItem(2,0,Items.getHealthPotion(0));
		addItem(3,0,Items.getManaPotion(0));
	}




	void drawItems(){
		for(int i = 0; i < items.Count; i++){
			items[i].position = new Rect(8 + slotX + position.x + items[i].x * slotWidth, 8 + slotY + position.y + items[i].y * slotHeight,items[i].width * slotWidth - 16,items[i].height * slotHeight - 16);
			GUI.DrawTexture(items[i].position, items[i].image);
		}
	}
	
	void onItemHover(){
		for(int i = 0; i < items.Count; i++){
			if(!itemPickedUp && items[i].position.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
				targetItem = items[i];
				Debug.Log("on item hover");
			}
			else if(!itemPickedUp){
				targetItem = null;
			}
		}
	}

	void drawTempItem(){
		if (itemPickedUp) {
			GUI.DrawTexture (new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, temp.width * slotWidth, temp.height * slotHeight), targetItem.image);
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

	bool addItem(int x, int y, Item item){
		//Debug.Log("comes"+ x + " , " + y);
		item.x = x;
		item.y = y;
		items.Add(item);

		for(int sX = x; sX < item.width + x; sX++){
			for(int sY = y; sY < item.height + y ; sY++){
				slots[sX,sY].occupied = true;
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
	
	void detectItemActions(){
		//Use item (consume/equip/etc..)
		if(Input.GetMouseButtonDown(1)){
			Debug.Log ("Using item");
			targetItem.useItem();
			removeItem(targetItem);
			deleteItem ();
		}
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
			Debug.Log("I'm left clicking");
			itemPickedUp = true;
			
		}
		
	}
	
	void detectMouseAction(){
		for(int x = 0; x < storageWidth ; x++){
			for(int y = 0; y < storageHeight ; y++){
				Rect slot = new Rect(position.x + slots[x,y].position.x, position.y + slots[x,y].position.y, slotWidth, slotHeight);
				if(slot.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
					if(Event.current.isMouse && Input.GetMouseButtonDown(0)){
						selected.x = x;
						selected.y = y;
						for(int index = 0; index < items.Count; index++){
							for(int countX = items[index].x; countX < items[index].x + items[index].width; countX++){
								for(int countY = items[index].y; countY < items[index].y + items[index].height; countY++){
										if(countX == x && countY == y){
											temp = items[index];
											removeItem(temp);
											return;
										}
									}
								}
							}
					}
					else if(Event.current.isMouse && Input.GetMouseButtonUp(0)){
						secondSelected.x = x;
						secondSelected.y = y;
						if(secondSelected.x != selected.x || secondSelected.y != selected.y){
							if(temp != null) {
								if(availableSlots((int)secondSelected.x, (int)secondSelected.y, temp)){
									addItem((int)secondSelected.x, (int)secondSelected.y, temp);
								}
								else{
									addItem(temp.x, temp.y, temp);
								}
								temp = null;
							}
						}
						else {
							addItem(temp.x, temp.y, temp);
							temp = null;
						}
					}
					else if(Event.current.isMouse && Input.GetKeyDown(KeyCode.Space)){
						Debug.Log ("Hello");
						temp.useItem();
						removeItem(temp);
						deleteItem ();
					}
					return;
				}
			}
		}
	}
	
	void removeItem(Item item){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				slots[x,y].occupied = false;
			}
		}
		items.Remove(item);
	}

	void deleteItem(){
		temp = null;
		targetItem = null;
	}
	
	private bool inWidthBoundaries(){
		return (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width);
	}
	
	private bool inHeightBoundaries(){
		return (Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height);
	}
}
