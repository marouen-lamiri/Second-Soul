using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	private Enemy enemyPrefab;
	
	/*private Vector3 spawnPosition = new Vector3 (341, 0, 984);
	private Vector3 spawnPosition2 = new Vector3 (328, 0, 984);
	private Vector3 spawnPosition3 = new Vector3 (340, 0, 983);
	private Vector3 spawnPosition4 = new Vector3 (339, 0, 982);*/

	private Fighter target;
	private Sorcerer sorcerer;

	// Use this for initialization
	public void setFactoryVariables(Enemy enemyPrefab, Fighter target, Sorcerer sorcerer){
		this.enemyPrefab=enemyPrefab;
		this.target=target;
		this.sorcerer=sorcerer;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void spawn(Vector3 spawnPosition){
		Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity)as Enemy;
		enemy.target = target;
		enemy.sorcerer = sorcerer;
		enemy.transform.parent = GameObject.Find("Enemies").transform;
	}
}
