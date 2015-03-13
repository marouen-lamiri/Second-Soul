using UnityEngine;
using System.Collections;

[System.Serializable]
public class ManaPotion : Potion {

	public int price = 50;
	public string description = "This item has been used since the dawn of time to re-energize Humans. " +
		"It has Darwen tears in it as it's main energizing agent and is said to be the most " +
			"powerful tonic in the world.";
	public string itemName = "Mana Potion";

	public ManaPotion() : base(){
	
	}

	public override void useItem(){
		consume ();
	}

	public override int getPrice(){
		return price;
	}

	public override string getString(){
		return itemName;
	}
	
	public override string getDescription(){
		return description;
	}
	
	public override void consume(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			player.SendMessage("rechargeCharacter", 100);
		}
	}
	
	public override Texture2D getImage(){
		return ManaPotionModel.getImage();
	}
	
	public override int getWidth(){
		return ManaPotionModel.getWidth();
	}
	
	public override int getHeight(){
		return ManaPotionModel.getHeight();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
