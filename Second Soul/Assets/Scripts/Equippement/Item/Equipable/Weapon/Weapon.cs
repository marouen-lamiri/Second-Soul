﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : Item, IEquipable {
	
	public Weapon() : base(){
		
	}
	
	public override abstract void useItem();
	public override abstract Texture2D getImage();
	public override abstract int getWidth();
	public override abstract int getHeight();
	public abstract void equip();
	public abstract void unequip();
	
}
