using System;
using NUnit.Framework;

public class PlayerEnergyBarTest{

	public PlayerCombat player = new PlayerCombat();
	
	[Test]
	public void HealthBarLessOrEqualMaxHP ([Random(0, 100, 10)] double value) {
		player.energy = value;
		double maxEnergy = 100;
		Assert.That (player.energy <= maxEnergy);
	}
	
	[Test]
	public void HealthBarMoreThanZeroHP ([Random(0, 100, 10)] double value) {
		player.energy = value;
		Assert.That (player.energy >= 0);
	}
	
	[Test]
	public void HealthBarLessThanZeroHP ([Random(0, 100, 10)] double value) {
		player.energy = value;
		Assert.That (!(player.energy < 0));
	}
}
