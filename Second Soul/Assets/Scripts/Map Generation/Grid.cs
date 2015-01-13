using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	// Use this for initialization
	public LayerMask unwalkableMask; 
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public GameObject player;
	Node[,] grid;
	public List<Node> path;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start(){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); 
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter); 
		createGrid();
	}

	public List<Node> getNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;
				
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		
		return neighbours;
	}

	public Node nodeFromWorld(Vector3 worldPosition){
		float percentX = (worldPosition.x) / gridWorldSize.x;
		float percentY = (worldPosition.z) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		
		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	//Creates a list of all the position in a path using node path
	public List<Vector3> worldFromNode(List<Node> node){
		List<Vector3> path = new List<Vector3>();
		for(int i = 0; i < node.Count; i++){
			path.Add(new Vector3(node[i].worldPosition.x, 0, node[i].worldPosition.z));
			Debug.Log("The next position is: " + path[i]);
		}
		return path;
	}

	void createGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position;

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				grid[x,y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
		
		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				if (path != null)
					if (path.Contains(n))
						Gizmos.color = Color.black;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}
