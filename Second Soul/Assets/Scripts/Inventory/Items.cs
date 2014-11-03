using UnityEngine;
using System.Collections.Generic;

public class Items : MonoBehaviour {

	public Character player;
	public List<Armor> armorInspector;
	private static List<Armor> armor;
	public List<HealthPotion> healthPotionListInspector;
	private static List<HealthPotion> healthPotionList;
	public List<ManaPotion> manaPotionListInspector;
	private static List<ManaPotion> manaPotionList;

	void Start(){
		armor = armorInspector;
		healthPotionList = healthPotionListInspector;
		manaPotionList = manaPotionListInspector;
	}

	public static Armor getArmor(int id){
		Armor armor = new Armor();
		armor.image = Items.armor[id].image;
		armor.width = Items.armor[id].width;
		armor.height = Items.armor[id].height;
		return armor;
	}

	public static HealthPotion getHealthPotion(int id){
		HealthPotion potion = new HealthPotion();
		potion.image = Items.healthPotionList[id].image;
		potion.width = Items.healthPotionList[id].width;
		potion.height = Items.healthPotionList[id].height;
		return potion;
	}

	public static ManaPotion getManaPotion(int id){
		ManaPotion mana = new ManaPotion();
		mana.image = Items.manaPotionList[id].image;
		mana.width = Items.manaPotionList[id].width;
		mana.height = Items.manaPotionList[id].height;
		return mana;
	}
}
