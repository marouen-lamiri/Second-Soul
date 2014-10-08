using UnityEngine;
using System.Collections;

public class PlayerEnergyBar : MonoBehaviour {

	public float globeHeight;
	public Texture globepic;
	public int globesize;
	public double energy;
	public double maxEnergy;
	public double energyPercent;
	public PlayerCombat player;
	
	void Start () 
	{
		energyPercent = 1;
		energy = player.energy;
		maxEnergy = energy;
	}
	
	void OnGUI () {
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
		globeHeight = (float) energyPercent*globesize;
		
		//Drawing Energy Bar
		GUI.BeginGroup(new Rect(Screen.width - 80, Screen.height-(globeHeight+20), globesize,globesize));
		GUI.DrawTexture(new Rect(0, -globesize+globeHeight, globesize, globesize),globepic);
		GUI.EndGroup();

	}
	
	// Update is called once per frame
	void Update () {
		energy = player.energy;
	}
}
