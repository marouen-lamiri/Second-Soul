﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Misc : Item, IEquipable {

	public Misc() : base(){
		
	}

	public override abstract void useItem();
	public override abstract Texture2D getImage();
	public override abstract int getWidth();
	public override abstract int getHeight();
	public abstract void equip();
	public abstract void unequip();
	public override abstract int getX();
	public override abstract int getY();
	public override abstract string getTypeAsString();

}