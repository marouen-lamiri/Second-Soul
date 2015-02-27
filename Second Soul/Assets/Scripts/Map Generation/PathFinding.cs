using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {
	
	private Player seeker, target;
	
	Grid grid;
	
	void Awake() {
		seeker = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		target = GameObject.FindObjectOfType (typeof(Sorcerer))as Sorcerer;
		grid = GetComponent<Grid>();
		seeker.setPathing (this);
		target.setPathing (this);
	}
	
	void FixedUpdate() {
		//findPath(seeker.transform.position,target.transform.position);
	}
	
	public void findPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.nodeFromWorld(startPos);
		Node targetNode = grid.nodeFromWorld(targetPos);
		int limitedTrial = 2000;
		
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
						currentNode = openSet[i];
				}
			}
			
			openSet.Remove(currentNode);
			closedSet.Add(currentNode);
			
			if (currentNode == targetNode) {
				RetracePath(startNode,targetNode);
				grid.worldFromNode(grid.path);
				return;
			}

			foreach (Node neighbour in grid.getNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}
				
				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;
					
					if (!openSet.Contains(neighbour)){
						openSet.Add(neighbour);
					}

					if(limitedTrial <= 0){
						limitedTrial = 2000;
						return;
					}
					limitedTrial--;
				}
			}
		}
	}
	
	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		
		grid.path = path;
		
	}
	
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}