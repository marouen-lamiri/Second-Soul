using UnityEngine;
using System.Collections;

[System.Serializable]
public class Chest : Armor {

	public Chest(){
		
	}
	
	public override void useItem(){
		equip ();
	}
	
	public override void equip(){
		EquippedItems.items.Add(this);
		FighterItems.chest = this;	
	}
	
	public override Texture2D getImage(){
		return ChestModel.getImage();
	}
	
	public override int getWidth(){
		return ChestModel.getWidth();
	}
	
	public override int getHeight(){
		return ChestModel.getHeight();
	}
}
