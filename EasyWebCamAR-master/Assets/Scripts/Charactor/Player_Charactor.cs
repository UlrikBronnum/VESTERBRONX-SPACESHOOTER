using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{

	// timer to control internal behavior
	public char gameSetting;

	private GUIStyle myGUIStyle = new GUIStyle();


	private bool levelLoaded;
	private string systemState;
	private List<LevelScript_Base> levels = new List<LevelScript_Base>();

	public Hangar_Base hangar;
	protected int hangarCapacity;
	public int shipChoise;

	public int credits;

	public ProfileSavenLoad profileMan;

	// textures for the interface:
	public Texture hangarTex;
	public Texture missionTex;
	public Texture canonTex;
	public Texture shipTex;
	public Texture creditsTex;

	public void OnApplicationQuit(){
		profileMan.gameSave();
	}
	public void Start () 
	{
		profileMan = gameObject.AddComponent("ProfileSavenLoad") as ProfileSavenLoad;
		shipChoise = 0;
		systemState = "Menu";
		levelLoaded = false;
		hangar = gameObject.AddComponent("Hangar_Base") as Hangar_Base;
		setLevels();


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
		}

		hangar.setHangar();

		// finds the textures in the resources folder
		hangarTex = Resources.Load("Interface/MainMenu/Hangar button") as Texture;
		missionTex = Resources.Load("Interface/MainMenu/Mission Level button") as Texture;
		canonTex = Resources.Load("Interface/MainMenu/Cannon Shop Button") as Texture;
		shipTex = Resources.Load("Interface/MainMenu/Ship Shop button") as Texture;
		creditsTex = Resources.Load("Interface/MainMenu/Credits") as Texture;

	}

	public  void Update () 
	{

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

	public void OnGUI(){
		if(systemState == "Menu"){

			if(GUI.Button(new Rect(Screen.width/2-((Screen.width/3)/2),((Screen.height/5)/2),Screen.width/3,Screen.height/5),hangarTex, GUIStyle.none)){
				systemState = "Hangar";
				levelLoaded = false;
				levels[0].loadLevel();

			}
			if(GUI.Button(new Rect(Screen.width/2-((Screen.width/3)/2),((Screen.height/5)/2)+(Screen.height/5),Screen.width/3,Screen.height/5),missionTex,GUIStyle.none)){
				systemState = "MissionLevel";
				levelLoaded = false;
				levels[1].loadLevel();

			}
			if(GUI.Button(new Rect(Screen.width/2-((Screen.width/3)/2),((Screen.height/5)/2)+2*(Screen.height/5),Screen.width/3,Screen.height/5),canonTex,GUIStyle.none)){
				systemState = "CanonShop";
				levelLoaded = false;
				levels[2].loadLevel();

			}
			if(GUI.Button(new Rect(Screen.width/2-((Screen.width/3)/2),((Screen.height/5)/2)+3*(Screen.height/5),Screen.width/3,Screen.height/5),shipTex,GUIStyle.none)){
				systemState = "ShipShop";
				levelLoaded = false;
				levels[3].loadLevel();
				
			}
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
			myGUIStyle.normal.textColor =  new Color(0F, 1F, 3F, 0.6F);
			myGUIStyle.fontSize = 16;
			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Box (new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/7),  creditsTex, GUIStyle.none )  ;
			GUI.Box (new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/7), credits.ToString(), myGUIStyle )  ;

			if(levels[2].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[2].levelGUI();
			}
		}
		else if(systemState == "ShipShop"){
			myGUIStyle.normal.textColor =  new Color(0F, 1F, 3F, 0.6F);
			myGUIStyle.fontSize = 16;
			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Box (new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/7),  creditsTex, GUIStyle.none )  ;
			GUI.Box (new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/7), credits.ToString(), myGUIStyle )  ;

			if(levels[3].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[3].levelGUI();
			}

		}

<<<<<<< Updated upstream
		//GUI.TextField (new Rect(Screen.width/2+Screen.width/4,Screen.height/16,Screen.width/7,Screen.height/8) , "Credits: " + credits.ToString() )  ;
		
=======

>>>>>>> Stashed changes
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

