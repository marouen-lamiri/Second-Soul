using System;
using NUnit.Framework;

public class GameOverTest{

	public PlayerCombat player = new PlayerCombat();

	[Test]
	public void GameOverTestLoadScreen () {
		Assert.True (player.gameOverScreen() == true);
	}
}
