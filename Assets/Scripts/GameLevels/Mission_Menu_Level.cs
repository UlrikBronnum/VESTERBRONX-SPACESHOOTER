using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Menu_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;
	public List<Mission_Level> levels = new List<Mission_Level>();
	protected string missionState;
	protected bool levelLoaded;
	protected string[] levelNames;
	int levelCounter = 0;
	protected bool[] access = new bool[3] { true , false , false};

	

	public override void loadLevel()
	{
		// finds the texture for the buttons

		setMainVars();
		levelNames = new string[3];
		if (script.gameSetting == 1) {
						levelNames [0] = "Planet_One";
						levelNames [1] = "Planet_Two";
						levelNames [2] = "Planet_Three";
				} else {
			levelNames [0] = "Planet Enghave";
			levelNames [1] = "Planet Sønder Blvd";
			levelNames [2] = "Planet Istedgade";
				
		}
	
		
		if(levels.Count == 0){
			setLevels();
		}
		
		levelLoaded = true;
		completed = false;
		missionState = "Home";
				
		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;

		if(script.gameSetting == 0){
			newProp = "LevelProps/Menu_vesterbro";
			newScale = new Vector3(1,1,1);
			newPosition = new Vector3(0,0,0);
			newRotation = new Vector3(0,0,0);
			createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}else
		{
			newProp = "LevelProps/Menu_space";
			newScale = new Vector3(1f,1f,1f);
			newPosition = new Vector3(0,0,0);
			newRotation = new Vector3(0,0,0);
			createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}
		transform.parent = transform;

		
		swipeScript = props[0].GetComponent<CameraSwipe>();

		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,15,-15);
		newRotation = new Vector3(125,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, new Color (0.8f,0.3f,0.0f,1.0f));

		if(script.levelsCompleted > 7){
			access[1] = true;
		}
		if(script.levelsCompleted > 15){
			access[2] = true;
		}

	}

	public override void updateLevel()
	{



		if(missionState == "Home"){
			if(levelLoaded == false){
				levelLoaded = true;
				loadLevel();
				swipeScript.resetSwipe(levelCounter);

			}
			levelCounter = swipeScript.NumberOfSwipes;
		}
		
		if(missionState == levelNames[levelCounter]){
			if(levelLoaded == false && access[levelCounter]){
				closeLevel();
				levelLoaded = true;
				levels[levelCounter].loadLevel();
			}else if (levels[levelCounter].Completed) {
				missionState = "Home";
				levelLoaded = false;
			}else{
				levels[levelCounter].updateLevel();
			}
		}


		
	}
	public override void levelGUI()
	{

		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;
		

		
		if(missionState == "Home"){

			if (script.firstTime) {
				GUI.DrawTexture(new Rect(Screen.width/2-Screen.width/10,Screen.height-Screen.height/3, Screen.width/6,Screen.height/5), swipeSym, ScaleMode.ScaleToFit, true, 0);
			}

			placementX = Screen.width - buttonWidth; 
			placementY = 0;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
				missionState = levelNames[swipeScript.NumberOfSwipes];
				levelLoaded = false;
			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), levelNames[swipeScript.NumberOfSwipes], myGUIStyle);
			GUI.EndGroup();

			placementX = 0; 
			placementY = 0;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none)){

				completed = true;	
				closeLevel();
				destroyContent();
				levels.Clear();
			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Back", myGUIStyle);
			GUI.EndGroup();

		}else {
			if(levelLoaded)
			levels[swipeScript.NumberOfSwipes].levelGUI();
		}

		if (!access[levelCounter])
		{
			buttonHeight = Screen.height/2;
			buttonWidth = Screen.height/2;
			placementX = Screen.width/2 - buttonWidth/2;
			placementY = Screen.height/2 - buttonHeight/2; 
			
			scaleFont = 50;
			
			
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),Resources.Load("Interface/NOAccess") as Texture);
			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "No Access", myGUIStyle);
			GUI.EndGroup();
		}
		
	}
	public void setLevels()
	{
		Planet_One_Level newLevel0 = gameObject.AddComponent("Planet_One_Level") as Planet_One_Level;
		levels.Add(newLevel0);
		Planet_Two_Level newLevel1 = gameObject.AddComponent("Planet_Two_Level") as Planet_Two_Level;
		levels.Add(newLevel1);
		Planet_Three_Level newLevel2 = gameObject.AddComponent("Planet_Three_Level") as Planet_Three_Level;
		levels.Add(newLevel2);

	}
	public void destroyContent(){
		for (int i = 0; i < levels.Count ; i++){
			Destroy(levels[i]);
		}
		Destroy(swipeScript);
	}
}
