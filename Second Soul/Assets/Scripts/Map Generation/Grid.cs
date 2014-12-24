using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// Use this for initialization
	public LayerMask unwalkableMask; 
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public GameObject player;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start(){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); 
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter); 
		createGrid();
	}

	public Node nodeFromWorld(Vector3 worldPosition){
		float percentX = (worldPosition.x * gridWorldSize.x/2)/ gridWorldSize.x;
		float percentY = (worldPosition.z * gridWorldSize.y/2)/ gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];


	}

	void createGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				grid[x,y] = new Node(walkable, worldPoint);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,0, gridWorldSize.y));
		
		if(grid != null){
			Node playerNode = nodeFromWorld(player.transform.position);
			foreach(Node n in grid){
				if(n.walkable) 
				{
					Gizmos.color = Color.white;
				}
				else{
					Gizmos.color = Color.red;
				}
				if(playerNode == n){
					Gizmos.color = Color.cyan;
				}
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}
