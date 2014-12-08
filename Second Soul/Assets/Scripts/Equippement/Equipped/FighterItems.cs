using UnityEngine;
using System.Collections;

public class FighterItems : EquippedItems {

	public static Chest chest;
	public static Boots boots;
	public static Weapon weapon;
	
	public static EquipSlot chestSlot;
	public static EquipSlot bootsSlot;
	public static EquipSlot weaponSlot;
	
	//Offset and size of chest rect slot
	public int chestOffsetX;
	public int chestOffsetY;
	
	public int chestPixelHeight;
	public int chestPixelWidth;
	
	public Texture2D imageTemp;
	
	// Use this for initialization
	void Start () {
		chestSlot = new EquipSlot(new Rect(chestOffsetX + 867, chestOffsetY + 18, chestPixelHeight, chestPixelWidth), new Chest());
		bootsSlot = new EquipSlot(new Rect(), new Boots());
		weaponSlot = new EquipSlot(new Rect(), new Axe()); // set to dynamic weapon then have weapon check parent to make sure its a weapon
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (Inventory.isInventoryOn) {
			drawEquippedItems();
		}
	}
	
	void drawEquippedItems(){
		Debug.Log("I happen");
		GUI.DrawTexture(chestSlot.position, imageTemp);
	}
}
