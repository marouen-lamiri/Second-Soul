using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Fighter fighter;   // The Fighter from the scene.
	public Sorcerer sorcerer; // The Sorcerer from the scene.

	void Update()
	{
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

	// Move the MiniMap's camera to the entered position.
	void moveMiniMapCamera(Vector3 position)
	{
		this.transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
