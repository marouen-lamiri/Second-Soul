using UnityEngine;
using System.Collections;

public class Loading: MonoBehaviour
{
	public Texture2D texture;
	static Loading instance;
	bool checkState;


	void Awake(){
		if (instance){
			Destroy(gameObject);
			hide();
			return;
		}
		instance = this;
		Debug.Log (animation);
		gameObject.AddComponent<GUITexture>().enabled = false;
		guiTexture.texture = texture;
		transform.position = new Vector3(0.5f, 0.5f, 1f);
		DontDestroyOnLoad(this);
	}

	void Update(){
		if(!Application.isLoadingLevel){
			hide();
			checkState = false;
		}
		else{
			checkState = true;
		}
	}

	void OnGUI(){
		if(checkState){

		}
	}

	public static void show(){
		if (!InstanceExists())
		{
			return;
		}
		instance.guiTexture.enabled = true;
	}

	public static void hide(){
		if (!InstanceExists())
		{
			return;
		}
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

