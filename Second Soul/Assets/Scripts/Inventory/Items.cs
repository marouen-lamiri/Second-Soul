using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Items {

	public Texture2D Image;
	public int X;
	public int Y;
	public int Width;
	public int Height;

	//Abstract Method, should be overwritten in any inherited class
	public abstract void performAction ();
}


