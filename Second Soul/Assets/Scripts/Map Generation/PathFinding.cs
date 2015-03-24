using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour, ISorcererSubscriber {
	
	private Player seeker, target;
	int maxTrial = 2000;

	Grid grid;
	
	void Awake() {

		subscribeToSorcererInstancePublisher (); // jump into game

		seeker = GameObject.FindObjectOfType(typeof(Fighter))as Fighter;
		target = (Sorcerer)SorcererInstanceManager.getSorcerer (); // target = GameObject.FindObjectOfType (typeof(Sorcerer))as Sorcerer;
		grid = GetComponent<Grid>();
		seeker.setPathing (this);
		target.setPathing (this);
	}

	public void StartFindPath(Vector3 startPos, Vector3 targetPos){
		//StartCoroutine(findPath(startPos, targetPos));
	}
	
	public void findPath(Vector3 startPos, Vector3 targetPos) {

//		Vector3 [] waypoints = new Vector3[0];
//		bool pathSuccess = false;
		Node startNode = grid.nodeFromWorld(startPos);
		Node targetNode = grid.nodeFromWorld(targetPos);
		//this can be changed, it's purpose is not to allow an infinite loop to occur in case of an impossible path, althought this shouldn't happen, it is present for emergency
		int limitedTrial = maxTrial; 
		if(startNode.walkable && targetNode.walkable){
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.removeFirst();

	//			for (int i = 1; i < openSet.Count; i ++) {
	//				if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
	//						currentNode = openSet[i];
	//				}
	//			}
	//			
	//			openSet.Remove(currentNode);
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					//pathSuccess = true;
					RetracePath(startNode,targetNode);
					grid.worldFromNode(grid.path);
					break;
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
						else{
							openSet.UpdateItem(neighbour);
						}
					}
					if(limitedTrial <= 0){
						limitedTrial = maxTrial;
						grid.path = null;
						return;
					}
					limitedTrial--;
				}
			}
		}
		else{
			grid.path = null;
		}
	}

	//Reverse path since path finding gives the opposite path 
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

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.target = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

}