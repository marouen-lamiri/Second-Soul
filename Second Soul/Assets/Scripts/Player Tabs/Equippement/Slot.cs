using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slot 
{
	public bool occupied;
	public Rect position;
	
	public Slot(Rect position)
	{
		this.position = position;
	}
}
