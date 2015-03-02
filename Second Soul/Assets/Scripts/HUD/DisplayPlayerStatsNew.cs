using UnityEngine;
using System.Collections;

public class DisplayPlayerStatsNew : MonoBehaviour {

	// Constants
	public const int BOX_WIDTH         = 150;
	public const int LABEL_WIDTH       = 150;
	public const int BUTTON_WIDTH      = 70;
	public const int LABEL_HEIGHT      = 20;
	public const int BUTTON_HEIGHT     = 20;
	public const int SPACE_BETWEEN     = 5;

	// Variable related to the scroll-bar.
	public Vector2 scrollPosition = Vector2.zero;

	// Variable responsible for hiding/unhiding the menu.
	bool isStatsPressed;
	bool isSecondaryStatsPressed;

	// Label style.
	public GUIStyle labelStyle;

	// Labels and Buttons
	enum primaryStatsLabels {Level, Class, Strength, Dexterity, Endurance, Health, Energy};
	enum secondaryStatsLabels {Armor, FireResistance, ColdResistance, LightningtResistance, Accuracy,
								AttackSpeed, CastSpeed, Cdr, CriticalChance, CriticalDamage, AttackPower, 
								SpellCriticalChance, SpellCriticalDamage, SpellPower, Health, MaxHealth, 
								Energy, MaxEnergy, HealthRegen, EnergyRegen};
	enum primaryStatsButtons {Details};

	// Containers for Primary and Secondary Stats' tooltips.
	Hashtable primaryStatsTooltip;
	Hashtable secondaryStatsTooltip;

	// Lengths of the all enums.
	int primaryLength   = System.Enum.GetValues (typeof(primaryStatsLabels)).Length;
	int secondaryLength = System.Enum.GetValues (typeof(secondaryStatsLabels)).Length;
	int primaryButtonsLength   = System.Enum.GetValues (typeof(primaryStatsButtons)).Length;
	
	// Arrays with the names of all enums.
	string[] namesPrimary   = System.Enum.GetNames(typeof(primaryStatsLabels));
	string[] namesSecondary = System.Enum.GetNames(typeof(secondaryStatsLabels));
	string[] namesPrimaryButtons   = System.Enum.GetNames(typeof(primaryStatsButtons));

	void Start ()
	{
		/* Setting up the tool-tips for Primary and Secondary Stats. */

		// Primary Stats tool-tip information.
		primaryStatsTooltip   = new Hashtable();

		primaryStatsTooltip.Add (primaryStatsLabels.Level, "Level is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Class, "Class is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Strength, "Strength is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Dexterity, "Dexterity is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Endurance, "Endurance is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Health, "Health is ...");
		primaryStatsTooltip.Add (primaryStatsLabels.Energy, "Energy is ...");

		// Secondary/Detailed Stats tool-tip information.
		secondaryStatsTooltip = new Hashtable();

		secondaryStatsTooltip.Add (secondaryStatsLabels.Armor, "Armor is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.FireResistance, "FireResistance is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.ColdResistance, "ColdResistance is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.LightningtResistance, "LightningtResistance is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.Accuracy, "Accuracy is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.AttackSpeed, "AttackSpeed is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.CastSpeed, "CastSpeed is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.Cdr, "Cdr is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.CriticalChance, "CriticalChance is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.CriticalDamage, "CriticalDamage is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.AttackPower, "AttackPower is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.SpellCriticalChance, "SpellCriticalChance is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.SpellCriticalDamage, "SpellCriticalDamage is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.SpellPower, "SpellPower is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.Health, "Health is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.MaxHealth, "MaxHealth is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.Energy, "Energy is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.MaxEnergy, "MaxEnergy is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.HealthRegen, "HealthRegen is ...");
		secondaryStatsTooltip.Add (secondaryStatsLabels.EnergyRegen, "EnergyRegen is ...");
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
	
	// Returning the height of the box that contains the Primary Stats' Labels and Buttons.
	private int getBoxHeight (int numberLabels, int numberButtons)
	{
		int preferableBoxHeight = (numberLabels * LABEL_HEIGHT) + ((numberLabels + 1) * SPACE_BETWEEN) + (numberButtons * (BUTTON_HEIGHT + SPACE_BETWEEN));

		return preferableBoxHeight;
	}
	
	void OnGUI()
	{
		// Screen percentage: 50% of the screen.
		int screenWidth50  = Screen.width / 2;
		int screenHeight50 = Screen.height / 2;
		
		// Box position.
		var boxWidthPosition  = screenWidth50 - BOX_WIDTH / 2;
		var boxHeightPosition = screenHeight50 - getBoxHeight(primaryLength, primaryButtonsLength) / 2;

		if(isStatsPressed)
		{
			// Drawing the Box with Primary Stats.
			GUI.Box(new Rect(boxWidthPosition, boxHeightPosition, BOX_WIDTH, getBoxHeight(primaryLength, primaryButtonsLength)), /*"Primary Stats"*/"");

			int i;
			// Drawing the Lables.
			for (i = 0; i < primaryLength; i++)
			{
				GUI.Label(new Rect (boxWidthPosition + SPACE_BETWEEN, boxHeightPosition + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), new GUIContent(namesPrimary[i], (string)primaryStatsTooltip[(primaryStatsLabels)System.Enum.Parse(typeof(primaryStatsLabels), namesPrimary[i])])/*, labelStyle*/);
			}

			int j;
			// Drawing the Buttons.
			for (j = 0; j < primaryButtonsLength; j++)
			{
				if (GUI.Button(new Rect (boxWidthPosition + BOX_WIDTH - BUTTON_WIDTH - SPACE_BETWEEN, boxHeightPosition + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), BUTTON_WIDTH, BUTTON_HEIGHT), namesPrimaryButtons[j]))
				{
					// Details
					if (string.Equals(namesPrimaryButtons[j], System.Enum.GetName(typeof(primaryStatsButtons), primaryStatsButtons.Details)))
					{
						// Toggling (hiding/unhiding) the secondary stats.
						if (isSecondaryStatsPressed == true)
						{
							isSecondaryStatsPressed = false;
						}
						else
						{
							isSecondaryStatsPressed = true;
						}
					}
				}
			}

			if (isSecondaryStatsPressed)
			{
				// Drawing the Box with Secondary Stats.
				GUI.Box(new Rect(boxWidthPosition - BOX_WIDTH, boxHeightPosition, BOX_WIDTH, getBoxHeight(primaryLength, primaryButtonsLength)), /*"Secondary Stats"*/"");

				// Drawing the Scroll-bar and Labels.
				scrollPosition = GUI.BeginScrollView(new Rect(boxWidthPosition - BOX_WIDTH, boxHeightPosition, BOX_WIDTH, getBoxHeight(primaryLength, primaryButtonsLength)), scrollPosition, new Rect(0, 0, BOX_WIDTH - 20, getBoxHeight(secondaryLength, 0)));

				int k;
				// Drawing the Secondary Lables.
				for (k = 0; k < secondaryLength; k++)
				{
					GUI.Label(new Rect (SPACE_BETWEEN, (k * LABEL_HEIGHT) + ((k + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), new GUIContent(namesSecondary[k], (string)secondaryStatsTooltip[(secondaryStatsLabels)System.Enum.Parse(typeof(secondaryStatsLabels), namesSecondary[k])])/*, labelStyle*/);
				}
				
				GUI.EndScrollView();
			}

			// Displaying the tooltip.
			displayTooltip();
		}
	}

	private void displayTooltip()
	{
		float x = Event.current.mousePosition.x;
		float y = Event.current.mousePosition.y; 
		
		if (GUI.tooltip != "")
			GUI.Box(new Rect(x - SPACE_BETWEEN, y - 2 * SPACE_BETWEEN, LABEL_WIDTH * 2, LABEL_HEIGHT * 2), "");
		
		GUI.Label(new Rect(x, y - 2 * SPACE_BETWEEN, LABEL_WIDTH * 2, LABEL_HEIGHT * 2), GUI.tooltip);
	}
}