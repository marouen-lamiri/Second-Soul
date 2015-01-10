using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sword : Weapon {

	public Sword(){
		
	}
	
	public override void useItem(){
		equip ();
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
	
	public override string getTypeAsString(){
		return "Sword";
	}
}
