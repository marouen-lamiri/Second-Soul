using UnityEngine;
using System.Collections;

[System.Serializable]
public class Axe : Weapon {

	public int price = 150;
	public string description = "This axe is used to cut down trees, kill animals and even warfare. " +
		"Its a terrific allrounder that can be used in everyday life";
	public string itemName = "Wood cutting Axe";

	public Axe(){
		
	}

	public override void setPlayer(){
		if(player == null){
			player = (Character)GameObject.FindObjectOfType(typeof(Character));
		}
	}
	
	/*public override void useItem(){
		equip ();
	}*/

	public override int getPrice(){
		return price;
	}

	public override string getString(){
		return itemName;
	}
	
	public override string getDescription(){
		return description;
	}
	
	//moved up to weapon
	/*public override void equip(){
		//Debug.Log(FighterItems.chestSlot.position);
		foreach(EquipSlot slot in Storage.equipSlots){
			if(Storage.validEquipSlot(slot, this)){
				this.position = slot.position;	
				Storage.equipItems.Add(this);
				slot.item = this;
				/*Storage.equipSlots.Remove(slot);
				FighterItems.chestSlot.item = this;
				Storage.equipSlots.Add(FighterItems.chestSlot);*/
				/*return;
			}
		}
	}*/
	
	/*public override void unequip(){
		
	}*/
	
	public override Texture2D getImage(){
		return AxeModel.getImage();
	}
	
	public override int getWidth(){
		return AxeModel.getWidth();
	}
	
	public override int getHeight(){
		return AxeModel.getHeight();
	}

	public override int getX(){
		return x;
	}
	
	public override int getY(){
		return y;
	}
}
