using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{
	//box and color used for info boxes
	private Color infoColor = Color.grey;
	private Texture2D box;
	
	
	// bool making sure each texture is shown untill a button is clicked
	private bool hasShown = false;
	private bool hasShown2 = false;
	private bool hasShown3 = false;
	private bool hasShown4 = false;
	private bool hasShown5 = false;
	private bool hasShown6 = false;
	private bool hasShown7 = false;

	private Texture swipeSym;
	
	
	private bool firstTime;
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
	
	public string[] enemyVersion ;
	public string[] playerVersion ;
	public string[] playerArmory;
	
	
	protected Color[] gameTextColors = new Color[2];
	public Color textColor;
	
	public int userDatabaseID;
	private float terminationTime = 0.0f;
	public string startTime;
	public string endTime;
	public GUIStyle EmptyGuiStyle;
	
	public StartUp databaseConnect; 
	private bool userCreated = false;
	
	
	public void OnApplicationQuit(){
		profileMan.gameSave();
		endTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		Debug.Log(endTime);
	}
	public void Start () 
	{
		print( Application.persistentDataPath );

		databaseConnect = gameObject.AddComponent("StartUp") as StartUp; 
		gameSetting = 0;
		levelsCompleted = 24;

		swipeSym = Resources.Load ("Interface/swipeSymbol") as Texture;
		gameButtonTexture[0] = Resources.Load("Interface/Button_Vesterbro_3_down") as Texture;
		gameButtonTexture[1] = Resources.Load("Interface/GUI") as Texture;
		gameTextColors[0] = new Color(0.0f,0.0f,0.0f,1.0f);
		gameTextColors[1] = new Color(0.0f,1f,1f,1.0f);
		
		
		profileMan = gameObject.AddComponent("ProfileSavenLoad") as ProfileSavenLoad;
		shipChoise = 0;
		systemState = "Menu";
		levelLoaded = false;
		hangar = gameObject.AddComponent("Hangar_Base") as Hangar_Base;
		hangar.run();
		
		setLevels();
		
		// box used for info:
		box = new Texture2D(1,1);
		
		myGUIStyle.alignment = TextAnchor.MiddleCenter;
		

		if(profileMan.filePresent()){
			userCreated = true;
			firstTime = false;
			profileMan.gameLoad();
			setGameVersion();
			databaseConnect.id2 = userDatabaseID.ToString();
			Debug.Log("Load");
			hangar.setHangar();
		}else{
			userCreated = false;
		}
	}
	private void setGameVersion(){
		
		buttonTexture = gameButtonTexture[gameSetting];
		newFont = fontCollection[gameSetting];
		textColor = gameTextColors[gameSetting];
		myGUIStyle.font = newFont;
		myGUIStyle.normal.textColor = textColor;
		
		if(gameSetting == 0)
		{
			playerVersion = new string[2] {"PlayerShips/CargoBike","PlayerShips/FixeBus"};
			//cagobike, carlsberg wagon, cristianiaBike 
			playerArmory  = new string[6] {"VesterBro/Coffee gun_weapon","VesterBro/Durum launcher_weapon","VesterBro/Needle gun_weapon","VesterBro/Bottle Launcher_weapon","VesterBro/MeatCleaver gun_weapon","VesterBro/Cannon_weapon"};
			
			enemyVersion = new string[4] {"VesterBro/CargoBike","VesterBro/Carlsberg_wagon","VesterBro/ChristaniaBike","VesterBro/Christiania_bike"};
			
		}else{
			playerVersion = new string[2] {"PlayerShips/Spaceship_1ed","PlayerShips/SpikePlayer"};
			enemyVersion = new string[4] {"Space/Mustang","Space/Needle","Space/X_Fighter","Space/Phoenix"};
			playerArmory  = new string[6] {"Space/Minigun_weapon","Space/Cobra Missile _Weapon","Space/Rocket launcher_weapon","Space/Plasma Missile_weapon","Space/Plasma gun_weapon","Space/Laser_weapon"};
		} 
	}
	
	
	
	public  void Update () 
	{
		
		if(userCreated){
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
					startTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
				profileMan.gameSave();
				if(GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().isFound){
					Debug.Log("Found Target");
					systemState = "Menu";
				}else if(terminationTime < Time.time){
					Debug.Log ("Close");
				}
			}
			
			if(systemState == "Menu"){
				if(!GameObject.Find("ImageTarget").GetComponent<DefaultTrackableEventHandler>().isFound){
					profileMan.gameSave();
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
		}else{
			
		}
	}
	
	public void OnGUI()
	{	
		
		if(userCreated){
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
				
				//myGUIStyle.alignment = TextAnchor.MiddleCenter;
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
				{
					systemState = "MissionLevel";
					levelLoaded = false;
					levels[1].loadLevel();
					
				}
				//scaleFont = buttonWidth/10;
				//myGUIStyle.fontSize = scaleFont;
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
				//scaleFont = buttonWidth/10;
				//myGUIStyle.fontSize = scaleFont;
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
				//scaleFont = buttonWidth/10;
				//myGUIStyle.fontSize = scaleFont;
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
			
			
			// makes info screens
			if(firstTime){
				myGUIStyle.fontSize = Screen.width/26;
				
				if(systemState == "Menu"){
					if(!hasShown){
						//	StartCoroutine (info((x) => hasShown = x));
						
						// sets the color for the info box:
						box.SetPixel(0,0, infoColor);
						box.Apply();
						GUI.skin.box.normal.background = box;
						
						//	GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6),  box)  ;
						//	GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6), " ", myGUIStyle);
					}
				}
				else if(systemState == "Hangar"){

					GUI.DrawTexture(new Rect(Screen.width/2-Screen.width/10,Screen.height-Screen.height/3, Screen.width/6,Screen.height/5), swipeSym, ScaleMode.ScaleToFit, true, 0);

					if(!hasShown2){
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6),  box)  ;
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6), "In the hangar you can choose"+ "\n" +"which weapons should be mounted"+ "\n" +"on your ship."+ "\n" +"You need to buy a new weapon"+ "\n" +"in the cannon store before you"+ "\n" +"can change your weapon", myGUIStyle);
						
						if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
							info(ref hasShown2);
						}
						GUI.Box (new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight), "Continue", myGUIStyle);
						
					}}
				else if(systemState == "MissionLevel"){

					GUI.DrawTexture(new Rect(Screen.width/2-Screen.width/10,Screen.height-Screen.height/3, Screen.width/6,Screen.height/5), swipeSym, ScaleMode.ScaleToFit, true, 0);

					if(!hasShown3){
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6),  box)  ;
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6), "Here you choose a level to play."+ "\n" +"There are 3 locations with 8 levels each."+ "\n" +"Use swipe to manage the levels,"+ "\n" +"and press the upper right button"+ "\n" +"to enter a location or play a level", myGUIStyle);
						
						if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
							info(ref hasShown3);
						}
						GUI.Box (new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight), "Continue", myGUIStyle);
					}}
				else if(systemState == "CanonShop"){
					GUI.DrawTexture(new Rect(Screen.width/2-Screen.width/10,Screen.height-Screen.height/3, Screen.width/6,Screen.height/5), swipeSym, ScaleMode.ScaleToFit, true, 0);

					if(!hasShown4){
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6),  box)  ;
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6), "In the Cannon Shop, you can "+ "\n" +"upgrade your weapons and buy new ones."+ "\n" +"Swipe to the right to see the next weapon.", myGUIStyle);
						
						if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
							info(ref hasShown4);
						}
						GUI.Box (new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight), "Continue", myGUIStyle);
					}}
				else if(systemState == "ShipShop"){
					GUI.DrawTexture(new Rect(Screen.width/2-Screen.width/10,Screen.height-Screen.height/3, Screen.width/6,Screen.height/5), swipeSym, ScaleMode.ScaleToFit, true, 0);

					if(!hasShown5){
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6),  box)  ;
						GUI.Box (new Rect(Screen.width/10,Screen.height/12, Screen.width-Screen.width/5,Screen.height-Screen.height/6), "In the Ship Shop, you can"+ "\n" +"upgrade your ship or buy a new."+ "\n" +"Use swipe to manouvre"+ "\n" + "between your ships", myGUIStyle);
						
						if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
							info(ref hasShown5);
						}
						GUI.Box (new Rect(Screen.width/2-buttonWidth/2,Screen.height-Screen.height/6,buttonWidth,buttonHeight), "Continue", myGUIStyle);
						//	StartCoroutine (info((x) => hasShown5 = x));
						//info1 = Resources.Load("Interface/InfoBoxShip") as Texture;
						//GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height), info1, ScaleMode.ScaleToFit, true, 0);
					}}
			}
		}else{
			databaseConnect.levelGUI();
			if(databaseConnect.userCreatedBool){
				gameSetting = databaseConnect.tal;
				string ne = databaseConnect.id2;
				userDatabaseID = int.Parse(databaseConnect.id2);
				setGameVersion();
				firstTime = true;
				hangar.addGunToHangar(playerArmory[0]);
				hangar.addToCanonUpgrades();
				hangar.addSpaceshipToHangar(playerVersion[0]);
				hangar.addToShipUpgrades();
				credits = 1000000;
				hangar.setHangar();
				Debug.Log("noLoad");
				userCreated = true;
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
		reportString +=  "GameVersion=" + gameSetting + "\n";
		reportString +=  "LevelCompleted=" + levelsCompleted + "\n";
		reportString +=  "Credit=" + credits + "\n";
		reportString +=  "DatabaseID=" + userDatabaseID + "\n";
		reportString +=  "ShipChoise=" + shipChoise + "\n";
		return reportString;
	}
	
	private void info( ref bool Shown){
		Shown = true;
	}
	
	/*IEnumerator info(System.Action<bool>Shown){
		yield return new WaitForSeconds (50);
		Shown (true);	
		//Shown = false;
		}*/
	
}

