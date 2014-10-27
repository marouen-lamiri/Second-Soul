using System;
using NUnit.Framework;

public class PausingTests{

	Pausing pause = new Pausing();

	//Tests if the time is stopped
	[Test]
	public void isGamePaused () {
		pause.Pause ();
		Assert.That (pause.getTimeScale() == 0);
	}

	//Tests if the time is resumed
	[Test]
	public void isGameResumed () {
		pause.Pause ();
		//pause.Pause ();
		Assert.That (pause.getTimeScale() == 1);
	}
	
}
