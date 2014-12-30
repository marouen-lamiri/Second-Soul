using UnityEngine;
using System.Collections;

public class NetworkLevelLoader : MonoBehaviour
{
	// implements singleton-style behavior
	public static NetworkLevelLoader Instance
	{
		get
		{
			// no instance yet? Create a new one
			if( instance == null )
			{
				GameObject go = new GameObject( "_networkLevelLoader" );
				// hide it to avoid cluttering up the hieararchy
				go.hideFlags = HideFlags.HideInHierarchy;
				instance = go.AddComponent<NetworkLevelLoader>();
				
				// don't destroy it when a new scene loads
				GameObject.DontDestroyOnLoad( go );
			}
			return instance;
		}
	}
	private static NetworkLevelLoader instance;
	
	public void LoadLevel( string levelName, int prefix = 0 )
	{
		StopAllCoroutines();
		StartCoroutine( doLoadLevel( levelName, prefix ) );
	}
	
	// do the work of pausing the network queue, 
	//loading the level, waiting, and then unpausing
	IEnumerator doLoadLevel( string name, int prefix )
	{
		Network.SetSendingEnabled( 0, false );
		Network.isMessageQueueRunning = false;
		
		Network.SetLevelPrefix( prefix );
		Application.LoadLevel( name );
		yield return null;
		yield return null;
		
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled( 0, true );
	}
}