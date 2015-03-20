using UnityEngine;
using System.Collections;

public class Stash : Storage {

	public Texture2D image;
	
	public int slotsOffsetX;
	public int slotsOffsetY;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("u")) {
			toggleTab();
		}
	}
	
	protected void toggleTab(){
		isStashOn = !isStashOn;
	}
	
	void OnGUI(){
		if (isStashOn) {
			drawStash ();
			//drawSlots ();
			drawItems ();
			//Debug.Log (itemPickedUp);
		}
	}
	
	void drawStash(){
		position.x = 0;
		position.y = Screen.height - position.height - Screen.height * 0.2f;
		GUI.DrawTexture(position, image);
	}
	
	protected override void drawItems(){
		for(int i = 0; i < stashItems.Count; i++){
			stashItems[i].position = new Rect(6 + slotsOffsetX + position.x + stashItems[i].x * slotWidth, 
												6 + slotsOffsetY + position.y + stashItems[i].y * slotHeight,
												stashItems[i].width * slotWidth - 12, 
												stashItems[i].height * slotHeight - 12);
			GUI.DrawTexture(stashItems[i].position, stashItems[i].getImage());
		}
	}
}
