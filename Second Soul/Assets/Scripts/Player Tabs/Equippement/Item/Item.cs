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

	public Item(){
		this.image = getImage();
		this.width = getWidth();
		this.height = getHeight();
	}

	public abstract void useItem();
	public abstract Texture2D getImage();
	public abstract int getWidth();
	public abstract int getHeight();
	public abstract int getX();
	public abstract int getY();
}
