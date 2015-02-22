using UnityEngine;
using System.Collections;

public class SkillTreeModel : MonoBehaviour {

	private static Rect position;	
	private static Texture2D image;
	
	//used to put ressources directly from inspector rather than ressources
	public Texture2D imageInspector;
	public Rect positionInspector;
	
	// Use this for initialization
	void Start () {
		image = imageInspector;
		position = positionInspector;
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public static Rect getPosition(){
		return position;
	}
	
	public static Texture2D getImage(){
		return image;
	}
}
