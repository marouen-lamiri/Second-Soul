using UnityEngine;
using System.Collections;

public class HealthPotionModel : MonoBehaviour {

	private static Texture2D image;
	private static int width;
	private static int height;
	
	
	public Texture2D imageInspector;
	public int widthInspector;
	public int heightInspector;
	
	// Use this for initialization
	void Start () {
		image = imageInspector;
		width = widthInspector;
		height = heightInspector;
	}
	
	public static Texture2D getImage(){
		return image;
	}
	
	public static int getWidth(){
		return width;
	}
	
	public static int getHeight(){
		return height;
	}
}
