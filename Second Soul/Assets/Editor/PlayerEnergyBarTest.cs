using System;
using NUnit.Framework;

public class PlayerEnergyBarTest{

	public Fighter player = new Fighter();

	//Tests if the player Energy is higher than his maximum Energy
	[Test]
	public void energyBarLessOrEqualMaxHP () {
		double regularEnergy = player.energy;
		double maxEnergy = player.maxEnergy;
		Assert.That (regularEnergy <= maxEnergy);
	}

	//Tests if the player Energy is higher or equal to 0
	[Test]
	public void energyBarMoreThanZeroHP () {
		double regularEnergy = player.energy;
		Assert.That (regularEnergy >= 0);
	}

	//Tests if the player Energy is Lower to 0
	[Test]
	public void energyBarLessThanZeroHP () {
		double regularEnergy = player.energy;
		Assert.That (!(regularEnergy < 0));
	}
}
