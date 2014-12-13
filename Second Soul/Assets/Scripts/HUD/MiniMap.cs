using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Transform Fighter; // The Fighter from the scene.

	void Update()
	{
		// Adjusting the position of the MiniMap to the Fighter's one.
		this.transform.position = new Vector3 (Fighter.position.x, transform.position.y, Fighter.position.z);
	}
}
