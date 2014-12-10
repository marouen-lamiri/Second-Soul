using UnityEngine;
using System.Collections;

[System.Serializable]
public class Axe : Weapon {

	public Axe(){
		
	}
	
	public override void useItem(){
		equip ();
	}
	
	public override void equip(){
		
	}
	
	public override void unequip(){
		
	}
	
	public override Texture2D getImage(){
		return AxeModel.getImage();
	}
	
	public override int getWidth(){
		return AxeModel.getWidth();
	}
	
	public override int getHeight(){
		return AxeModel.getHeight();
	}
}
