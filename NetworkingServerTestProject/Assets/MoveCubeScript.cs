using UnityEngine;
using System.Collections;

public class MoveCubeScript : MonoBehaviour {

	float carVelocity = 1.3f;
	float carDirectionAngle = 4.0f;//1.8f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKey(KeyCode.LeftArrow)) {
//			transform.Rotate(0,-carDirectionAngle,0);
//		} 
//		else if(Input.GetKey(KeyCode.RightArrow)) {
//			transform.Rotate(0,carDirectionAngle,0);
//		} 
//		if(Input.GetKey(KeyCode.UpArrow)) {
//			//transform.Translate (0,0,carVelocity * Time.deltaTime); 
//			transform.Translate (0,0,Input.GetAxis("Vertical") * Time.deltaTime);
//		} 
//		else if(Input.GetKey(KeyCode.DownArrow)) {
//			//transform.Translate (0,0,-carVelocity * Time.deltaTime); 
//			transform.Translate (0,0,Input.GetAxis("Vertical") * Time.deltaTime);
//		}




		// move the cube around:
		if (Network.isServer)
		{
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			float speed = 5;
			transform.Translate(speed * moveDir * Time.deltaTime);
		}

		//

	}


}
