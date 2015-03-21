using UnityEngine;
using System.Collections;

public class Node: IHeapItem<Node> {

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	int index;

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

	public int HeapIndex{
		get {
			return index;
		}
		set {
			index = value;
		}
	}

	public int CompareTo(Node nodeToCompare){
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if(compare == 0){
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}
