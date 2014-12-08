using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipSlot {

	public Item item;
	public bool occupied;
	public Rect position;
	
	public EquipSlot(Rect position, Item item )
	{
		this.position = position;
		this.item = item;
	}
	
	//not currently used
	public void draw(float frameX, float frameY)
	{
		if(item!=null){
			GUI.DrawTexture(new Rect(frameX + position.x, frameY + position.y, position.width, position.height), item.image);
		}
	}
}
