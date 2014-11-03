using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemList: MonoBehaviour {

	public List<Armor> armorInspector;
	public static List<Armor> armor;

	void Start(){
		armor = armorInspector;
	}
}
