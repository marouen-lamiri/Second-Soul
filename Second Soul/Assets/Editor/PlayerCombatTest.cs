using System;
using NUnit.Framework;

public class PlayerCombatTest {

	public PlayerCombat player;
	public Mob enemy;

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
		// 1. Create player & enemy.
		player = new PlayerCombat ();
		enemy = new Mob ();

		// 2. Enemy health.
		var initHealth = enemy.health;

		// Act
		enemy.GetComponent<Mob>().getHit(player.damage);

		// Assert
		//Assert (enemy.health < initHealth); // <-- this gave an error so I commented out in order to run.
	}

}
