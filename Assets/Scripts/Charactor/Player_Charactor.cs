using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{

	// timer to control internal behavior
	public int gameSetting ;

	private GUIStyle myGUIStyle = new GUIStyle();
	public Font[] fontCollection = new Font[3];
	public Font newFont ;

	private bool levelLoaded;
	private string systemState;
	private List<LevelScript_Base> levels = new List<LevelScript_Base>();

	public Hangar_Base hangar;
	protected int hangarCapacity;
	public int shipChoise;

	public int credits;

	public ProfileSavenLoad profileMan;

	// textures for the interface:
	protected Texture[] gameButtonTexture = new Texture[3];
	public Texture buttonTexture;

	protected Color[] gameTextColors = new Color[3];
	public Color textColor;

	public void OnApplicationQuit(){
		profileMan.gameSave();
	}
	public void Start () 
	{
		gameSetting = 1;

		profileMan = gameObject.AddComponent("ProfileSavenLoad") as ProfileSavenLoad;
		shipChoise = 0;
		systemState = "Menu";
		levelLoaded = false;
		hangar = gameObject.AddComponent("Hangar_Base") as Hangar_Base;
		setLevels();

		/*
		if(profileMan.filePresent()){
			profileMan.gameLoad();
			Debug.Log("Load");
		}else{
			hangar.addGunToHangar("projectileCanon");
			hangar.addToCanonUpgrades();
			hangar.addSpaceshipToHangar("TurdClass");
			hangar.addToShipUpgrades();
			credits = 0;
			Debug.Log("noLoad");
		}*/

		hangar.addSpaceshipToHangar("PlayerShips/NeedlePlayer");
		hangar.addSpaceshipToHangar("PlayerShips/MustangPlayer");
		hangar.addSpaceshipToHangar("PlayerShips/SpikePlayer");
		hangar.addToShipUpgrades();
		hangar.addGunToHangar("Weapons/MiniGun");
		hangar.addToCanonUpgrades();
		hangar.addToShipUpgrades();
		credits = 1000000;




		hangar.setHangar();

		// finds the textures in the resources folder
		gameButtonTexture[0] = Resources.Load("Interface/Button_Vesterbro") as Texture;
		gameButtonTexture[1] = Resources.Load("Interface/Button_Space_3") as Texture;
		gameButtonTexture[2] = Resources.Load("Interface/GUI") as Texture;


		gameTextColors[0] = new Color(1.0f,1f,1f,1.0f);
		//gameTextColors[1] = new Color(1.0f,0.2f,0.2f,1.0f);
		gameTextColors[1] = new Color(1.0f,1f,1f,1.0f);
		gameTextColors[2] = new Color(0.0f,1f,1f,1.0f);

		buttonTexture = gameButtonTexture[gameSetting];
		newFont = fontCollection[gameSetting];
		textColor = gameTextColors[gameSetting];

		// sets the font style for the price, which varies
		//myGUIStyle.normal.textColor =  new Color(0F, 1F, 3F, 0.6F);
		myGUIStyle.font = newFont;
		myGUIStyle.normal.textColor = textColor;// Color.white;
		myGUIStyle.alignment = TextAnchor.MiddleCenter;
	}

	public  void Update () 
	{
		if (Input.GetKeyDown("space")){
			gameSetting++;
			if(gameSetting > 2){
				gameSetting = 0;
			}
		}

		buttonTexture = gameButtonTexture[gameSetting];
		newFont = fontCollection[gameSetting];
		textColor = gameTextColors[gameSetting];
		myGUIStyle.font = newFont;
		myGUIStyle.normal.textColor = textColor;

		if(systemState == "Menu"){

		}
	


		if(systemState == "Hangar"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[0].loadLevel();
			}else{
				levels[0].updateLevel();
			}
		}

		if(systemState == "MissionLevel"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[1].loadLevel();
			}else{
				levels[1].updateLevel();
			}
		}

		if(systemState == "CanonShop"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[2].loadLevel();
			}else{
				levels[2].updateLevel();
				
			}
		}

		if(systemState == "ShipShop"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[2].loadLevel();
			}else{
				levels[3].updateLevel();
				
			}
		}
	}

	public void OnGUI()
	{	
		int buttonHeight = Screen.height/5 , buttonWidth = Screen.width/3, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;

		if(systemState == "Menu"){
			placementX = Screen.width/2 - buttonWidth/2; 
			placementY = (Screen.height/5)/2 ;
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "Hangar";
				levelLoaded = false;
				levels[0].loadLevel();

			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Hangar", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "MissionLevel";
				levelLoaded = false;
				levels[1].loadLevel();

			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Missions", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "CanonShop";
				levelLoaded = false;
				levels[2].loadLevel();

			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Canon Shop", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "ShipShop";
				levelLoaded = false;
				levels[3].loadLevel();
				
			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Ship Shop", myGUIStyle);
			GUI.EndGroup();
		}
		else if(systemState == "Hangar"){
			if(levels[0].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[0].levelGUI();
			}
		}
		else if(systemState == "MissionLevel"){
			if(levels[1].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else {
				levels[1].levelGUI();
			}
		}
		else if(systemState == "CanonShop"){
			if(levels[2].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[2].levelGUI();
			}
		}
		else if(systemState == "ShipShop"){

			if(levels[3].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[3].levelGUI();
			}

		}

	}



	private void setLevels(){
		Hangar_Level newHangarLevel = gameObject.AddComponent("Hangar_Level") as Hangar_Level;
		levels.Add (newHangarLevel);
		Mission_Menu_Level newMissionLevel = gameObject.AddComponent("Mission_Menu_Level") as Mission_Menu_Level;
		levels.Add(newMissionLevel);
		CanonShop_Level newCanonLevel = gameObject.AddComponent("CanonShop_Level") as CanonShop_Level;
		levels.Add(newCanonLevel);
		SpaceshipShop_Level newShipLevel = gameObject.AddComponent("SpaceshipShop_Level") as SpaceshipShop_Level;
		levels.Add(newShipLevel);
	}
	public string returnContentString(){
		string reportString = "";
		reportString +=  "Credit=" + credits + "\n";
		return reportString;
	}

}

