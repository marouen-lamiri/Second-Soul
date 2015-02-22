﻿using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	private Enemy enemyPrefab;
	int mobRadius;
	/*private Vector3 spawnPosition = new Vector3 (341, 0, 984);
	private Vector3 spawnPosition2 = new Vector3 (328, 0, 984);
	private Vector3 spawnPosition3 = new Vector3 (340, 0, 983);
	private Vector3 spawnPosition4 = new Vector3 (339, 0, 982);*/

	private Fighter target;
	private Sorcerer sorcerer;

	// Use this for initialization
	public void setFactoryVariables(Enemy enemyPrefab, Fighter target, Sorcerer sorcerer){
		this.enemyPrefab = enemyPrefab;
		this.target = target;
		this.sorcerer = sorcerer;
	}

	void Start () {
		mobRadius = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawn(Vector3 spawnPosition){
		Enemy enemy = Network.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, 3)as Enemy;
		enemy.target = target;
		enemy.sorcerer = sorcerer;
		enemy.transform.parent = GameObject.Find("Enemies").transform;
	}

	public void spawnMob(Vector3 mobPosition, int mobSize){
		for (int i=0; i<mobSize; ++i) {
			Vector2 offset = Random.insideUnitCircle*3;
			Vector3 spawnPosition = mobPosition + new Vector3(offset.x, 0, offset.y);
			spawn (spawnPosition);
		}
	}
}
