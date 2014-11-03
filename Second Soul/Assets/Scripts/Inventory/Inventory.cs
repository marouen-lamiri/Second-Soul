using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Texture2D Image;
	public Rect Position;

	public List<Items> items = new List<Items>();
	int slotWidthSize = 6;
	int slotHeightSize = 4;
	public Slots[,] slots;
	
	public int slotX;
	public int slotY;
	public int width = 32;
	public int height = 32;

	private Items temp;
	private bool test;
	private bool showInventory = false;
	private Vector2 selected;
	private Vector2 secondSelect;

	// Use this for initialization
	void Start () {
		setSlots ();
		test = false;
	}

	void testMethod(){
		addItems (0,0,ItemList.getArmor(0));
		addItems (2,2,ItemList.getArmor(0));
		test = true;
	}

	//Position slots in the right position
	void setSlots() {
		slots = new Slots[6,4];
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++){
				slots[x,y] = new Slots (new Rect(slotX + width * x, slotY + height * y, width, height));
//				slotY += 5;
//				if(slotHeightSize - y == 1)
//					slotY -= 5 * slotHeightSize;
			}
//			slotX += 5;
//			if(slotWidthSize - x == 1)
//				slotX -= 5 * slotWidthSize;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!test) {
			testMethod();
		}
	}

	//Determines to display Inventory or not
//	void changeShowInventory () {
//		if (showInventory) {
//			showInventory = false;
//		}
//		else {
//			showInventory = true;
//		}
//	}

	//Draw on Screen the inventory
	void OnGUI() {
		drawInventory ();
		drawSlots ();
		drawItems ();
		detectGuiAction ();
		detectMouseAction ();
	}

	//Create slots for the inventory
	void drawSlots () {
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++){
				slots[x,y].Draw(Position.x, Position.y);
			}
		}
	}

	//Draw items in slots for inventory
	void drawItems(){
		for (int count = 0; count < items.Count; count++) {
			GUI.DrawTexture(new Rect(2 + slotX + Position.x + items[count].X * width, 2 + slotY + Position.y + items[count].Y * height,items[count].Width * width - 4,
			                         items[count].Height * height - 4), items[count].Image);
		}
	}

	//Add item in inventory by checking if not item is present in that location
	void addItems(int x, int y, Items item){

		for (int counterX = 0; counterX < item.Width; counterX++) {
			for(int counterY = 0; counterY < item.Height; counterY++){
				if(slots[x,y].Occupied){
					//Debug.Log ("Breaks " + x + ", " + y);
					return;
				}
			}
		}
		//Debug.Log ("Comes " + x + ", " + y);
		if (x + item.Width > slotWidthSize) {
			//Debug.Log ("Out of X bounds");
			return;
		}
		else if (y + item.Height > slotHeightSize) {
			//Debug.Log ("Out of Y bounds");
			return;
		}
		item.X = x;
		item.Y = y;
		items.Add (item);

		for (int counterX = x; counterX < item.Width + x; counterX++) {
			for(int counterY = y; counterY < item.Height + y; counterY++){
				slots[counterX, counterY].Occupied = true;
			}
		}
	}

	//Draw Inventory 
	void drawInventory(){
		Position.x = Screen.width - Position.x;
		Position.y = Screen.height - Position.y - Screen.height * 0.1f;
		GUI.DrawTexture (Position, Image);
	}

	void detectGuiAction(){
		//Debug.Log (Input.mousePosition.y);
		if(Input.mousePosition.x > Position.x && Input.mousePosition.x < Position.x + Position.width){
			if(Screen.height - Input.mousePosition.y > Position.y && Input.mousePosition.y < Position.y + Position.height){
				ClickToMove.busy = true;
				return;
			}
		}
		ClickToMove.busy = false;
	}

	void detectMouseAction(){
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++){
				Rect slot = new Rect(slots[x,y].Position.x + Position.x, slots[x,y].Position.y + Position.y, width, height);
				if(slot.Contains(new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
					if(Event.current.isMouse && Input.GetMouseButtonDown(0)){
						selected.x = x;
						selected.y = y;
						if(slots[x, y].Item != null) {
							for(int index = 0; index < items.Count; index++){
								for(int countX = 0; countX < items.Count; countX++){
									for(int countY = 0; countY < items.Count; countY++){
										if(countX == x && countY == y){
											temp = items[index];
										}
									}
								}
							}
						}
					}
					else if(Event.current.isMouse && Input.GetMouseButtonUp(0)){
						secondSelect.x = x;
						secondSelect.y = y;
						if(secondSelect.x != selected.x || secondSelect.y != selected.y) {
							addItems ((int)secondSelect.x, (int)secondSelect.y, temp);
						}
					}
					//Debug.Log(selected + "    /   " + secondSelect);
					return;
				}
			}
		}
	}
}
