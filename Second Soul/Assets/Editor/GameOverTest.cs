using System;
using NUnit.Framework;

public class GameOverTest{

	public PlayerCombat player = new PlayerCombat();

	[Test]
	public void GameOverTestLoadScreen ([Random(0, 10, 10)] double value) {
		Assert.True (player.gameOverScreen() == true);
	}
}
