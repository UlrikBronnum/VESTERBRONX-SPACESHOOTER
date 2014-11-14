using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{

	// timer to control internal behavior
	public int gameSetting ;
	public int levelsCompleted;

	private GUIStyle myGUIStyle = new GUIStyle();
	public Font[] fontCollection = new Font[3];
	public Font newFont ;
	private bool targetNotFound = true;
	private bool levelLoaded;
	private string systemState;
	private List<LevelScript_Base> levels = new List<LevelScript_Base>();

	public Hangar_Base hangar;
	protected int hangarCapacity;
	public int shipChoise;

	public int credits;
	public ProfileSavenLoad profileMan;

	// textures for the interface:
	protected Texture[] gameButtonTexture = new Texture[2];
	public Texture buttonTexture;

	public string[] enemyVersion = new string[3];
	public string[] playerVersion = new string[3];
	public string[] playerArmory;


	protected Color[] gameTextColors = new Color[2];
	public Color textColor;

	private float terminationTime = 0.0f;
	public string startTime;
	public string endTime;
	public GUIStyle EmptyGuiStyle;

	public void OnApplicationQuit(){
		profileMan.gameSave();
		endTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		Debug.Log(endTime);
	}
	public void Start () 
	{
		gameSetting = 1;
		levelsCompleted = 0;
		
		gameButtonTexture[0] = Resources.Load("Interface/Button_Vesterbro_3_down") as Texture;
		gameButtonTexture[1] = Resources.Load("Interface/GUI") as Texture;
		gameTextColors[0] = new Color(0.0f,0.0f,0.0f,1.0f);
		gameTextColors[1] = new Color(0.0f,1f,1f,1.0f);

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
			hangar.addGunToHangar("Weapons/MiniGun");
			hangar.addToCanonUpgrades();
			hangar.addSpaceshipToHangar("PlayerShips/MustangPlayer");
			hangar.addToShipUpgrades();
			credits = 0;
			Debug.Log("noLoad");
		}*/
		hangar.addSpaceshipToHangar("SecondClass");
		hangar.addGunToHangar("Space/Minigun_weapon");
		hangar.addToCanonUpgrades();
		hangar.addSpaceshipToHangar("PlayerShips/FixeBus");
		hangar.addToShipUpgrades();
		credits = 70000;

		hangar.setHangar();


		myGUIStyle.alignment = TextAnchor.MiddleCenter;
		setGameVersion();
	
	}
	private void setGameVersion(){
		buttonTexture = gameButtonTexture[gameSetting];
		newFont = fontCollection[gameSetting];
		textColor = gameTextColors[gameSetting];
		myGUIStyle.font = newFont;
		myGUIStyle.normal.textColor = textColor;
	
		if(gameSetting == 0)
		{
			playerVersion = new string[3] {"PlayerShips/CargoBike","PlayerShips/FixeBus","PlayerShips/MustangPlayer"};
			enemyVersion = new string[5] {"VesterBro/Christiania_bike","VesterBro/Christiania_bike","VesterBro/CycleMonster","VesterBro/Christiania_bike","VesterBro/Spike"};
			playerArmory  = new string[6] {"VesterBro/Coffee gun_weapon","VesterBro/Durum launcher_weapon","VesterBro/Needle gun_weapon","VesterBro/Bottle Launcher_weapon","VesterBro/MeatCleaver gun_weapon","VesterBro/Cannon_weapon"};
		}else{
			playerVersion = new string[3] {"PlayerShips/SpikePlayer","PlayerShips/Spaceship_1ed","PlayerShips/MustangPlayer"};
			enemyVersion = new string[5] {"Space/Mustang","Space/Needle","Space/Spike","Space/X_Fighter","Space/Phoenix"};
			playerArmory  = new string[6] {"Space/Minigun_weapon","Space/Cobra Missile _Weapon","Space/Rocket launcher_weapon","Space/Plasma Missile_weapon","Space/Plasma gun_weapon","Space/Laser_weapon"};
		} 
	}
	public  void Update () 
	{
		if (Input.GetKeyDown("space")){
			gameSetting++;
			if(gameSetting > 1){
				gameSetting = 0;
			}
			setGameVersion();
		}
		if(systemState == "No State"){
			if(GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().isFound){
				Debug.Log("Found Target");
				systemState = "Menu";
			}else{
				Debug.Log("No Target");
				systemState = "Image Lost";
				terminationTime = Time.time + 5;
				Debug.Log(terminationTime);
			}
		}

		if(systemState == "Image Lost")
		{
			if(GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().isFound){
				Debug.Log("Found Target");
				systemState = "Menu";
			}else if(terminationTime < Time.time){
				Debug.Log ("Close");
			}
		}
			

		if(systemState == "Hangar"){
			if(levelLoaded == false){
				levelLoaded = true;
			}else{
				levels[0].updateLevel();
			}
		}

		if(systemState == "MissionLevel"){
			if(levelLoaded == false){
				levelLoaded = true;
			}else{
				levels[1].updateLevel();
			}
		}

		if(systemState == "CanonShop"){
			if(levelLoaded == false){
				levelLoaded = true;
			}else{
				levels[2].updateLevel();
				
			}
		}

		if(systemState == "ShipShop"){
			if(levelLoaded == false){
				levelLoaded = true;
			}else{
				levels[3].updateLevel();
				
			}
		}
	}

	public void OnGUI()
	{	
		int buttonHeight = Screen.height/5 , buttonWidth = Screen.width/3, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;
		myGUIStyle.alignment = TextAnchor.MiddleCenter;

		if(systemState == "Menu")
		{	
			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			placementX = Screen.width/2 - buttonWidth/2; 
			placementY = (Screen.height/5)/2 ;
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
			{
				systemState = "Hangar";
				levelLoaded = false;
				levels[0].loadLevel();

			}
			scaleFont = buttonWidth/10;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "Hangar", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
			{
				systemState = "MissionLevel";
				levelLoaded = false;
				levels[1].loadLevel();

			}
			scaleFont = buttonWidth/10;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "Missions", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "CanonShop";
				levelLoaded = false;
				levels[2].loadLevel();

			}
			scaleFont = buttonWidth/10;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "Cannon Shop", myGUIStyle);
			GUI.EndGroup();

			placementY += buttonHeight;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
			{
				systemState = "ShipShop";
				levelLoaded = false;
				levels[3].loadLevel();
				
			}
			scaleFont = buttonWidth/10;
			myGUIStyle.fontSize = scaleFont;
			//-scaleFont/2 instead of second parameter:
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "Ship Shop", myGUIStyle);
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

		}else{
			placementX = (int)(Screen.width/10 * 0.5f); 
			placementY = (int)(Screen.height/10 * 0.5f);
			buttonHeight = (int)Screen.height/10 * 9; 
			buttonWidth = (int)Screen.width/10 * 9;

			GUI.BeginGroup(new Rect(placementX,placementY,Screen.width/10 * 9,Screen.height/10 * 9));
			GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),Resources.Load("Interface/imagetargetpositiontex") as Texture);
			scaleFont = 35;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "No Image Target", myGUIStyle);
			GUI.EndGroup();
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
		reportString +=  "GameVersion=" + gameSetting + "\n";
		reportString +=  "LevelCompleted=" + levelsCompleted + "\n";
		reportString +=  "Credit=" + credits + "\n";
		return reportString;
	}

}

