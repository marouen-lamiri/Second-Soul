using UnityEngine;
using System.Collections;

[System.Serializable]
public class Chest : Armor {

	public Chest() : base(){

	}
	
	public override void useItem(){
		equip ();
	}
	
	public override void equip(){
		Debug.Log(FighterItems.chestSlot.position);
		this.position = FighterItems.chestSlot.position;	
		EquippedItems.equipItems.Add(this);
		
		FighterItems.equipSlots.Remove(FighterItems.chestSlot);
		FighterItems.chestSlot.item = this;
		FighterItems.equipSlots.Add(FighterItems.chestSlot);
	}
	
	public override void unequip(){
	
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
