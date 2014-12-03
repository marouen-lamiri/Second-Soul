using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour{

	// Use this for initialization
	List<int> listOfWalls;
	int[,] mapArray;
	int numberRooms = 10;
	int mapSizeX = 100;
	int mapSizeZ = 100;

	void Start () {
		generateMap (mapSizeX, mapSizeZ, numberRooms);
	}
	
	// Update is called once per frame
	void Update () {
		
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

			/*int  random= Random.Range(0,1);
			bool startXAxis;
			if(random<0.5f){
				startXAxis=true;
			}
			else{
				startXAxis=false;
			}
			if(startXAxis){*/
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

			/*}
			else{

			}*/
			//This segment draws the corridors as '-1' on the map
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

	int[,] generateMap(int sizeX, int sizeZ, int squaresToGenerate){
		int [,] genMap;

		genMap = createSquares(sizeX, sizeZ, squaresToGenerate);
		numberRooms = FindRooms (genMap, sizeX, sizeZ);
		if (numberRooms >= 1) {
			genMap = createDungeonHalls(genMap, sizeX,sizeZ,numberRooms);

		}

		//SpawnEnvironment(genMap, sizeX, sizeZ, numberRooms); // Create environment(Spider eggs for now)
		
		//listOfWalls = EvaluateWallToBuild(genMap, sizeX, sizeZ);
		//InstantiateMapWalls(_ListOfWalls);
		
		return genMap;
	}

	static int[,] createSquares(int sizeX, int sizeZ, int squaresToGenerate){
		int[,] newMap = new int[sizeX,sizeZ]; // Create empty map with imputted dimensions
		
		for(int i = 0; i < squaresToGenerate; i++){
			// Calculate random room size and position. Make sure the room is inside the map.
			int roomSizeX = Random.Range (10,sizeX/5);
			int roomSizeZ = Random.Range (10,sizeZ/5);
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
