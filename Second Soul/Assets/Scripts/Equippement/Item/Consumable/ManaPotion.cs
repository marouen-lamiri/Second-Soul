using UnityEngine;
using System.Collections;

[System.Serializable]
public class ManaPotion : Item {

	public override void performAction(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			player.SendMessage("rechargeCharacter", 100);
		}
	}
}
