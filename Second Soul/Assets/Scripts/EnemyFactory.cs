using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	public Enemy enemyPrefab;
	
	private Vector3 spawnPosition = new Vector3 (341, 0, 984);
	private Vector3 spawnPosition2 = new Vector3 (328, 0, 984);
	private Vector3 spawnPosition3 = new Vector3 (340, 0, 983);
	private Vector3 spawnPosition4 = new Vector3 (339, 0, 982);

	public Fighter target;
	public Sorcerer sorcerer;
	
	// Use this for initialization
	void Start () {
		
		Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity)as Enemy;
		enemy.target = target;
		enemy.sorcerer = sorcerer;
		Enemy enemy2 = Instantiate(enemyPrefab, spawnPosition2, Quaternion.identity)as Enemy;
		enemy2.target = target;
		enemy2.sorcerer = sorcerer;
		Enemy enemy3 = Instantiate(enemyPrefab, spawnPosition3, Quaternion.identity)as Enemy;
		enemy3.target = target;
		enemy3.sorcerer = sorcerer;
		Enemy enemy4 = Instantiate(enemyPrefab, spawnPosition4, Quaternion.identity)as Enemy;
		enemy4.target = target;
		enemy4.sorcerer = sorcerer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
