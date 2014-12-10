using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour {

	protected static List<Item> inventoryItems = new List<Item>();
	
	protected static Slot[,] inventorySlots;

	protected static bool isInventoryOn;
	
	//used for targeting and picking up (hovering) item
	protected Item targetItem;
	protected bool itemPickedUp;
	
	//positional or offset pixel values
	protected int hoverOffset = 10;
	
	//slot pixel dimensions
	public int slotWidth = 40;
	public int slotHeight = 40;

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

	public static void addInventoryItem(int x, int y, Item item){
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
	
	protected void resetTargetItem(){
		itemPickedUp = false;
		targetItem = null;
	}
	
	protected Vector2 mousePositionInInventory(){
		return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}
	
}
