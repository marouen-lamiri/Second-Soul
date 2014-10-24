using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	public Enemy enemyPrefab;
	
	public Vector3 spawnPosition = new Vector3 (340, 0, 988);

	public Fighter player;
	
	// Use this for initialization
	void Start () {
		
		Enemy temp = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity)as Enemy;
		temp.playerTransform = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
