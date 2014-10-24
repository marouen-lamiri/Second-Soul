using System;
using NUnit.Framework;

[TestFixture]
public class PlayerHealthBarTest{

	public Fighter player = new Fighter();
	
	[Test]
	public void HealthBarLessOrEqualMaxHP ([Random(0, 10000, 5)] double value) {
		player.health = value;
		double maxhp = 10000;
		Assert.That (player.health <= maxhp);
	}
	
	[Test]
	public void HealthBarMoreThanZeroHP ([Random(0, 10000, 5)] double value) {
		player.health = value;
		Assert.That (player.health >= 0);
	}

	[Test]
	public void HealthBarLessThanZeroHP ([Random(0, 10000, 5)] double value) {
		player.health = value;
		Assert.That (!(player.health < 0));
	}
}
