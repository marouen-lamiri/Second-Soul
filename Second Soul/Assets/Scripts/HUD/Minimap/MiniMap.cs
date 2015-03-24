using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour, ISorcererSubscriber {

	private Fighter fighter;   // The Fighter from the scene.
	private Sorcerer sorcerer; // The Sorcerer from the scene.
	public static Material material;
	List<GameObject> spheres = new List<GameObject>();
	List<GameObject> playerSpheres = new List<GameObject>();
	List<GameObject> enemySpheres = new List<GameObject>();
	Player target;

	void Start(){

		subscribeToSorcererInstancePublisher (); // jump into game

		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); // sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
		target = (fighter.playerEnabled) ? (Player) fighter : sorcerer;
//		if(Network.isServer) {
//			buildMinimap ();
//		}

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

	public static void buildLine(Vector3 start, Vector3 end){
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
		string positions = start.x.ToString () + "," + start.y.ToString () + "," + start.z.ToString () + "," + end.x.ToString () + "," + end.y.ToString () + "," + end.z.ToString ();
		if (Network.isServer) {
			ClientNetwork client = (ClientNetwork) GameObject.FindObjectOfType(typeof(ClientNetwork));
			client.startClientLines (positions);
		}
	}

	public static void setLine(string positions) {
		string[] posStrings = positions.Split (',');

		Vector3 startVector3 = Vector3.zero;
		startVector3.x = float.Parse(posStrings[0]);
		startVector3.y = float.Parse(posStrings[1]);
		startVector3.z = float.Parse(posStrings[2]);

		Vector3 endVector3 = Vector3.zero;
		endVector3.x = float.Parse(posStrings[3]);
		endVector3.y = float.Parse(posStrings[4]);
		endVector3.z = float.Parse(posStrings[5]);

		buildLine (startVector3, endVector3);
	}

	void FixedUpdate()
	{
		this.transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
//		if (fighter.playerEnabled)
//		{
//			// Adjusting the MiniMap camera to the Fighter position.
//			this.moveMiniMapCamera(fighter.transform.position);
//		}
//		else 
//		{
//			// Adjusting the MiniMap camera to the Sorcerer position.
//			this.moveMiniMapCamera(sorcerer.transform.position);
//		}
	}

	// Move the MiniMap's camera to the entered position.
	void moveMiniMapCamera(Vector3 position)
	{
		this.transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}
}
