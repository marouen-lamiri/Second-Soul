using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemList: MonoBehaviour {

	public List<Armor> armorInspector;
	private static List<Armor> armor;

	//Initialize Both Lists
	void Start(){
		armor = armorInspector;
	}

	public static Armor getArmor (int Id) {
		Armor armor = new Armor();
		armor.Image = ItemList.armor [Id].Image;
		armor.Width = ItemList.armor [Id].Width;
		armor.Height = ItemList.armor [Id].Height;
		return armor;
	}
}
