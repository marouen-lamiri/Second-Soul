using UnityEngine;
using System.Collections;

public class SorcererItems : EquippedItems {

	public static EquipSlot amuletSlot;
	public static EquipSlot ringSlot;
	
	
	//Offset and size of chest rect slot
	public int ringOffsetX;
	public int ringOffsetY;
	
	public int ringPixelHeight;
	public int ringPixelWidth;
	
	// Use this for initialization
	void Start () {
		amuletSlot = new EquipSlot(new Rect(), "Amulet");
		ringSlot = new EquipSlot(new Rect(ringOffsetX + inventoryOffsetX, ringOffsetY + inventoryOffsetY, ringPixelHeight, ringPixelWidth), "Ring");
		equipSlots.Add(amuletSlot);
		equipSlots.Add(ringSlot);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
