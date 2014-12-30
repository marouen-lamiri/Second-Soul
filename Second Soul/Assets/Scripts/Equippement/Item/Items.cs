using UnityEngine;
using System.Collections.Generic;

public class Items : MonoBehaviour {

	private Character player;
	public List<Chest> chestInspector;
	private static List<Chest> chest;
	public List<HealthPotion> healthPotionListInspector;
	private static List<HealthPotion> healthPotionList;
	public List<ManaPotion> manaPotionListInspector;
	private static List<ManaPotion> manaPotionList;

	void Start(){
		player = (Character)GameObject.FindObjectOfType (typeof(Character));
		healthPotionList = healthPotionListInspector;
		manaPotionList = manaPotionListInspector;
		chest = chestInspector;
	}
	
	public static Chest getChest(int id){
		return chest[id];
	}

	public static HealthPotion getHealthPotion(int id){
		return healthPotionList[id];
	}

	public static ManaPotion getManaPotion(int id){
		return manaPotionList[id];
	}
}
