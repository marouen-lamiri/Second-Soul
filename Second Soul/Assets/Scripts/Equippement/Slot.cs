using UnityEngine;
using System.Collections;

[System.Serializable]

public class Slot 
{
	public Item item;
	public bool occupied;
	public Rect position;
	
	public Slot(Rect position)
	{
		this.position = position;
	}
	
	public void draw(float frameX, float frameY)
	{
		if(item!=null){
			GUI.DrawTexture(new Rect(frameX + position.x, frameY + position.y, position.width, position.height), item.getImage());
		}
	}
}
