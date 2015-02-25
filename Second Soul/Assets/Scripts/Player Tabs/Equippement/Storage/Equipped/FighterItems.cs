using UnityEngine;
using System.Collections.Generic;

public class FighterItems : EquippedItems {
	
	public static EquipSlot chestSlot;
	public static EquipSlot bootsSlot;
	public static EquipSlot weaponSlot;

	//Offset and size of chest rect slot
	public int chestOffsetX;
	public int chestOffsetY;
	
	public int chestPixelHeight;
	public int chestPixelWidth;
	
	//Offset and size of chest rect slot
	public int weaponOffsetX;
	public int weaponOffsetY;
	
	public int weaponPixelHeight;
	public int weaponPixelWidth;
	
	public int guiDepth = 0;
	
	// Use this for initialization
	public void Start () {
		setSlots();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void setSlots(){
		// FIXME: MAYBE DEPEND ON THE ENABLED/ACTIVE PLAYER IF SO ADD if(playeEnabled)
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		chestSlot = new EquipSlot(new Rect(chestOffsetX + position.x, chestOffsetY + position.y, chestPixelHeight, chestPixelWidth), typeof(Chest));
		bootsSlot = new EquipSlot(new Rect(), typeof(Boots));
		weaponSlot = new EquipSlot(new Rect(weaponOffsetX + position.x, weaponOffsetY + position.y, weaponPixelHeight, weaponPixelWidth), typeof(Weapon));
		equipSlots.Add(chestSlot);
		equipSlots.Add(bootsSlot);
		equipSlots.Add(weaponSlot); // set to dynamic weapon then have weapon check parent to make sure its a weapon
	}

	void OnGUI(){
		GUI.depth = guiDepth;
		if (isInventoryOn) {
			drawEquippedItems();
			//detectGUIAction();
			//Debug.Log(chestSlot.position);
		}
	}

	void drawEquippedItems(){
		//Debug.Log("I happen");
		foreach(Item item in equipItems){
			GUI.DrawTexture(item.position, item.getImage());
		}
	}
	
	
	
	/*void detectGUIAction(){
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
	
	}*/
}
