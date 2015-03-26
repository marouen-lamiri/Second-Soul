using UnityEngine;
using System.Collections;

public class DisplayPlayerStatsNew : MonoBehaviour, ISorcererSubscriber {
	
	// Constants
	public const int BOX_WIDTH     = 200;
	public const int LABEL_WIDTH   = 150;
	public const int BUTTON_WIDTH  = 70;
	public const int LABEL_HEIGHT  = 20;
	public const int BUTTON_HEIGHT = 20;
	public const int SPACE_BETWEEN = 5;
	
	// Instances refering to the objects.
	private Fighter fighter;
	private Sorcerer sorcerer;
	
	// Variable related to the scroll-bar.
	public Vector2 scrollPosition = Vector2.zero;
	
	// Variable responsible for hiding/unhiding the menu.
	bool isStatsPressed;
	bool isSecondaryStatsPressed;
	
	// Label style.
	public GUIStyle labelStyle;
	
	// Labels and Buttons
	enum primaryStatsLabelsFighter {Level, Class, Strength, Dexterity, Endurance, Health, Energy};
	enum primaryStatsLabelsSorcerer {Level, Class, Intelligence, Wisdom, Spirit, Health, Energy};
	
	// Primary Stats tooltips for both Fighter and Sorcerer.
	public string tooltip_Level  = "Level tooltip ...";
	public string tooltip_Class  = "Class tooltip ...";
	public string tooltip_Health = "Health tooltip ...";
	public string tooltip_Energy = "Energy tooltip ...";
	// Fighter's Primary Stats tooltips.
	public string tooltip_Strength  = "Strength tooltip ...";
	public string tooltip_Dexterity = "Dexterity tooltip ...";
	public string tooltip_Endurance = "Endurance tooltip ...";
	// Sorcerer's Primary Stats tooltips.
	public string tooltip_Intelligence = "Intelligence tooltip ...";
	public string tooltip_Wisdom       = "Wisdom tooltip ...";
	public string tooltip_Spirit       = "Spirit tooltip ...";
	
	enum secondaryStatsLabelsFighter {Armor, FireResistance, ColdResistance, LightningtResistance, Accuracy, AttackSpeed, 
		Cdr, CriticalChance, CriticalDamage, AttackPower, HealthRegen, EnergyRegen};
	enum secondaryStatsLabelsSorcerer {Accuracy, CastSpeed, SpellCriticalChance, SpellCriticalDamage, SpellPower};
	
	// Secondary Stats tooltips for both Fighter and Sorcerer.
	public string tooltip_Armor                = "Armor tooltip ...";
	public string tooltip_FireResistance       = "FireResistance tooltip ...";
	public string tooltip_ColdResistance       = "ColdResistance tooltip ...";
	public string tooltip_LightningtResistance = "LightningtResistance tooltip ...";
	public string tooltip_Accuracy             = "Accuracy tooltip ...";
	public string tooltip_AttackSpeed          = "AttackSpeed tooltip ...";
	public string tooltip_CastSpeed            = "CastSpeed tooltip ...";
	public string tooltip_Cdr                  = "Cdr tooltip ...";
	public string tooltip_CriticalChance       = "CriticalChance tooltip ...";
	public string tooltip_CriticalDamage       = "CriticalDamage tooltip ...";
	public string tooltip_AttackPower          = "AttackPower tooltip ...";
	public string tooltip_SpellCriticalChance  = "SpellCriticalChance tooltip ...";
	public string tooltip_SpellCriticalDamage  = "SpellCriticalDamage tooltip ...";
	public string tooltip_SpellPower           = "SpellPower tooltip ...";
	public string tooltip_HealthRegen          = "HealthRegen tooltip ...";
	public string tooltip_EnergyRegen          = "EnergyRegen tooltip ...";
	
	enum primaryStatsButtons {Details};
	
	// Containers for Primary and Secondary Stats' tooltips.
	Hashtable primaryStatsTooltip;
	Hashtable secondaryStatsTooltip;
	
	// Containers for Primary and Secondary Stats' values.
	string[] primaryStatsValues;
	string[] secondaryStatsValues;
	
	// Arrays with the names of all enums.
	string[] namesPrimary;
	string[] namesSecondary;
	string[] namesPrimaryButtons = System.Enum.GetNames(typeof(primaryStatsButtons));
	
	void Start ()
	{
		subscribeToSorcererInstancePublisher (); // jump into game

		// Getting the reference of Fighter and Sorcerer.
		fighter  = (Fighter) GameObject.FindObjectOfType (typeof (Fighter));
		sorcerer = (Sorcerer)SorcererInstanceManager.getSorcerer (); //sorcerer = (Sorcerer)GameObject.FindObjectOfType (typeof (Sorcerer));
		
		// TOOLTIPS
		// Primary and Secondary Stats tool-tip initialization.
		primaryStatsTooltip   = new Hashtable ();
		secondaryStatsTooltip = new Hashtable ();

		// Initializing the stats for Fighter and Sorcerer.
		if (fighter.playerEnabled)
		{
			// Is Fighter.
			namesPrimary = System.Enum.GetNames(typeof(primaryStatsLabelsFighter));
			primaryStatsTooltip.Add (namesPrimary [0], tooltip_Level);
			primaryStatsTooltip.Add (namesPrimary [1], tooltip_Class);
			primaryStatsTooltip.Add (namesPrimary [2], tooltip_Strength);
			primaryStatsTooltip.Add (namesPrimary [3], tooltip_Dexterity);
			primaryStatsTooltip.Add (namesPrimary [4], tooltip_Endurance);
			primaryStatsTooltip.Add (namesPrimary [5], tooltip_Health);
			primaryStatsTooltip.Add (namesPrimary [6], tooltip_Energy);
			
			primaryStatsValues = new string[namesPrimary.Length];

			namesSecondary = System.Enum.GetNames(typeof(secondaryStatsLabelsFighter));
			secondaryStatsTooltip.Add (namesSecondary [0], tooltip_Armor);
			secondaryStatsTooltip.Add (namesSecondary [1], tooltip_FireResistance);
			secondaryStatsTooltip.Add (namesSecondary [2], tooltip_ColdResistance);
			secondaryStatsTooltip.Add (namesSecondary [3], tooltip_LightningtResistance);
			secondaryStatsTooltip.Add (namesSecondary [4], tooltip_Accuracy);
			secondaryStatsTooltip.Add (namesSecondary [5], tooltip_AttackSpeed);
			secondaryStatsTooltip.Add (namesSecondary [6], tooltip_Cdr);
			secondaryStatsTooltip.Add (namesSecondary [7], tooltip_CriticalChance);
			secondaryStatsTooltip.Add (namesSecondary [8], tooltip_CriticalDamage);
			secondaryStatsTooltip.Add (namesSecondary [9], tooltip_AttackPower);
			secondaryStatsTooltip.Add (namesSecondary [10], tooltip_HealthRegen);
			secondaryStatsTooltip.Add (namesSecondary [11], tooltip_EnergyRegen);
			
			secondaryStatsValues = new string[namesSecondary.Length];
		}
		else
		{
			// Is Sorcerer.
			namesPrimary   = System.Enum.GetNames(typeof(primaryStatsLabelsSorcerer));
			primaryStatsTooltip.Add (namesPrimary [0], tooltip_Level);
			primaryStatsTooltip.Add (namesPrimary [1], tooltip_Class);
			primaryStatsTooltip.Add (namesPrimary [2], tooltip_Intelligence);
			primaryStatsTooltip.Add (namesPrimary [3], tooltip_Wisdom);
			primaryStatsTooltip.Add (namesPrimary [4], tooltip_Spirit);
			primaryStatsTooltip.Add (namesPrimary [5], tooltip_Health);
			primaryStatsTooltip.Add (namesPrimary [6], tooltip_Energy);
			
			primaryStatsValues = new string[namesPrimary.Length];

			namesSecondary = System.Enum.GetNames(typeof(secondaryStatsLabelsSorcerer));
			secondaryStatsTooltip.Add (namesSecondary [0], tooltip_Accuracy);
			secondaryStatsTooltip.Add (namesSecondary [1], tooltip_CastSpeed);
			secondaryStatsTooltip.Add (namesSecondary [2], tooltip_SpellCriticalChance);
			secondaryStatsTooltip.Add (namesSecondary [3], tooltip_SpellCriticalDamage);
			secondaryStatsTooltip.Add (namesSecondary [4], tooltip_SpellPower);
			
			secondaryStatsValues = new string[namesSecondary.Length];
		}
	}
	
	// Update is called once per frame, checks if 's'-key is pressed.
	void Update ()
	{
		// Catches the 's'-key press.
		if(Input.GetKeyDown("s"))
		{
			PlayerStats();
		}

		/* Setting up the tool-tips for Primary and Secondary Stats. */
		if (fighter.playerEnabled)
		{
			// Is Fighter.
			primaryStatsValues [0] = namesPrimary [0] + ": " + fighter.level;
			primaryStatsValues [1] = namesPrimary [1] + ": " + fighter.GetType ();
			primaryStatsValues [2] = namesPrimary [2] + ": " + fighter.getStrength ();
			primaryStatsValues [3] = namesPrimary [3] + ": " + fighter.getDexterity ();
			primaryStatsValues [4] = namesPrimary [4] + ": " + fighter.getEndurance ();
			primaryStatsValues [5] = namesPrimary [5] + ": " + (int)fighter.health + " / " + fighter.maxHealth;
			primaryStatsValues [6] = namesPrimary [6] + ": " + (int)fighter.energy + " / " + fighter.maxEnergy;
			
			secondaryStatsValues [0] = namesSecondary [0] + ": " + fighter.armor;
			secondaryStatsValues [1] = namesSecondary [1] + ": " + fighter.fireResistance;
			secondaryStatsValues [2] = namesSecondary [2] + ": " + fighter.coldResistance;
			secondaryStatsValues [3] = namesSecondary [3] + ": " + fighter.lightningtResistance;
			secondaryStatsValues [4] = namesSecondary [4] + ": " + fighter.accuracy;
			secondaryStatsValues [5] = namesSecondary [5] + ": " + fighter.attackSpeed;
			secondaryStatsValues [6] = namesSecondary [6] + ": " + fighter.cdr;
			secondaryStatsValues [7] = namesSecondary [7] + ": " + fighter.criticalChance;
			secondaryStatsValues [8] = namesSecondary [8] + ": " + fighter.criticalDamage;
			secondaryStatsValues [9] = namesSecondary [9] + ": " + fighter.attackPower;
			secondaryStatsValues [10] = namesSecondary [10] + ": " + fighter.healthRegen;
			secondaryStatsValues [11] = namesSecondary [11] + ": " + fighter.energyRegen;
		}
		else
		{
			// Is Sorcerer.
			primaryStatsValues [0] = namesPrimary [0] + ": " + sorcerer.level;
			primaryStatsValues [1] = namesPrimary [1] + ": " + sorcerer.GetType ();
			primaryStatsValues [2] = namesPrimary [2] + ": " + sorcerer.getIntelligence ();
			primaryStatsValues [3] = namesPrimary [3] + ": " + sorcerer.getWisdom ();
			primaryStatsValues [4] = namesPrimary [4] + ": " + sorcerer.getSpirit ();
			primaryStatsValues [5] = namesPrimary [5] + ": " + (int)sorcerer.health + " / " + sorcerer.maxHealth;
			primaryStatsValues [6] = namesPrimary [6] + ": " + (int)sorcerer.energy + " / " + sorcerer.maxEnergy;
			
			secondaryStatsValues [0] = namesSecondary [0] + ": " + sorcerer.accuracy;
			secondaryStatsValues [1] = namesSecondary [1] + ": " + sorcerer.castSpeed;
			secondaryStatsValues [2] = namesSecondary [2] + ": " + sorcerer.spellCriticalChance;
			secondaryStatsValues [3] = namesSecondary [3] + ": " + sorcerer.spellCriticalDamage;
			secondaryStatsValues [4] = namesSecondary [4] + ": " + sorcerer.spellPower;
		}
	}
	
	public void PlayerStats ()
	{
		// Toggling (hiding/unhiding) the primary stats.
		if (isStatsPressed == true)
		{
			isStatsPressed = false;
			isSecondaryStatsPressed = false;
		}
		else
		{
			isStatsPressed = true;
		}
	}
	
	// Returning the height of the box that contains the Primary Stats' Labels and Buttons.
	private int getBoxHeight (int numberLabels, int numberButtons)
	{
		int preferableBoxHeight = ((numberLabels + 1) * LABEL_HEIGHT) + ((numberLabels + 1) * SPACE_BETWEEN) + (numberButtons * (BUTTON_HEIGHT + SPACE_BETWEEN));
		
		return preferableBoxHeight;
	}
	
	void OnGUI()
	{
		// Screen percentage: 50% of the screen.
		int screenWidth50  = Screen.width / 2;
		int screenHeight50 = Screen.height / 2;
		
		// Box position.
		var boxWidthPosition  = screenWidth50 - BOX_WIDTH / 2;
		var boxHeightPosition = screenHeight50 - getBoxHeight(namesPrimary.Length, namesPrimaryButtons.Length) / 2;
		var boxHeightPositionLabel = boxHeightPosition + LABEL_HEIGHT;
		
		int leftShift   = 5;
		int bottomShift = 2;
		
		if(isStatsPressed)
		{
			// Drawing the Box with Primary Stats.
			GUI.Box(new Rect(boxWidthPosition, boxHeightPosition, BOX_WIDTH, getBoxHeight(namesPrimary.Length, namesPrimaryButtons.Length)), /*"Primary Stats"*/"");
			
			int i;
			// Drawing the Lables.
			for (i = 0; i < namesPrimary.Length; i++)
			{
				GUI.Label(new Rect (boxWidthPosition + SPACE_BETWEEN * leftShift, boxHeightPositionLabel + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), 
				          new GUIContent(primaryStatsValues[i], (string)primaryStatsTooltip[namesPrimary[i]])/*, labelStyle*/);
			}
			
			int j;
			// Drawing the Buttons.
			for (j = 0; j < namesPrimaryButtons.Length; j++)
			{
				if (GUI.Button(new Rect (boxWidthPosition + BOX_WIDTH - BUTTON_WIDTH - SPACE_BETWEEN, boxHeightPositionLabel + (i * LABEL_HEIGHT) + ((i + 1) * SPACE_BETWEEN), BUTTON_WIDTH, BUTTON_HEIGHT), namesPrimaryButtons[j]))
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
				GUI.Box(new Rect(boxWidthPosition - BOX_WIDTH, boxHeightPosition, BOX_WIDTH, getBoxHeight(namesPrimary.Length, namesPrimaryButtons.Length)), /*"Secondary Stats"*/"");
				
				// Drawing the Scroll-bar and Labels.
				scrollPosition = GUI.BeginScrollView(new Rect(boxWidthPosition - BOX_WIDTH, boxHeightPositionLabel, BOX_WIDTH, getBoxHeight(namesPrimary.Length, namesPrimaryButtons.Length) - bottomShift * LABEL_HEIGHT), scrollPosition, new Rect(0, 0, BOX_WIDTH - 20, getBoxHeight(namesSecondary.Length, 0)));
				
				int k;
				// Drawing the Secondary Lables.
				for (k = 0; k < namesSecondary.Length; k++)
				{
					GUI.Label(new Rect (SPACE_BETWEEN * leftShift, (k * LABEL_HEIGHT) + ((k + 1) * SPACE_BETWEEN), LABEL_WIDTH, LABEL_HEIGHT), 
					          new GUIContent(secondaryStatsValues[k], (string)secondaryStatsTooltip[namesSecondary[k]])/*, labelStyle*/);
				}
				
				GUI.EndScrollView();
			}
			
			// Displaying the tooltip.
			displayTooltip();
		}
	}
	
	private void displayTooltip()
	{
		int leftShift   = 5;
		int topShift    = 2;
		int resizeRatio = 2;
		
		float x = Event.current.mousePosition.x;
		float y = Event.current.mousePosition.y; 
		
		if (GUI.tooltip != "")
			GUI.Box(new Rect(x - SPACE_BETWEEN, y - (topShift * SPACE_BETWEEN), resizeRatio * LABEL_WIDTH, resizeRatio * LABEL_HEIGHT), "");
		
		GUI.Label(new Rect(x + (leftShift * SPACE_BETWEEN), y - (topShift * SPACE_BETWEEN), resizeRatio * LABEL_WIDTH, resizeRatio * LABEL_HEIGHT), GUI.tooltip);
	}

	// ------- for jump into game: ----------
	public void updateMySorcerer(Sorcerer newSorcerer) {
		this.sorcerer = newSorcerer;
	}

	public void subscribeToSorcererInstancePublisher() {
		SorcererInstanceManager.subscribe (this);
	}

}