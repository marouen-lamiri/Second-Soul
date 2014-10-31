using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	public Enemy enemyPrefab;
	
	private Vector3 spawnPosition = new Vector3 (341, 0, 984);
	private Vector3 spawnPosition2 = new Vector3 (328, 0, 984);

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
