﻿using UnityEngine;
using System.Collections;

public class DatabaseFighter : MonoBehaviour {
	
	// Use this for initialization
	int interval = 300;
	int count;
	public Character player;
	private Fighter fighter;
	
	void Start () {
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
	}
	
	// Update is called once per frame
	void Update () {
		if(count == interval){
			savePrimaryStats();
			UnityNotificationBar.UNotify("Saved Fighter Stats"); //although this might appear false in Mono-Develop, it actually works as an external asset
			count = 0;
		}
		count++;
	}
	
	void savePrimaryStats(){
		PlayerPrefs.SetInt ("Strength", fighter.getStrength ());
		PlayerPrefs.SetInt ("Dexterity", fighter.getDexterity ());
		PlayerPrefs.SetInt ("Endurance", fighter.getEndurance ());
		PlayerPrefs.SetInt ("Level", fighter.getLevel());
		PlayerPrefs.SetInt ("NextLevel", fighter.getNextLevel());
		PlayerPrefs.SetInt ("LevelXP", fighter.getLevelXP());
		PlayerPrefs.SetFloat ("LevelFactor", fighter.getLevelBaseXP());
	}
	
	public void readPrimaryStats(){
		fighter = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		fighter.setStrength((int) PlayerPrefs.GetInt("Strength"));
		fighter.setDexterity((int) PlayerPrefs.GetInt("Dexterity"));
		fighter.setEndurance((int) PlayerPrefs.GetInt("Endurance"));
		fighter.setLevelXP((int)PlayerPrefs.GetInt("LevelXP"));
		fighter.setLevelBaseXP(PlayerPrefs.GetInt("LevelFactor"));
		fighter.setLevel((int)PlayerPrefs.GetInt("Level"));
		fighter.setNextLevel((int)PlayerPrefs.GetInt("NextLevel"));
		fighter.calculateNewPrimaryStats();
		fighter.calculateSecondaryStats();
		fighter.calculateLevel();
		fighter.calculateNextLevelXP();
	}
}
