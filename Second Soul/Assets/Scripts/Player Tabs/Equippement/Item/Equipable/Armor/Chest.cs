using UnityEngine;
using System.Collections;

[System.Serializable]
public class Chest : Armor {

	public int price = 150;
	public string description = "This Chest Plate has been always used in wartime, and is said to have said billons " +
		"during the thousand year war against the demon country, Elexia";
	public string itemName = "Warrior Chest Plate";

	public Chest() : base(){

	}
	
	public override void useItem(){
		equip ();
	}

	public override int getPrice(){
		return price;
	}

	public override string getString(){
		return itemName;
	}
	
	public override string getDescription(){
		return description;
	}
	
	public override void equip(){
		//Debug.Log(FighterItems.chestSlot.position);
		foreach(EquipSlot slot in Storage.equipSlots){
			if(slot.type == this.GetType()){
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

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
