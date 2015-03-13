using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ring : Misc {

	public int price = 150;
	public string description = "This ring has been used by magicians for its durability" +
		"during the thousand year war against the demon country, Elexia";
	public string itemName = "Magicians Pride";

	public Ring(){
		
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
		return RingModel.getImage();
	}
	
	public override int getWidth(){
		return RingModel.getWidth();
	}
	
	public override int getHeight(){
		return RingModel.getHeight();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
