using System;
using NUnit.Framework;

[TestFixture]
public class PlayerHealthBarTest{

	public PlayerCombat player = new PlayerCombat();
	
	[Test]
	public void HealthBarLessOrEqualMaxHP ([Random(0, 10000, 10)] double value) {
		player.health = value;
		double maxhp = 10000;
		Assert.That (player.health <= maxhp);
	}
	
	[Test]
	public void HealthBarMoreThanZeroHP ([Random(0, 10000, 10)] double value) {
		player.health = value;
		Assert.That (player.health >= 0);
	}

	[Test]
	public void HealthBarLessThanZeroHP ([Random(0, 10000, 10)] double value) {
		player.health = value;
		Assert.That (!(player.health < 0));
	}
}
