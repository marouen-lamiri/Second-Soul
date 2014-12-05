using UnityEngine;
using System.Collections;

[System.Serializable]
public class Armor : Item{

	public override void useItem(){

	}
	
	public override Texture2D getImage(){
		return ArmorModel.getImage();
	}
	
	public override int getWidth(){
		return ArmorModel.getWidth();
	}
	
	public override int getHeight(){
		return ArmorModel.getHeight();
	}
}
