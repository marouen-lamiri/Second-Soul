using UnityEngine;
using System.Collections;

[System.Serializable]
public class ManaPotion : Item {

	public override void useItem(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			player.SendMessage("rechargeCharacter", 100);
		}
	}
	
	public override Texture2D getImage(){
		return ManaPotionModel.getImage();
	}
	
	public override int getWidth(){
		return ManaPotionModel.getWidth();
	}
	
	public override int getHeight(){
		return ManaPotionModel.getHeight();
	}
}
