using System;
using NUnit.Framework;

public class PlayerCombatTest {

	public Fighter player = new Fighter ();
	public Enemy enemy = new Enemy ();

	[Test]
	public void DamageEnemy ([Random(1, 100, 5)] int initHealth, [Random(1, 20, 5)] int damageValue) {
		// Arrange
		// 1. Enemy
		enemy.health = initHealth;

		// 2. Player
		//player.getDamage() = damageValue;

		// Act
		//enemy.takeDamage(player.getDamage(),DamageType.Physical);
		enemy.takeDamage(damageValue,DamageType.Physical);
		// Assert
		Assert.That (enemy.health < initHealth);
	}

}
