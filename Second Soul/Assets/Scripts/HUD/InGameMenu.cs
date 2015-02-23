using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	
	// Constants
	public const int BOX_WIDTH         = 200;
	public const int BUTTON_WIDTH      = 150;
	public const int BUTTON_HEIGHT     = 50;
	public const int BUTTON_HEIGHT_MIN = 20;
	public const int SPACE_BUTTONS     = 20;
	public const int SPACE_BUTTONS_MIN = 10;
	
	// Variable responsible for hiding/unhiding the menu.
	bool isMenuPressed;

	// Buttons
	enum menuButtons {Resume, Options, Quit};

	// Length of the 'menuButtons'-enum.
	int enumLength = System.Enum.GetValues (typeof(menuButtons)).Length;

	// An array with the names of 'menuButtons'-enum.
	string[] names = System.Enum.GetNames(typeof(menuButtons));

	void Start ()
	{
	}
	
	// Update is called once per frame, checks if Escape-button is pressed.
	void Update ()
	{
		// Catches the Esc-button press.
		if(Input.GetKeyDown("escape"))
		{
			GameMenu();
		}
	}
	
	public void GameMenu ()
	{
		// Toggling (hiding/unhiding) the menu.
		if (isMenuPressed == true)
		{
			isMenuPressed = false;
		}
		else
		{
			isMenuPressed = true;
		}
	}

	// Returning the height of the box that contains the buttons.
	private int getBoxHeight (int numberButtons)
	{
		
		int preferableBoxHeight = (numberButtons * BUTTON_HEIGHT) + ((numberButtons + 1) * SPACE_BUTTONS);

		/* Will be improved */

//		int minBoxHeight        = (numberButtons * BUTTON_HEIGHT_MIN) + ((numberButtons + 1) * SPACE_BUTTONS_MIN);
//		int sceenHeight         = Screen.height;
//		
//		// If sceen's height is bigger or equal to prefered size - return prefered size.
//		if (sceenHeight >= preferableBoxHeight)
//		{
//			return preferableBoxHeight;
//		}
//		// If sceen's height is smaller or equal to min size - return min size.
//		else if (sceenHeight <= minBoxHeight)
//		{
//			return minBoxHeight;
//		}
//		// Else, box's height equal to the sceen's height.
//		else
//		{
//			return sceenHeight;
//		}

		return preferableBoxHeight;
	}
	
	void OnGUI() {

		// Screen percentage: 50% of the screen.
		int screenWidth50  = Screen.width / 2;
		int screenHeight50 = Screen.height / 2;
		
		// Box position.
		var boxWidthPosition  = screenWidth50 - BOX_WIDTH / 2;
		var boxHeightPosition = screenHeight50 - getBoxHeight(enumLength) / 2;
		
		if(isMenuPressed)
		{
			// Drawing the Box.
			GUI.Box(new Rect(boxWidthPosition, boxHeightPosition, BOX_WIDTH, getBoxHeight(enumLength)), /*"Main Menu"*/"");

			// Drawing the Buttons.
			for (int i = 0; i < enumLength; i++)
			{
				if (GUI.Button(new Rect (screenWidth50 - BUTTON_WIDTH / 2, boxHeightPosition + (i * BUTTON_HEIGHT) + ((i + 1) * SPACE_BUTTONS), BUTTON_WIDTH, BUTTON_HEIGHT), names[i]))
				{
					/* Need to add an implementation for each (new) button in "menuButtons"-enum. */

					// Resume
					if (string.Equals(names[i], System.Enum.GetName(typeof(menuButtons), menuButtons.Resume)))
					{
						isMenuPressed = false;
					}

					// Options
					if (string.Equals(names[i], System.Enum.GetName(typeof(menuButtons), menuButtons.Options)))
					{
						Debug.Log("Options-button pressed; no implementation yet.");
					}

					// Quit
					if (string.Equals(names[i], System.Enum.GetName(typeof(menuButtons), menuButtons.Quit)))
					{
						Application.Quit();
					}
				}
			}
		}
	}
}
