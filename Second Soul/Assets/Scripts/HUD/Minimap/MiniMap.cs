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

		GameObject fighterSphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		fighterSphere.transform.parent = fighter.transform;
		playerSpheres.Add (fighterSphere);
		GameObject sorcererSphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sorcererSphere.transform.parent = sorcerer.transform;
		playerSpheres.Add (sorcererSphere);
		spheres.AddRange (playerSpheres);

		GameObject [] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i=0; i<enemies.GetLength(0); ++i) {
			GameObject enemySphere = GameObject.CreatePrimitive (PrimitiveType.Sphere); 
			enemySphere.transform.parent=enemies[i].transform;
			enemySpheres.Add (enemySphere);
		}
		spheres.AddRange (enemySpheres);
		for (int i=0; i<spheres.Count; ++i) {
			spheres[i].renderer.material.color = Color.red;
			spheres[i].renderer.castShadows = false;
			spheres[i].renderer.receiveShadows = false;
			//was going to scale to 0.1f, but scaling the map down didn't seem to work
			spheres[i].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			spheres[i].gameObject.layer = LayerMask.NameToLayer ("Minimap");
		}
		for (int i=0; i<playerSpheres.Count; ++i) {
			playerSpheres[i].renderer.material.color = Color.blue;
		}
		buildMinimap ();
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
		fighter.transform.FindChild("Sphere").transform.position=new Vector3(fighter.transform.position.x, 10.0f, fighter.transform.position.z);
		sorcerer.transform.FindChild("Sphere").transform.position=new Vector3(sorcerer.transform.position.x, 10.0f, sorcerer.transform.position.z);
		for (int i=0; i<enemySpheres.Count; ++i) {
			enemySpheres[i].transform.position = new Vector3 (enemySpheres[i].transform.parent.transform.position.x, 10.0f, enemySpheres[i].transform.parent.transform.position.z);
		}

	}

	// Move the MiniMap's camera to the entered position.
	void moveMiniMapCamera(Vector3 position)
	{
		this.transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
