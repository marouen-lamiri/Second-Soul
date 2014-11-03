using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item{
	public Texture2D image;	
	public int x;
	public int y;
	public int width;
	public int height;
	public Character player;

	public abstract void performAction();

}
