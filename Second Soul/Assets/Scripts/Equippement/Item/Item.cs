using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item{
	public Rect position;
	public Texture2D image;	
	public int x;
	public int y;
	public int width;
	public int height;

	public abstract void useItem();
	public abstract Texture2D getImage();
	public abstract int getWidth();
	public abstract int getHeight();
	

}
