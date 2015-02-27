using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipSlot : Slot{

	public Item item;
	//public string type; //type of item it accepts
	public System.Type type;
	
	public EquipSlot(Rect position) : base(position)
	{
	
	}
	
	public EquipSlot(Rect position, Item item ) : base(position)
	{
		this.item = item;
	}
	
	public EquipSlot(Rect position, System.Type type ) : base(position)
	{
		this.type = type;
	}
}
