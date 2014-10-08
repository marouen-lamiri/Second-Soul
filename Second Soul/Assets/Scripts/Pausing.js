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