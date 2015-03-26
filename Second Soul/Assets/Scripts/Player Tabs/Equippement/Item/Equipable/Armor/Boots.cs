using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boots : Armor {

	public int price = 150;
	public string description = "Boots used for all exploration as they are made of the most durable materials " +
		"found in the world";
	public string itemName = "Adventure Boots";

	public Boots(){
		
	}

	public override void setPlayer(){
		if(player == null){
			player = (Character)GameObject.FindObjectOfType(typeof(Character));
		}
	}
	
	/*public override void useItem(Inventory inventory){
		equip (inventory);
	}*/

	public override int getPrice(){
		return price;
	}

	public override string getString(){
		return itemName;
	}
	
	public override string getDescription(){
		return description;
	}
	
	/*public override void equip(){

	}*/
	
	/*public override void unequip(Inventory inventory){

	}*/
	
	public override Texture2D getImage(){
		return BootsModel.getImage();
	}
	
	public override int getWidth(){
		return BootsModel.getWidth();
	}
	
	public override int getHeight(){
		return BootsModel.getWidth();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
