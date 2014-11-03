using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Items {

	public Texture2D Image;
	public int X;
	public int Y;
	public int Width;
	public int Height;

	public abstract void performAction ();
}


