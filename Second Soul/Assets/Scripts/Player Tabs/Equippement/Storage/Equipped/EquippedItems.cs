using UnityEngine;
using System.Collections.Generic;

public abstract class EquippedItems : Storage {

	//public static List<Item> equipItems = new List<Item>();
	
	//FIXME: Static Offset for inventory position (should be like Inventory where new GUI is relative to first)
	public int inventoryOffsetX = 867;
	public int inventoryOffsetY = 18;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected override abstract void drawItems();
	
}
