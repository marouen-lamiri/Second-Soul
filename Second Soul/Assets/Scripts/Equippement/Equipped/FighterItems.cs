using UnityEngine;
using System.Collections.Generic;

public class FighterItems : EquippedItems {

	public static Chest chest;
	public static Boots boots;
	public static Weapon weapon;
	
	public static List<EquipSlot> equipSlots = new List<EquipSlot>();
	
	public static EquipSlot chestSlot;
	public static EquipSlot bootsSlot;
	public static EquipSlot weaponSlot;
	
	//FIXME: Static Offset for inventory position (should be like Inventory where new GUI is relative to first)
	public int inventoryOffsetX = 867;
	public int inventoryOffsetY = 18;
	
	
	//Offset and size of chest rect slot
	public int chestOffsetX;
	public int chestOffsetY;
	
	public int chestPixelHeight;
	public int chestPixelWidth;
	
	// Use this for initialization
	void Start () {
		setSlots();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void setSlots(){
		chestSlot = new EquipSlot(new Rect(chestOffsetX + inventoryOffsetX, chestOffsetY + inventoryOffsetY, chestPixelHeight, chestPixelWidth), new Chest());
		bootsSlot = new EquipSlot(new Rect(), new Boots());
		weaponSlot = new EquipSlot(new Rect(), new Axe());
		equipSlots.Add(chestSlot);
		equipSlots.Add(bootsSlot);
		equipSlots.Add(weaponSlot); // set to dynamic weapon then have weapon check parent to make sure its a weapon
	}

	void OnGUI(){
		if (isInventoryOn) {
			drawEquippedItems();
			detectGUIAction();
			//Debug.Log(chestSlot.position);
		}
	}
	
	void drawEquippedItems(){
		//Debug.Log("I happen");
		foreach(Item item in equipItems){
			GUI.DrawTexture(item.position, item.getImage());
		}
	}
	
	void onItemHover(){
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
	
	void detectGUIAction(){
		if(true){//inWidthBoundaries() && inHeightBoundaries()){ // similar to code from inventory add equipped boundaries, ideally make boundary from storage level then check deeper for specific actions
			onItemHover();
			if(targetItem != null){
				detectItemActions();
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
		foreach(EquipSlot slot in equipSlots){
			if(slot.position.Contains(mousePositionInInventory())){
				targetItem.useItem();
				resetTargetItem();
				return;
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
	
	void removeItem(Item targetItem){
	
	}
}
