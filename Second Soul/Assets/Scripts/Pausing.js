#pragma strict
var isPaused : boolean = false;
     
function Update()
{
    if(Input.GetKeyDown("p"))
    {
    	Pause();
    }
}

function Pause()
{
    if (isPaused == true)
	{
    	Time.timeScale = 1;
    	isPaused = false;
    }
    else
    {
    	Time.timeScale = 0;
    	isPaused = true;
    }
}

function OnGUI()
{
	 if(isPaused)
	 {
	 	GUI.Label (new Rect (Screen.width/2 - 75, Screen.height/2 - 25, 200, 100),"<Color=red><size=40>-Paused-</size></Color>");
	 }
}
