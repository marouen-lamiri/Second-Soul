using UnityEngine;
using System.Collections;

public class HealModel : MonoBehaviour {
	
	private static Texture2D image;
	
	//used to put ressources directly from inspector rather than ressources
	public Texture2D imageInspector;
	
	// Use this for initialization
	void Start () {
		image = imageInspector;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static Texture2D getImage(){
		return image;
	}
}
