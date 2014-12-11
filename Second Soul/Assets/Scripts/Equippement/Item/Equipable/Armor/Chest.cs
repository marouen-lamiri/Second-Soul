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
		//Debug.Log(FighterItems.chestSlot.position);
		foreach(EquipSlot slot in Storage.equipSlots){
			if(slot.type == this.GetType().ToString()){
				this.position = slot.position;	
				Storage.equipItems.Add(this);
				slot.item = this;
				/*Storage.equipSlots.Remove(slot);
				FighterItems.chestSlot.item = this;
				Storage.equipSlots.Add(FighterItems.chestSlot);*/
				return;
			}
		}
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
