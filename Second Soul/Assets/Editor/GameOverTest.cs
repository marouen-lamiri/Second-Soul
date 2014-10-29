using System;
using Unity;
using NUnit.Framework;

public class GameOverTest{

	Fighter player = new Fighter();

	[Test]
	public void GameOverTestLoadScreenWithHealthZero () {
		player.health = 0;
		Assert.True (player.isDead() == true);
	}

	[Test]
	public void GameOverTestLoadScreenWithHealthMoreThanZero () {
		player.health = 1;
		Assert.True (player.isDead() == false);
	}

	[Test]
	public void GameOverTestLoadScreenWithHealthLessThanZero () {
		player.health = -1;
		Assert.True (player.isDead() == true);
	}
}
