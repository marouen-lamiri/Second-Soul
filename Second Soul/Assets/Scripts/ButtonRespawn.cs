using UnityEngine;
using System.Collections;

public class ButtonRespawn : MonoBehaviour {

	public Font myFont;

	void OnGUI () {
		if (!myFont) {
			Debug.LogError("No font found, assign one in the inspector.");
			return;
		}
		GUI.skin.font = myFont;
		GUI.color = Color.red;
		//GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (Screen.width / 2 - 30, Screen.height / 2 + 20, 95, 25), "-Respawn-"))
			loadNextScene ();
		if (GUI.Button (new Rect (Screen.width / 2 - 30, Screen.height / 2 + 45, 95, 25), "-Close-"))
			ApplicationExit ();
	}

	public bool loadNextScene()
	{
		Application.LoadLevel ("killPlayer");
		return true;
	}

	public bool ApplicationExit()
	{
		Application.Quit ();
		return true;
	}
}
