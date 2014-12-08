using UnityEngine;
using System.Collections;

public class SorcererItems : EquippedItems {

	public static Amulet amulet;
	public static Ring ring;
	
	public static EquipSlot amuletSlot;
	public static EquipSlot ringSlot;
	
	// Use this for initialization
	void Start () {
		amuletSlot = new EquipSlot(new Rect(), new Amulet());
		ringSlot = new EquipSlot(new Rect(), new Ring());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
