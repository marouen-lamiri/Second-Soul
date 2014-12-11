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
		//Debug.Log(FighterItems.chestSlot.position);
		foreach(EquipSlot slot in Storage.equipSlots){
			if(slot.type == this.GetType().BaseType.ToString()){
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
		return AxeModel.getImage();
	}
	
	public override int getWidth(){
		return AxeModel.getWidth();
	}
	
	public override int getHeight(){
		return AxeModel.getHeight();
	}
}
