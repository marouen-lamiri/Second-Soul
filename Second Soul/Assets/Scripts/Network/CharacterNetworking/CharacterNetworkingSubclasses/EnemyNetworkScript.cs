using UnityEngine;
using System.Collections;
using System;

public class EnemyNetworkScript : CharacterNetworkScript {

	// Use this for initialization
	void Start () {
		setCharacterScript ();
	}
	
	// Update is called once per frame
	void Update () {
		watchCharacterHealth ();

	}

	protected override void setCharacterScript() {
		characterScript = (Enemy)gameObject.GetComponent<Enemy> ();
	}
	

}
