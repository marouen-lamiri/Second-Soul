using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Shop : MonoBehaviour {

	protected int price;
	protected List<Item> sell = new List<Item>();

	//GUI Style
	public GUIStyle styleBox;
	public GUIStyle standartSkin;
	public GUIStyle buttonStyle;

	//Outer Box
	protected float boxWidth = Screen.width/1.3f;
	protected float boxHeight = Screen.height/1.3f;
	protected float boxStartPositionWidth = Screen.width/8;
	protected float boxStartPositionHeight = Screen.height/8;

	//Regular item Box
	protected float regularItemPositionWidth = Screen.width/4.5f;
	protected float regularItemPositionHeight = Screen.height/5.25f;
	protected float regularItemBoxWidth = Screen.width/3.8f;
	protected float regularItemBoxHeight = Screen.height/15;
	protected float offset = Screen.height/15;

	//Buy box
	protected float buyBoxStartPositionWidth = 2 * Screen.width/3 + Screen.width/24;
	protected float buyBoxStartPositionHeight = 2 * Screen.height/3;
	protected float buyItemBoxWidth = Screen.width/12;
	protected float buyItemBoxHeight = Screen.height/12;

	//Close box
	protected float closeBoxStartPositionWidth = 2 * Screen.width/3 - 3 * Screen.width/24;
	protected float closeBoxStartPositionHeight = 2 * Screen.height/3;
	protected float closeItemBoxWidth = Screen.width/12;
	protected float closeItemBoxHeight = Screen.height/12;

	//Image Box
	protected float ImageBoxWidth = Screen.width/3.5f;
	protected float ImageBoxHeight = Screen.height/5 + Screen.height/15;
	protected float ImageBoxLocationWidth = Screen.width/2 + Screen.width/48;
	protected float ImageBoxLocationHeight = Screen.height/2 - Screen.height/3.2f;

	//Description Box
	protected float descriptionBoxWidth = Screen.width/3.5f;
	protected float descriptionBoxHeight = Screen.height/5;
	protected float descriptionLocationWidth = Screen.width/2 + Screen.width/48;
	protected float descriptionLocationHeight = Screen.height/2 -  Screen.height/24;

	//Strings to be used in the GUI
	protected string buyButton = "Buy";
	protected string closeButton = "close";
	protected string sellButton = "Sell";
	protected string greetingMessage = "What would you like to buy?";

	//Shared Methods
	protected int retrieveItemPrice(Item item){
		return item.getPrice();
	}

	protected Texture2D retrieveImage(Item item){
		return item.getImage();
	}

	protected string retrieveString(Item item){
		return item.getString();
	}

	protected string retrieveDescritpion(Item item){
		return item.getDescription();
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
		return (Input.mousePosition.x > boxHeight && Input.mousePosition.x < boxHeight + boxStartPositionHeight);
	}

	public bool inWidthBoundaries(){
		return (Input.mousePosition.x > boxWidth && Input.mousePosition.x < boxWidth + boxStartPositionWidth);
	}

	public abstract bool shopEnabled();

}
