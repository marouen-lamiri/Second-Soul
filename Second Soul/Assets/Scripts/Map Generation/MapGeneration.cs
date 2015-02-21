using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour{
	
	// Use this for initialization
	public GameObject wallPrefab;
	public GameObject obstaclePrefab;
	public GameObject torchPrefab;
	public GameObject crystalPrefab;
	public GameObject statuePrefab;
	public Enemy enemyPrefab;
	public EnemyFactory enemyfactory;
	public ItemHolder itemHolderPrefab;
	public LootFactory lootFactory;
	private Fighter fighter;
	private Sorcerer sorcerer;
	List<int> listOfWalls;
	public static int[,] mapArray;
	int numberRooms = 5;
	public static int mapSizeX = 8;
	public static int mapSizeZ = 8;

	// network:
	public static Vector3 playerStartPositionVector3;
	public static bool mapGenerationCompleted;
	
	void Awake () {
		mapGenerationCompleted = false;
		if (Network.isServer) {
			fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
			sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
			enemyfactory.setFactoryVariables(enemyPrefab, fighter, sorcerer);
			lootFactory.setFactoryVariables(itemHolderPrefab, fighter);
			mapArray = generateMap (mapSizeX, mapSizeZ, numberRooms, fighter.gameObject, sorcerer.gameObject);
			buildMap (mapArray);
			mapGenerationCompleted = true; // signal for client network to set the sorcerer's position with rpc call
		} 
	}
	
	// Update is called once per frame
	void Update () {

//		// generate the map only after the players have been created (becasue they are needed for some reason for the map generation code:
//		bool serverAndClientAreBothConnected = Network.connections.Length != 0; // 0 length means no connection, i.e. no client connected to server.
//		print ("BEFORE 1");
//		if(serverAndClientAreBothConnected && Network.isServer) {
//			print ("BEFORE 2.0");
//			fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
//			sorcerer = (Sorcerer) GameObject.FindObjectOfType (typeof (Sorcerer));
//			if(fighter != null) {
//				print ("BEFORE 2.1");
//				if(sorcerer != null) {
//					print ("BEFORE 2.2");
//					factory.setFactoryVariables(enemyPrefab, fighter, sorcerer);
//					int[,] map = generateMap (mapSizeX, mapSizeZ, numberRooms, fighter, sorcerer);
//					buildMap (map);
//					
//					// immediately after creating the map, load the game scene: the map and players (fighter and sorcerer) should be kept, using DontDestroyOnLoad
//					print ("BEFORE 3");
//					NetworkLevelLoader.Instance.LoadLevel("NetworkingCollaboration",1); 
//					print ("AFTER");
//
//				}
//			}
//		}
		
	}
	
	
	int[,] playerStartPosition(int [,] map, GameObject player){
		for (int i=0; i<mapSizeX; i++) {
			for(int j=0;j<mapSizeZ;j++){
				if (map [i,j] != 0 && map [i-1,j] != 0 && map [i,j-1] != 0 && map [i-2,j] != 0 && map [i,j-2] != 0 && map [i,j] != 99
				    && map [i-1,j-1] != 0 && map [i-2,j-2] != 0
				    && map [i,j] != 98 && map [i,j] != 90){
					map [i,j] = 99;
					playerStartPositionVector3 = new Vector3(i*10,0.08f,j*10);
					player.transform.position = playerStartPositionVector3;
					fighter.setInitialPosition(playerStartPositionVector3);
					i = mapSizeX;
					j = mapSizeZ;
				}
			}
		}
		return map;
	}

	
	int[,] enemySpawnLocation(int [,] map){
		int nbrEnemies = Random.Range (25,35);
		int nbrEnemiesByRoom = Random.Range (1,4);
		int previousRoomNumber = 0;
		for (int i=0; i<mapSizeX; i++) {
			for(int j=0;j<mapSizeZ;j++){
				if (map [i, j] != 0 && map [i - 1, j] != 0 && map [i, j - 1] != 0 && nbrEnemies != 0 && nbrEnemiesByRoom != 0
				    && previousRoomNumber != map [i, j] && map [i, j] != 99 && map [i,j] != 98 && map [i-1,j] != 98 && map [i,j-1] != 98 
				    && map [i-2,j] != 98 && map [i,j-2] != 98  && map [i,j-1] != 98 && map [i,j-2] != 98
				    && map [i-1,j-1] != 98){
					map [i,j] = 98;
					Vector3 spawnLocation = new Vector3(i*10,0,j*10);
					enemyfactory.spawnMob(spawnLocation,5);
					nbrEnemies--;
					nbrEnemiesByRoom--;
					if(nbrEnemiesByRoom == 0){
						previousRoomNumber = map[i,j];
						nbrEnemiesByRoom = Random.Range (1,4);
					}
				}
			}
		}
		return map;
	}
	
	void buildMap(int[,] map){
		for (int i=0; i<mapSizeX; i++) {
			for(int j=0;j<mapSizeZ;j++){
				if (map [i,j] != 0){
					buildWalls(map,i,j);
				}
			}
		}
	}

	//Checks all 4 directions
	void buildWalls (int [,] map,int i,int j){
		Vector3 position = new Vector3 (i*10,0,j*10);

		if (map [i - 1, j] == 0) {
			GameObject wall = Network.Instantiate(wallPrefab, new Vector3(position.x,0,position.z+5), Quaternion.Euler (0,180,0), 2)as GameObject;
			wall.transform.parent = GameObject.Find("Walls").transform;
			DontDestroyOnLoad(wall.transform.gameObject);
		}
		if (map [i + 1, j] == 0) {
			GameObject wall = Network.Instantiate(wallPrefab, new Vector3(position.x+10,0,position.z+5), new Quaternion(), 2)as GameObject;
			wall.transform.parent = GameObject.Find("Walls").transform;
			DontDestroyOnLoad(wall.transform.gameObject);
		}
		if (map [i, j - 1] == 0) {
			GameObject wall = Network.Instantiate(wallPrefab, new Vector3(position.x+5,0,position.z), Quaternion.Euler (0,90,0), 2)as GameObject;
			wall.transform.parent = GameObject.Find("Walls").transform;
			DontDestroyOnLoad(wall.transform.gameObject);
		}
		if (map [i, j + 1] == 0) {
			GameObject wall = Network.Instantiate(wallPrefab, new Vector3(position.x+5,0,position.z+10), Quaternion.Euler (0,-90,0), 2)as GameObject;
			wall.transform.parent = GameObject.Find("Walls").transform;
			DontDestroyOnLoad(wall.transform.gameObject);
		}
		
	}
	
	int[,] createDungeonHalls(int[,] map, int sizeX, int sizeZ, int numberRooms){
		Vector2[] corridorStarts = new Vector2[numberRooms];
		Vector2[] corridorEnds = new Vector2[numberRooms];
		//This segment randomizes the starting and ending points of each corridor
		for (int i=0; i<numberRooms; ++i) {
			bool goodPosition=false;
			do{
				int x = Random.Range (0,mapSizeX-1);
				int z = Random.Range (0,mapSizeZ-1);//mapsize is a 2D array, so y is representing our z axis since in 3D y is up
				if(map[x,z]==i+1){//because rooms start as 1. 0 is empty squares
					goodPosition=true;
					corridorStarts[i]= new Vector2(x,z);
					map[x,z]=(i+1)*-1;
				}
			}while(!goodPosition); 
			goodPosition=false;
			do{
				int x = Random.Range (0,mapSizeX-1);
				int z = Random.Range (0,mapSizeZ-1);//mapsize is a 2D array, so y is representing our z axis since in 3D y is up
				if(map[x,z]!=0 && map[x,z]!=i+1){
					goodPosition=true;
					corridorEnds[i]= new Vector2(x,z);
					map[x,z]=(i+1)*-1;
				}
			}while(!goodPosition);
			
			//This segment randomizes the sizes of the squares that make up the corridors
			int hallSizeX = Random.Range (2,5);//used to randomize width of Z-direction hall
			int hallSizeZ = Random.Range (2,5);//used to randomize width of X-direction hall
			
			int xAdjusted;
			int zAdjusted;
			for(int x=0; x<Mathf.Abs(corridorEnds[i].x-corridorStarts[i].x); ++x){
				
				for(int z=0; z<hallSizeZ; ++z){
					if((corridorEnds[i].x-corridorStarts[i].x)<0){
						xAdjusted=x*-1;
					}
					else{
						xAdjusted=x;
					}
					if((corridorEnds[i].y-corridorStarts[i].y)<0){
						zAdjusted=z*-1;
					}
					else{
						zAdjusted=z;
					}
					if(map[(int)corridorStarts[i].x+xAdjusted,(int)corridorStarts[i].y+ zAdjusted]==0){
						map[(int)corridorStarts[i].x+xAdjusted,(int)corridorStarts[i].y+ zAdjusted]=-1;
					}
				}
			}
			for(int z=0; z<Mathf.Abs(corridorEnds[i].y-corridorStarts[i].y); ++z){
				
				for(int x=0; x<hallSizeX; ++x){
					
					if((corridorEnds[i].x-corridorStarts[i].x)<0){
						xAdjusted=x*-1;
					}
					else{
						xAdjusted=x;
					}
					if((corridorEnds[i].y-corridorStarts[i].y)<0){
						zAdjusted=z*-1;
					}
					else{
						zAdjusted=z;
					}
					if(map[(int)corridorEnds[i].x-xAdjusted,(int)corridorEnds[i].y-zAdjusted]==0){
						map[(int)corridorEnds[i].x-xAdjusted,(int)corridorEnds[i].y-zAdjusted]=-1;
					}
				}
			}
		}
		string[] text = new string[sizeX];
		
		for(int i=0;i<sizeX;++i){
			string line="";
			for(int j=0; j<sizeZ; ++j){
				if(map[i,j]>=0){
					line +=".";
				}
				line +=map[i,j];
				line += " ";
			}
			text[i]=line;
			
		}
		System.IO.File.WriteAllLines("yourtextfile.txt", text);
		//Debug.Log(line);
		return map;
		
	}
	
	int[,] generateMap(int sizeX, int sizeZ, int squaresToGenerate, GameObject player, GameObject player2){
		int [,] genMap;
		
		genMap = createSquares(sizeX, sizeZ, squaresToGenerate);
		numberRooms = FindRooms (genMap, sizeX, sizeZ);
		if (numberRooms >= 1) {
			genMap = createDungeonHalls(genMap, sizeX,sizeZ,numberRooms);
			genMap = generateObstacles (genMap, torchPrefab, false);
			genMap = generateObstacles (genMap, crystalPrefab, false);
			genMap = generateObstacles (genMap, obstaclePrefab, true);
			genMap = generateObstacles (genMap, statuePrefab, false);
			genMap = playerStartPosition(genMap, player);

			genMap = playerStartPosition(genMap, player2); // now setting a private var for the player2 position, because we need to send it over the network so the right instance sets its position.
			ClientNetwork clientNetwork = (ClientNetwork)GameObject.FindObjectOfType(typeof(ClientNetwork));
			//clientNetwork.OnSorcererPositionDeterminedAfterMapCreation(playerStartPositionVector3);
			clientNetwork.onStatsDisplayed();


			genMap = enemySpawnLocation (genMap);
		}		
		return genMap;
	}

	int[,] generateObstacles (int [,] map, GameObject obstacle, bool type){
		int nbrObstacles = Random.Range (10,20);
		int nbrObstaclesByRoom = Random.Range (4,12);
		int previousRoomNumber = 0;
		for (int i = 0; i < mapSizeX; i = Random.Range(i,i+3)) {
			for (int j = 0; j < mapSizeZ; j = Random.Range(j,j+3)) {
				if (map [i,j] != 0 && map [i-1, j] != 0 && map [i,j-1] != 0 && map [i-1,j-1] != 0 && map [i+1,j+1] != 0  && nbrObstacles != 0 
				    && map [i-1, j+1] != 0 && map [i+1,j-1] != 0 && map [i-2,j+1] != 0 && map [i+2,j-1] != 0
				    && nbrObstaclesByRoom != 0 && previousRoomNumber != map [i, j] && map [i, j] != 99 && map [i,j] != 98 && map [i-1,j] != 98 
				    && map [i,j-1] != 98 && map [i+1,j] != 98 && map [i,j-1] != 98 
				    && map [i-1,j-1] != 98 && map [i+1,j+1] != 98 && map[i,j] != 90) {
					map[i,j] = 90;
					GameObject mapObject;
					if(type){
						mapObject = (GameObject) Network.Instantiate (obstacle, new Vector3(i*10, -5.4f, j*10), Quaternion.Euler (0,0,0), 2)as GameObject;
						DontDestroyOnLoad(mapObject);
					}
					else {
						mapObject = (GameObject) Network.Instantiate (obstacle, new Vector3(i*10, 0, j*10), Quaternion.Euler (0,0,0), 2) as GameObject;
						DontDestroyOnLoad(mapObject.transform.gameObject);
					}
					if(mapObject.tag == "Crystals"){
						mapObject.transform.parent = GameObject.Find(mapObject.tag).transform;
					}
					else if(mapObject.tag == "Rock"){
						mapObject.transform.parent = GameObject.Find(mapObject.tag).transform;
					}
					else if(mapObject.tag == "Statue"){
						mapObject.transform.parent = GameObject.Find(mapObject.tag).transform;
					}
					else if(mapObject.tag == "TorchObstacles"){
						mapObject.transform.parent = GameObject.Find(mapObject.tag).transform;
					}

					nbrObstacles--;
					nbrObstaclesByRoom--;
					if(nbrObstaclesByRoom <= 0){
						previousRoomNumber = map [i,j];
						nbrObstaclesByRoom = Random.Range (1,5);
					}

				}
			}
		}
		return map;
	}
	
	static int[,] createSquares(int sizeX, int sizeZ, int squaresToGenerate){
		int[,] newMap = new int[sizeX,sizeZ]; // Create empty map with imputted dimensions
		
		for(int i = 0; i < squaresToGenerate; i++){
			// Calculate random room size and position. Make sure the room is inside the map.
			int roomSizeX = Random.Range (3,sizeX/8);
			int roomSizeZ = Random.Range (3,sizeZ/8);
			int roomPosX  = Random.Range (1,sizeX-roomSizeX);
			int roomPosZ  = Random.Range (1,sizeZ-roomSizeZ);
			
			// Add the square to the map. Avoid making square on the external values of the map.
			for(int j = roomPosX; j < roomPosX + roomSizeX; j++){
				for(int k = roomPosZ; k < roomPosZ + roomSizeZ; k++){
					newMap[j,k] = 1;
				}
			}
		}
		return newMap;
	}
	
	// Loop through the dungeon map and find rooms created by touching squares
	static int FindRooms(int[,] genMap, int sizeX, int sizeZ){
		
		List<Vector2> listCoordToTest = new List<Vector2>(); // List of all coordinate that need to be evaluated for the current room.
		int[,] modifiedMap  = new int[sizeX,sizeZ]; // This array will be a copy of modifiedMap where all known room cell are set as 0 to be ignored by the next steps
		int    nbrRoomFound = 0;
		
		System.Array.Copy(genMap, modifiedMap, sizeX*sizeZ); // Create a copy of the genMap in modifiedMap
		
		//Loop through all position of the map
		for(int i = 0; i < sizeX; i++){
			for(int j = 0; j < sizeZ; j++){
				// If a room is found
				if(modifiedMap[i,j] == 1){
					listCoordToTest.Add(new Vector2(i, j)); // Add the coordinate to the list to test
					while(listCoordToTest.Count > 0){  // Loop while there are coordinates to test 
						int x = (int)listCoordToTest[0].x; // Find x value of coordinate to test
						int z = (int)listCoordToTest[0].y; // Find y value of coordinate to test
						listCoordToTest.RemoveAt (0); // Remove the currently tested coordinate
						
						genMap[x,z] = nbrRoomFound + 1; // Update the genMap with the current number of room found
						
						// Look the 8 coordinate around the current coordinate to find for connected rooms
						for(int xAround = x - 1; xAround <= x + 1; xAround++){
							for(int zAround = z - 1 ; zAround <= z + 1; zAround++){
								// Test if evaluated coordinate is a square and need to be added to the room
								if(modifiedMap[xAround,zAround] == 1){
									listCoordToTest.Add(new Vector2(xAround, zAround)); // Add it to the list of coordinates to test
									modifiedMap[xAround,zAround] = 0; // Remove the room position from the modified map so we don't step on it again
								}
							}
						}
					}
					nbrRoomFound++;
				}
			}
		}
		return nbrRoomFound;
	}
}

