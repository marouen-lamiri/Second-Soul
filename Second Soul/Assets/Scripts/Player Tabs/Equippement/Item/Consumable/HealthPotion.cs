using UnityEngine;
using System.Collections;

[System.Serializable]
public class HealthPotion : Potion {

	public int price = 50;
	public string description = "This item has been used since the dawn of time to heal injuries sutained. " +
		"It has elven tears in it as it's healing agent and is said to be the most " +
		"powerful medicine in the world.";
	public string itemName = "Health Potion";
	public int healingPortion;
	public string methodToCall = "healCharacter";

	public HealthPotion() : base(){
	
	}

	public override void setPlayer(){
		if(player == null){
			player = (Character)GameObject.FindObjectOfType(typeof(Character));
		}
	}

	public override void useItem(Player player){
		consume (player);
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
	
	public override void consume(Player player){
		//setPlayer();
		player.SendMessage(methodToCall, determineAmount());
	}

	public override int determineAmount(){
		return healingPortion = (int) player.maxHealth/3;
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

	public override int getX(){
		return x;
	}

	public override int getY(){
		return y;
	}
}
