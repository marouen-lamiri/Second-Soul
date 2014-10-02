#pragma strict
var globeHeight:float;
var globepic:Texture;
var globesize:int;
var hp:float;
var maxhp:float;
var healthpercent:float;

function Start () {
	healthpercent = 1;
}

function OnGUI () {
	//Determining the health (through Percentage)
	healthpercent = hp/maxhp;
	if(healthpercent<0)
	{
		healthpercent = 0;
	}
	if(healthpercent>0)
	{
		healthpercent = 0;
	}
	globeHeight= healthpercent+globesize;
	
	//Drawing health Bar
	GUI.BeginGroup(new Rect(20, Screen.height-(globeHeight+20), globesize,globesize));
	GUI.DrawTexture(Rect(0, -globesize+globeHeight, globesize, globesize),globepic);
	GUI.EndGroup();
	
	if(GUI.Button(Rect(Screen.width/2,Screen.height-globesize, globesize, globesize), "Healing"))
	{
		hp+=10;
		if(hp>maxhp)
			hp = maxhp;
	}
	if(GUI.Button(Rect(Screen.width/2 + 65,Screen.height-globesize, globesize, globesize), "Damage"))
	{
		hp-=10;
		if(hp<0)
			hp = 0;
	}	
}