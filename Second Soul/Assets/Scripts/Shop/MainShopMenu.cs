using UnityEngine;
using System.Collections;

public class MainShopMenu : MonoBehaviour {

	//Variables
	protected int distance = 1000;
	protected int sizeFont = 16;
	protected string shopDoor = "ShopDoor";
	protected string greeting = "What would you like to do?";
	protected string buyOption = "Buy";
	protected string sellOption = "Sell";
	protected string closeOption = "Close";
	protected bool showMenu;
	public GUIStyle buttons;

	void OnGUI(){
		clicked();
		if(showMenu){
			GUIStyle centeredStyle = GUI.skin.GetStyle("Box");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			centeredStyle.fontSize = sizeFont;
			GUI.Box (new Rect (Screen.width/3 - Screen.width/36, Screen.height/3 + Screen.height/32, Screen.width/3  + Screen.width/22, Screen.height/3), greeting, centeredStyle);
			if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + Screen.height/12, Screen.width/3, Screen.height/12), buyOption, buttons)){
				showMenu = !showMenu;
				if(Network.isServer){
					FighterShop.fighterShop = true;
				}
				else{
					SorcererShop.sorcererShop = true;
				}
			}
			if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 2 * Screen.height/12, Screen.width/3, Screen.height/12), sellOption, buttons)){
				showMenu = !showMenu;
				SellShop.close = true;
			}
			if(GUI.Button (new Rect (Screen.width/3, Screen.height/3 + 3 * Screen.height/12, Screen.width/3, Screen.height/12), closeOption, buttons)){
				showMenu = !showMenu;
			}
		}
	}

	void clicked(){
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distance)){
				if (hit.transform.name == shopDoor){
					showMenu = true;
				}
			}
		}
	}
}
