using System;
using NUnit.Framework;

public class PlayerCombatTest {

	public PlayerCombat player = new PlayerCombat ();
	public Mob enemy = new Mob ();

	[Test]
	public void DamageEnemy ([Random(1, 100, 5)] int initHealth, [Random(1, 20, 5)] int damageValue) {
		// Arrange
		// 1. Enemy
		enemy.health = initHealth;

		// 2. Player
		player.damage = damageValue;

		// Act
		enemy.getHit(player.damage);

		// Assert
		Assert.That (enemy.health < initHealth);
	}

}
