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
	public int price;
	public string itemName;
	public string description;
	protected Character player;

	public Item(){
		this.image = getImage();
		this.width = getWidth();
		this.height = getHeight();
	}

	public abstract int getPrice();
	public abstract void useItem(Player player);
	public abstract Texture2D getImage();
	public abstract int getWidth();
	public abstract int getHeight();
	public abstract int getX();
	public abstract int getY();
	public abstract string getString();
	public abstract string getDescription();
	public abstract void setPlayer();
}
