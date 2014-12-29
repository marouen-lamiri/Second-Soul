using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	private Fighter fighter;   // The Fighter from the scene.
	private Sorcerer sorcerer; // The Sorcerer from the scene.

	void Start(){
	}

	void Update()
	{
		if(fighter == null) {
			fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		}
		if(sorcerer == null) {
			sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		}
		if(fighter != null && sorcerer != null) {
			if (fighter.playerEnabled)
			{
				// Adjusting the MiniMap camera to the Fighter position.
				this.moveMiniMapCamera(fighter.transform.position);
			}
			else if (sorcerer.playerEnabled)
			{
				// Adjusting the MiniMap camera to the Sorcerer position.
				this.moveMiniMapCamera(sorcerer.transform.position);
			}
		}
	}

	// Move the MiniMap's camera to the entered position.
	void moveMiniMapCamera(Vector3 position)
	{
		this.transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
