using UnityEngine;
using System.Collections;

public interface IEquipable {
	
	void equip(Player player);
	void unequip(Player player);
}
