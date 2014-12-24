using UnityEngine;
using System.Collections;

public class Node {

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(bool state, Vector3 position, int x, int y){
		walkable = state;
		worldPosition = position;
		gridX = x;
		gridY = y;
	}

	public int fCost {
		get{
			return gCost + hCost;
		}
	}
}
