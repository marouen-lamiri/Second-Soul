using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Misc : Item, IEquipable {

	public Misc() : base(){
		
	}

	//public override abstract void useItem(Inventory inventory);
	public override abstract Texture2D getImage();
	public override abstract int getWidth();
	public override abstract int getHeight();
	//public abstract void equip();
	//public abstract void unequip(Inventory inventory);
	public override abstract int getX();
	public override abstract int getY();

	public override void useItem(Player player){
		equip (player);
	}
	
	public void equip(Player player){
		//Debug.Log(FighterItems.chestSlot.position);
		foreach(EquipSlot slot in Storage.equipSlots){
			Inventory inventory = player.inventory;
			if(Storage.validEquipSlot(slot, this)){
				if(slot.item != null){
					Item item = slot.item;
					slot.item = null;
					inventory.takeItem(item);	
				}
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
	
	public void unequip(Player player){
		
	}
}
