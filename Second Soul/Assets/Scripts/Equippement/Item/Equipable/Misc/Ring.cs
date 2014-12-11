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
		return RingModel.getImage();
	}
	
	public override int getWidth(){
		return RingModel.getWidth();
	}
	
	public override int getHeight(){
		return RingModel.getHeight();
	}
}
