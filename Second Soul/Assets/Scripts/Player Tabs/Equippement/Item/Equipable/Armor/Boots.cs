using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boots : Armor {

	public Boots(){
		
	}
	
	public override void useItem(){
		equip ();
	}
	
	public override void equip(){
		
	}
	
	public override void unequip(){
		
	}
	
	public override Texture2D getImage(){
		return BootsModel.getImage();
	}
	
	public override int getWidth(){
		return BootsModel.getWidth();
	}
	
	public override int getHeight(){
		return BootsModel.getWidth();
	}

	public override string getTypeAsString(){
		return "Boots";
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
