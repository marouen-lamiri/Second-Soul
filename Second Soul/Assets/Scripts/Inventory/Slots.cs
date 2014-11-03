using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slots{

	public Items Item;
	public bool Occupied;
	public Rect Position;

	public Slots (Rect Position) {
		this.Position = Position;
	}

	public void Draw (float frameX, float frameY) {
		if (Item != null) {
			GUI.DrawTexture (new Rect (frameX + Position.x, frameY + Position.y, Position.width, Position.height), Item.Image);
		}
	}
}
