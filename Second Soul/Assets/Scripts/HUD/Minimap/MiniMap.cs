using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour {

	private Fighter fighter;   // The Fighter from the scene.
	private Sorcerer sorcerer; // The Sorcerer from the scene.
	public Material material;
	List<GameObject> spheres = new List<GameObject>();
	List<GameObject> playerSpheres = new List<GameObject>();
	List<GameObject> enemySpheres = new List<GameObject>();

	void Start(){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
<<<<<<< HEAD

		if(Network.isServer) {
			buildMinimap ();
		}

=======
		buildMinimap ();
>>>>>>> parent of d799c52... Merge branch 'master' of https://github.com/marouen-lamiri/Second-Soul into Development
	}

	void buildMinimap(){
		int[,] map = MapGeneration.mapArray;
		for (int i=0; i<MapGeneration.mapSizeX; ++i) {
			for (int j=0; j<MapGeneration.mapSizeZ; ++j) {
				if (map [i,j] != 0){

					if (map [i - 1, j] == 0) {
						buildLine(new Vector3(i, -1, j)*10.0f,new Vector3(i,-1, j+1)*10.0f);
					}
					if (map [i + 1, j] == 0) {
						buildLine(new Vector3(i+1, -1, j)*10.0f,new Vector3(i+1,-1, j+1)*10.0f);
					}
					if (map [i, j - 1] == 0) {
						buildLine(new Vector3(i, -1, j)*10.0f,new Vector3(i+1,-1, j)*10.0f);
					}
					if (map [i, j + 1] == 0) {
						buildLine(new Vector3(i, -1, j+1)*10.0f,new Vector3(i+1,-1, j+1)*10.0f);
					}
				}
			}
		}
	}

	void buildLine(Vector3 start, Vector3 end){
		LineRenderer line = new GameObject ("Line").AddComponent<LineRenderer> ();
		//apparently I should use this as a default material, but it didn't work so I used some arbitrary material
		//line.material = new Material(Shader.Find("Particles/Additive"));
		line.material = material;
		line.gameObject.layer = LayerMask.NameToLayer ("Minimap");
		line.material.SetColor ("colour", Color.black);
		line.castShadows = false;
		line.receiveShadows = false;
		line.transform.parent = GameObject.Find("lines").transform;
		line.SetWidth(1.0f, 1.0f);
		line.SetVertexCount(2);
		line.SetPosition(0, start);
		line.SetPosition (1, end);
	}
	
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
