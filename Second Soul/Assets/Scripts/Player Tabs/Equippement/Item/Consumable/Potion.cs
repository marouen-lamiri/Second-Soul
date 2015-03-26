using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Potion : Item, IConsumable {

	public Potion() : base(){
		
	}
			
	public override abstract void useItem(Player player);
	public override abstract Texture2D getImage();
	public override abstract int getWidth();
	public override abstract int getHeight();
	public abstract void consume(Player player);
	public abstract int determineAmount();
	public override abstract int getX();
	public override abstract int getY();
	public override abstract void setPlayer();
}
