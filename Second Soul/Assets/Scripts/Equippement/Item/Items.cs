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
	public List<Item> itemsInspector;
	private static List<Item> items;

	void Start(){
		armor = armorInspector;
		healthPotionList = healthPotionListInspector;
		manaPotionList = manaPotionListInspector;
	}

	public static Armor getArmor(int id){
		return armor[id];
	}

	public static HealthPotion getHealthPotion(int id){
		return healthPotionList[id];
	}

	public static ManaPotion getManaPotion(int id){
		return manaPotionList[id];
	}
}
