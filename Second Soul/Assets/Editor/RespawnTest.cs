using System;
using NUnit.Framework;

public class RespawnTest {

	public ButtonRespawn respawn = new ButtonRespawn();
	
	[Test]
	public void RespawnTestLoadScreen () {
		Assert.That (respawn.loadNextScene () == true);
	}

	[Test]
	public void RespawnTestExitScreen () {
		Assert.That (respawn.ApplicationExit() == true);
	}
}
