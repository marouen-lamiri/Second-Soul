using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	protected string greeting= "Would you like to teleport to the next location?";
	protected string okMessage = "Yes, please";
	protected string noMessage = "No, I'm not ready";
	protected string closeMessage = "close";
	protected int distance = 1000;
	protected bool showMenu = false;
	protected int sizeFont = 16;

	// Update is called once per frame
	public bool checkBoundaries () {
		return showMenu && inBoundaries();
	}

	public bool inBoundaries(){
		//lock player movement within HUD bounds
		if(inWidthBoundaries() && inHeightBoundaries()){
			return true;
		}
		else{
			return false;
		}
	}
	
	public bool inHeightBoundaries(){
		return (Input.mousePosition.y > Screen.height/3 + Screen.height/32 && Input.mousePosition.y < Screen.height/3 + Screen.height/32 + Screen.height/3);
	}
	
	public bool inWidthBoundaries(){
		return (Input.mousePosition.x > Screen.width/3 - Screen.width/36 && Input.mousePosition.x < Screen.width/3 - Screen.width/36 + Screen.width/3  + Screen.width/22);
	}
}
