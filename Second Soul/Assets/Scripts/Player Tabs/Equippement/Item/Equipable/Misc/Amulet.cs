using UnityEngine;
using System.Collections;

[System.Serializable]
public class Amulet : Misc {

	public Amulet(){
		
	}
	
	public override void useItem(){
		equip ();
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
