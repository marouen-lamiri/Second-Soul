using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerCombatTest {

	public PlayerCombat player = new PlayerCombat ();
	public Mob enemy = new Mob ();

	#if false
		[Test]
		public void EnemyInRange ()
		{
				// Arrange
				// Act
				// Assert
		}
	#endif

	[Test]
	public void DamageEnemy () {
		// Arrange
		// 1. Enemy
		double initHealth = 100;
		enemy.health = 100;

		// 2. Player
		player.damage = 40;

		// Act
		enemy.getHit(player.damage);

		// Assert
		Assert.That (enemy.health < initHealth);
	}

}
