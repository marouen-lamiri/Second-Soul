using System;
using NUnit.Framework;

[TestFixture]
public class PlayerHealthBarTest{

	Fighter player = new Fighter();

	//Tests if the player Health is higher than his maximum Health
	[Test]
	public void healthBarLessOrEqualMaxHP () {
		double regularHp = player.health;
		double maxHp = player.maxHealth;
		Assert.That (regularHp <= maxHp);
	}

	//Tests if the player Health is more or equal to 0
	[Test]
	public void healthBarMoreThanZeroHP () {
		double regularHp = player.health;
		Assert.That (regularHp >= 0);
	}

	//Tests if the player Health is less to 0
	[Test]
	public void healthBarLessThanZeroHP () {
		double regularHp = player.health;
		Assert.That (!(regularHp < 0));
	}
}
