using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public Texture2D image;
	public Rect position;

	public List<Item> items = new List<Item>();
	int slotWidthSize = 6;
	int slotHeightSize = 4;
	public Slot[,] slots;

	public int slotX;
	public int slotY;
	public int width = 40;
	public int height = 40;

	private Item temp;
	private Vector2 selected;
	private Vector2 secondSelected;
	private bool isInventoryOn = false;

	private bool test;
	// Use this for initialization
	void Start () {
		setSlots ();
		test = false;
	}

	void setSlots(){
		slots = new Slot[slotWidthSize,slotHeightSize];
		for(int x = 0; x < slotWidthSize ; x++){
			for(int y = 0; y < slotHeightSize ; y++){
				slots[x,y] = new Slot(new Rect(slotX + width*x, slotY + height*y, width, height));
			}
		}
	}

	void testMethod(){
		addItem(0,0,Items.getArmor(0));
		addItem(2,0,Items.getHealthPotion(0));
		addItem(3,0,Items.getManaPotion(0));
		test = true;
	}

	void shownInventory(){
		if (isInventoryOn) {
			isInventoryOn = false;
		}
		else {
			isInventoryOn = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if(!test){
			testMethod ();
		}
		if (Input.GetKeyDown ("i")) {
			shownInventory();
		}
	}

	void OnGUI(){
		if (isInventoryOn) {
			drawInventory ();
			drawSlots ();
			drawItems ();
			detectGUIAction ();
			drawTempItem();
		}
	}

	void drawSlots(){
		for(int x = 0; x < slotWidthSize ; x++){
			for(int y = 0; y < slotHeightSize ; y++){
				slots[x,y].draw(position.x, position.y);
			}
		}
	}

	void drawItems(){
		for(int count = 0; count < items.Count; count++){
			GUI.DrawTexture(new Rect(8 + slotX + position.x + items[count].x * width, 8 + slotY + position.y + items[count].y * height,items[count].width * width - 16,items[count].height * height - 16), items[count].image);
		}
	}

	void drawTempItem(){
		if (temp != null) {
			GUI.DrawTexture (new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, temp.width*width, temp.height*height), temp.image);
		}
	}

	bool addItem(int x, int y, Item item){	
		for(int sX = 0; sX < item.width ; sX++){
			for(int sY = 0; sY < item.height ; sY++){
				if(slots[x,y].occupied){
					//Debug.Log("breaks" + x + " , " + y);
					return false;
				}
			}
		}

		if(x + item.width > slotWidthSize){
			//Debug.Log("Out of X bounds");
			return false;
		}
		else if(y + item.height > slotHeightSize){
			//Debug.Log("Out of Y bounds");
			return false;
		}

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

	void removeItem(Item item){
		for(int x = item.x; x < item.x + item.width; x++){
			for(int y = item.y; y < item.y + item.height; y++) {
				slots[x,y].occupied = false;
			}
		}
		items.Remove(item);
	}

	void detectGUIAction(){
		if(Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width){
			if(Screen.height - Input.mousePosition.y > position.y && Screen.height - Input.mousePosition.y < position.y + position.height){
				detectMouseAction();
				ClickToMove.busy = true;
				return;
			}
		}
		ClickToMove.busy = false;
	}
	
	void detectMouseAction(){
		for(int x = 0; x < slotWidthSize ; x++){
			for(int y = 0; y < slotHeightSize ; y++){
				Rect slot = new Rect(position.x + slots[x,y].position.x, position.y + slots[x,y].position.y, width, height);
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
								if(addItem((int)secondSelected.x, (int)secondSelected.y, temp)){

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
						temp.performAction();
						removeItem(temp);
						deleteItem ();
					}
					return;
				}
			}
		}
	}

	void deleteItem(){
		temp = null;
	}

	void drawInventory(){
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}
}
