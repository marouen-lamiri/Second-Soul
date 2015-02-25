using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour
{
	public Texture2D texture;
	public Texture2D fullBar;
	public Texture2D emptyBar;
	static Loading instance;
	static bool checkState;
	float halfBar = 200;
	float offset = 90;
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
		transform.position = new Vector3(0.5f, 0.5f, 1f);
		DontDestroyOnLoad(this);
	}

	void Update(){
		if(!Application.isLoadingLevel){
			BarFullness = 0;
			hide();
		}
		else{
			BarFullness = Application.GetStreamProgressForLevel(1);
		}
	}

	void OnGUI(){
		if(checkState){
			GUI.Label (new Rect(Screen.width/2 -25, Screen.height/2 - 25, 200, 50), "Loading: "+ Application.GetStreamProgressForLevel(1) * 99 + "%");
			GUI.DrawTexture(new Rect(Screen.width/2 - halfBar,  Screen.height/2 + 15, BarFullness * (halfBar + halfBar), 10), fullBar);
			GUI.DrawTexture(new Rect(Screen.width/2 - halfBar - offset/2,  Screen.height/2 + 10 , halfBar + halfBar + offset, 20), emptyBar);
		}
	}

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

