using UnityEngine;
using System.Collections;

public class DisplayPlayerStatsNew : MonoBehaviour {

	// Constants
	public const int BOX_WIDTH         = 200;
	public const int LABEL_WIDTH       = 150;
	public const int BUTTON_WIDTH      = 150;
	public const int LABEL_HEIGHT      = 20;
	public const int BUTTON_HEIGHT     = 50;
	public const int LABLE_HEIGHT_MIN  = 20;
	public const int BUTTON_HEIGHT_MIN = 20;
	public const int SPACE_BETWEEN     = 10;
	public const int SPACE_BETWEEN_MIN = 5;
	
	// Variable responsible for hiding/unhiding the menu.
	bool isStatsPressed;

	// Label style.
	public GUIStyle labelStyle;

	// Labels and Buttons
	//enum primaryStatsLabels {Level = "Level", Class = "Class", Strength = "Strength", Dexterity = "Dexterity", Endurance = "Endurance", Health = "Health", Energy = "Energy"};
	//enum secondaryStatsLabels {Armor = "Armor", FireResistance = "FireResistance", Accuracy = "Accuracy", AttackSpeed = "AttackSpeed"};
	enum primaryStatsLabels {Level, Class, Strength, Dexterity, Endurance, Health, Energy};
	enum secondaryStatsLabels {Armor, FireResistance, Accuracy, AttackSpeed};
	enum primaryStatsButtons {Details};
	
	// Lengths of the all enums.
	int primaryLength   = System.Enum.GetValues (typeof(primaryStatsLabels)).Length;
	int secondaryLength = System.Enum.GetValues (typeof(secondaryStatsLabels)).Length;
	int buttonsLength   = System.Enum.GetValues (typeof(primaryStatsButtons)).Length;
	
	// Arrays with the names of all enums.
	string[] namesPrimary   = System.Enum.GetNames(typeof(primaryStatsLabels));
	string[] namesSecondary = System.Enum.GetNames(typeof(secondaryStatsLabels));
	string[] namesButtons   = System.Enum.GetNames(typeof(primaryStatsButtons));

	void Start ()
	{
	}
	
	// Update is called once per frame, checks if 's'-key is pressed.
	void Update ()
	{
		// Catches the 's'-key press.
		if(Input.GetKeyDown("s"))
		{
			PlayerStats();
		}
	}
	
	public void PlayerStats ()
	{
		// Toggling (hiding/unhiding) the primary stats.
		if (isStatsPressed == true)
		{
			isStatsPressed = false;
		}
		else
		{
			isStatsPressed = true;
		}
	}
	
	// Returning the height of the box that contains the labels and buttons.
	private int getBoxHeight (int numberLabels)
	{
		
		int preferableBoxHeight = (numberLabels * LABEL_HEIGHT) + ((numberLabels + 1) * SPACE_BETWEEN) + (BUTTON_HEIGHT + SPACE_BETWEEN);

		return preferableBoxHeight;
	}
	
	void OnGUI()
	{
		
		// Screen percentage: 50% of the screen.
		int screenWidth50  = Screen.width / 2;
		int screenHeight50 = Screen.height / 2;
		
		// Box position.
		var boxWidthPosition  = screenWidth50 - BOX_WIDTH / 2;
		var boxHeightPosition = screenHeight50 - getBoxHeight(primaryLength) / 2;
		
		if(isStatsPressed)
		{
			// Drawing the Box.
			GUI.Box(new Rect(boxWidthPosition, boxHeightPosition, BOX_WIDTH, getBoxHeight(primaryLength)), /*"Primary Stats"*/"");

			int i;
			// Drawing the Lables.
			for (i = 0; i < primaryLength; i++)
			{
				//GUI.Label(new Rect (screenWidth50 - LABEL_WIDTH / 2, boxHeightPosition + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), namesPrimary[i], labelStyle);
				GUI.Label(new Rect (screenWidth50 - LABEL_WIDTH / 2, boxHeightPosition + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), namesPrimary[i]);

//				if (GUI.Button(new Rect (screenWidth50 - BUTTON_WIDTH / 2, boxHeightPosition + (i * BUTTON_HEIGHT) + ((i + 1) * SPACE_BETWEEN), BUTTON_WIDTH, BUTTON_HEIGHT), namesButtons[i]))
//				{
//					/* Need to add an implementation for each (new) button in "primaryStatsButtons"-enum. */
//					
//					// Resume
//					if (string.Equals(namesButtons[i], System.Enum.GetName(typeof(primaryStatsButtons), primaryStatsButtons.Resume)))
//					{
//						isStatsPressed = false;
//					}
//					
//					// Options
//					if (string.Equals(namesButtons[i], System.Enum.GetName(typeof(primaryStatsButtons), primaryStatsButtons.Options)))
//					{
//						Debug.Log("Options-button pressed; no implementation yet.");
//					}
//					
//					// Quit
//					if (string.Equals(namesButtons[i], System.Enum.GetName(typeof(primaryStatsButtons), primaryStatsButtons.Quit)))
//					{
//						Application.Quit();
//					}
//				}
			}
			int j;
			// Drawing the Buttons.
			for (j = 0; j < buttonsLength; j++)
			{
				GUI.Button(new Rect (screenWidth50 - BUTTON_WIDTH / 2, boxHeightPosition + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), BUTTON_WIDTH, BUTTON_HEIGHT), namesButtons[j]);
			}

			GUI.Box(new Rect(5, 35, 110, 75), new GUIContent("Box", "this box has a tooltip"));
			GUI.Button(new Rect(10, 80, 100, 20), "No tooltip here");
			GUI.Button(new Rect(10, 150, 100, 20), new GUIContent("I have a tooltip", "The button overrides the box"));
			GUI.Label(new Rect(10, 190, 100, 40), GUI.tooltip);
		}
	}
}
