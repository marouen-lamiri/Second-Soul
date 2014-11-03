using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Texture2D Image;
	public Rect Position;

	public List<Items> items;
	int slotWidthSize = 6;
	int slotHeightSize = 4;
	public Slots[,] slots;
	
	public int slotX;
	public int slotY;
	public int width = 32;
	public int height = 32;

	private bool test;

	// Use this for initialization
	void Start () {
		setSlots ();
		test = false;
		items = new List<Items> (); 

	}

	void testMethod(){
		test = true;
		addItems (0,0,ItemList.armor[0]);
		addItems (1,1,ItemList.armor[0]);
	}

	void setSlots() {
		slots = new Slots[6,4];
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++){
				slots[x,y] = new Slots (new Rect(slotX + width * x, slotY + height * y, width, height));
				slotY += 5;
				if(slotHeightSize - y == 1)
					slotY -= 5 * slotHeightSize;
			}
			slotX += 5;
			if(slotWidthSize - x == 1)
				slotX -= 5 * slotWidthSize;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!test) {
			testMethod();
		}
	}

	void OnGUI() {
		drawInventory ();
		drawSlots ();
	}

	void drawSlots () {
		for (int x = 0; x < slotWidthSize; x++) {
			for (int y = 0; y < slotHeightSize; y++){
				slots[x,y].Draw(Position.x, Position.y);
			}
		}
	}

	void drawItems(){
		for (int count = 0; count < items.Count; count++) {
			GUI.DrawTexture(new Rect(slotX + Position.x +items[count].X * width,slotY + Position.y + items[count].Y * height,items[count].Width * width,items[count].Height * height), items[count].Image);
		}
	}

	void addItems(int x, int y, Items item){

		for (int counterX = 0; counterX < item.Width; counterX++) {
			for(int counterY = 0; counterY < item.Height; counterY++){
				if(slots[x,y].Occupied){
					Debug.Log ("Breaks " + x + ", " + y);
					return;
				}
			}
		}
		Debug.Log ("Comes " + x + ", " + y);
		items.Add (item);

		for (int counterX = x; counterX < item.Width + x; counterX++) {
			for(int counterY = y; counterY < item.Height + y; counterY++){
				slots[counterX, counterY].Occupied = true;
			}
		}
	}

	void drawInventory(){
		Position.x = Screen.width - Position.x;
		Position.y = Screen.height - Position.y - Screen.height * 0.1f;
		GUI.DrawTexture (Position, Image);
	}
}
