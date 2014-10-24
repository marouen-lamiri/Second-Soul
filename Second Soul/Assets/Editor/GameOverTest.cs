using System;
using NUnit.Framework;

public class GameOverTest{

	public Fighter player = new Fighter();

	[Test]
	public void GameOverTestLoadScreen () {
		Assert.True (player.gameOverScreen() == true);
	}
}
