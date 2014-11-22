using UnityEngine;
using System.Collections;

[System.Serializable]
public class HealthPotion : Item {

	public override void performAction(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			player.SendMessage("healCharacter", 100);
			Debug.Log ("Heal");
		}
	}
}
