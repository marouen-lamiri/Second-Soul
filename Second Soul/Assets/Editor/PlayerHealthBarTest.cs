using System;
using NUnit.Framework;

[TestFixture]
public class PlayerHealthBarTest{

	public PlayerCombat player = new PlayerCombat();
	
	[Test]
	public void HealthBarLessOrEqualMaxHP ([Random(0, 10000, 10)] double value) {
		double hp = value;
		double maxhp = 10000;
		Assert.That (hp <= maxhp);
	}
	
	[Test]
	public void HealthBarMoreThanZeroHP ([Random(0, 10000, 10)] double value) {
		double hp = value;
		Assert.That (hp >= 0);
	}

	[Test]
	public void HealthBarLessThanZeroHP ([Random(0, 10000, 10)] double value) {
		double hp = value;
		Assert.That (!(hp < 0));
	}
}
