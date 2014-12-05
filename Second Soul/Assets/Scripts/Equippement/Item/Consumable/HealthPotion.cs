using UnityEngine;
using System.Collections;

[System.Serializable]
public class HealthPotion : Item {

	public override void useItem(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			player.SendMessage("healCharacter", 100);
			Debug.Log ("Heal");
		}
	}
	
	public override Texture2D getImage(){
		return HealthPotionModel.getImage();
	}
	
	public override int getWidth(){
		return HealthPotionModel.getWidth();
	}
	
	public override int getHeight(){
		return HealthPotionModel.getHeight();
	}
}
