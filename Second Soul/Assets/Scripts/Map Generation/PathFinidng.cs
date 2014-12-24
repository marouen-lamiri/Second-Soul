using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinidng : MonoBehaviour {

	Grid grid;
	public GameObject enemyPrefab, player;

	void Awake(){
		grid = GetComponent<Grid>();
	}

	void Update(){
		//findPath (player.transform.position, click.CheckTrajectory());
	}

	void findPath(Vector3 startPos, Vector3 endPos){
		Node startNode = grid.nodeFromWorld(startPos);
		Node targetNode = grid.nodeFromWorld(endPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		while(openSet.Count > 0){
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i++){
				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){
					currentNode = openSet[i];
				}
			}
			openSet.Remove (currentNode);
			closedSet.Add(currentNode);

			if(currentNode == targetNode){
				retracePath(startNode, targetNode);
				return;
			}
			foreach (Node neighbour in grid.getNeighbours(currentNode)){
				if(!neighbour.walkable || closedSet.Contains(currentNode)){
					continue;
				}

				int newMovementCostNeighbour = currentNode.gCost + getDistance (currentNode, neighbour);
				if(newMovementCostNeighbour < neighbour.gCost || !openSet.Contains (neighbour)){
					neighbour.gCost = newMovementCostNeighbour;
					neighbour.hCost = getDistance(neighbour, targetNode); 
					neighbour.parent = currentNode;

					if(!openSet.Contains (neighbour)){
						openSet.Add(neighbour);
					}

				}
			}
		}
	}

	void retracePath(Node startNode, Node targetNode){
		List<Node> path = new List<Node>();
		Node currentNode = targetNode;

		while(currentNode != startNode){
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();
		grid.path = path;
	}

	//Uses formula to determine the distance
	int getDistance(Node nodeA, Node nodeB){
		int distanceX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int distanceY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if(distanceX > distanceY){
			return 14*distanceY + 10 * (distanceX - distanceY);
		}
		return 14*distanceX + 10 * (distanceY - distanceX);
	}
}
