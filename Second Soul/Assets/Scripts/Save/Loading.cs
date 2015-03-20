using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour
{
	public Texture2D texture;
	public Texture2D fullBar;
	public Texture2D emptyBar;
	public GUIStyle loadingStyle;
	static Loading instance;
	static bool checkState;
	float positionX = 0.5f;
	float positionY = 0.5f;
	float positionZ = 1f;
	float halfBar = 200;
	float offset = 90;
	float buttonWidth = 200;
	float buttonHeight = 50;
	float buttonWidthOffset = 75;
	float heightForPart1 = 10;
	float heightForPart2 = 20;
	float offsetForPart1 = 15;
	float offsetForPart2 = 10;
	int loadingPercentage = 100;
	static float BarFullness;


	void Awake(){
		if (instance){
			Destroy(gameObject);
			hide();
			return;
		}
		instance = this;
		gameObject.AddComponent<GUITexture>().enabled = false;
		guiTexture.texture = texture;
		transform.position = new Vector3(positionX, positionY, positionZ);
		DontDestroyOnLoad(this);
	}

	void Update(){
		if(!Application.isLoadingLevel){
			BarFullness = 0;
			hide();
		}
//		else{
//			BarFullness = Application.GetStreamProgressForLevel(1);
//		}
	}

//	void OnGUI(){
//		if(checkState){
//			GUI.Label (new Rect(Screen.width/2 - buttonWidthOffset, Screen.height/2 - buttonHeight, buttonWidth, buttonHeight), "Loading: "+ Application.LoadLevelAsync("Forest") * loadingPercentage + "%", loadingStyle);
//			GUI.DrawTexture(new Rect(Screen.width/2 - halfBar,  Screen.height/2 + offsetForPart1, BarFullness * (halfBar + halfBar), heightForPart1), fullBar);
//			GUI.DrawTexture(new Rect(Screen.width/2 - halfBar - offset/2,  Screen.height/2 + offsetForPart2 , halfBar + halfBar + offset, heightForPart2), emptyBar);
//		}
//	}

	public static void show(){
		if (!InstanceExists())
		{
			return;
		}
		BarFullness = Application.GetStreamProgressForLevel(1);
		checkState = true;
		instance.guiTexture.enabled = true;
	}

	public static void hide(){
		if (!InstanceExists())
		{
			return;
		}
		BarFullness = 0;
		checkState = false;
		instance.guiTexture.enabled = false;
	}
	static bool InstanceExists(){
		if (!instance)
		{
			return false;
		}
		return true;
	}
}

