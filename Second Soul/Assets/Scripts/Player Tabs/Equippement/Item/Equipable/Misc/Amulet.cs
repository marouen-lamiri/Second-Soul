using UnityEngine;
using System.Collections;

[System.Serializable]
public class Amulet : Misc {

	public int price = 150;
	public string description = "This type of amulet is said to be used by apprentices all around the world "
		+ "It's easy to use, and very durable";
	public string itemName = "Apprentice Amulet";

	public Amulet(){
		
	}
	
	public override void useItem(){
		equip ();
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
	
	public override void equip(){
		
	}
	
	public override void unequip(){
		
	}
	
	public override Texture2D getImage(){
		return AmuletModel.getImage();
	}
	
	public override int getWidth(){
		return AmuletModel.getWidth();
	}
	
	public override int getHeight(){
		return AmuletModel.getHeight();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
