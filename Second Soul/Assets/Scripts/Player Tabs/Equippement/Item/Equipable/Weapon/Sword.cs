using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sword : Weapon {

	public int price = 150;
	public string description = "A very durable sword used by knights all around the world";
	public string itemName = "Knight's sword";

	public Sword(){
		
	}

	public override void setPlayer(){
		if(player == null){
			player = (Character)GameObject.FindObjectOfType(typeof(Character));
		}
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
		return SwordModel.getImage();
	}
	
	public override int getWidth(){
		return SwordModel.getWidth();
	}
	
	public override int getHeight(){
		return SwordModel.getHeight();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
