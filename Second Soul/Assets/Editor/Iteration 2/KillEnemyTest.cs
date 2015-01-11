using System;
using NUnit.Framework;
using UnityEngine;

public class KillEnemyTest {
	
	public Fighter player = new Fighter ();
	public Enemy enemy = new Enemy ();
	
	[Test]
	public void KillEnemy ([Random(1, 100, 2)] int initHealth, [Random(1, 20, 2)] int damageValue) {
		// Arrange
		// 1. Enemy
		enemy.health = initHealth;
		
		// 2. Player
		//player.damage = damageValue;

		// Act
		while (enemy.health > 0)
			enemy.takeDamage(damageValue,DamageType.Physical);

		// Assert
		Assert.IsTrue (enemy.isDead());
	}
}
