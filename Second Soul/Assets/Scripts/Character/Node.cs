using UnityEngine;
using System.Collections;

public class Node {

	public bool walkable;
	public Vector3 worldPosition;

	public Node(bool state, Vector3 position){
		walkable = state;
		worldPosition = position;
	}
}
