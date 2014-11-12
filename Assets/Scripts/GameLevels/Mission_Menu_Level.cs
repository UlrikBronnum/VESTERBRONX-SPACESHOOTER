using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Menu_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;
	public List<Mission_Level> levels = new List<Mission_Level>();
	protected string missionState;
	protected bool levelLoaded;
	protected string[] levelNames;

	

	public override void loadLevel()
	{
		// finds the texture for the buttons

		setMainVars();


		levelNames = new string[3];
		levelNames[0] = "Planet_One";
		levelNames[1] = "Planet_Two";
		levelNames[2] = "Planet_Three";
	
		
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
		
	}


	public override void updateLevel()
	{

		if(!completed ){
		
			
		}else{

		}

		if(missionState == "Home"){
			if(levelLoaded == false){
				levelLoaded = true;
				loadLevel();
			}
		}
		
		if(missionState == levelNames[swipeScript.NumberOfSwipes]){
			if(levelLoaded == false){
				closeLevel();
				levelLoaded = true;
				levels[swipeScript.NumberOfSwipes].loadLevel();
			}else if (levels[swipeScript.NumberOfSwipes].Completed) {
				missionState = "Home";
				levelLoaded = false;
			}else{
				levels[swipeScript.NumberOfSwipes].updateLevel();
			}
		}
		
	}
	public override void levelGUI()
	{
		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;
		

		
		if(missionState == "Home"){
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
}
