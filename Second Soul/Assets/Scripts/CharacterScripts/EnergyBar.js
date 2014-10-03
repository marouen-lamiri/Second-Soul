#pragma strict
var globeHeight:float;
var globepic:Texture;
var globesize:int;
var energy:float;
var maxEnergy:float;
var energyPercent:float;

function Start () 
{

}

function OnGUI () {
	//Determining the health (through Percentage)
	energyPercent = energy/maxEnergy;
	if(energyPercent<0)
	{
		energyPercent = 0;
	}
	if(energyPercent>100)
	{
		energyPercent = 100;
	}
	globeHeight= energyPercent*globesize;
	
	//Drawing Energy Bar
	GUI.BeginGroup(new Rect(Screen.width - 80, Screen.height-(globeHeight+20), globesize,globesize));
	GUI.DrawTexture(Rect(0, -globesize+globeHeight, globesize, globesize),globepic);
	GUI.EndGroup();
	
	if(GUI.Button(Rect(Screen.width/2 - 65,Screen.height-globesize, globesize, globesize), "Energy+"))
	{
		energy+=10;
		if(energy>maxEnergy)
			energy = maxEnergy;
	}
	if(GUI.Button(Rect(Screen.width/2 - 130,Screen.height-globesize, globesize, globesize), "Energy-"))
	{
		energy-=10;
		if(energy<0)
			energy = 0;
	}	
}