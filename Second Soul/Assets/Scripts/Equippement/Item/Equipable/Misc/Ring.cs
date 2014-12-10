using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ring : Misc {

	public Ring(){
		
	}

	public override void useItem(){
		equip ();
	}
	
	public override void equip(){
	
	}
	
	public override void unequip(){
		
	}
	
	public override Texture2D getImage(){
		return RingModel.getImage();
	}
	
	public override int getWidth(){
		return RingModel.getWidth();
	}
	
	public override int getHeight(){
		return RingModel.getHeight();
	}
}
