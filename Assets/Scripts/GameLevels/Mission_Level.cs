using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	protected string planetState;
	protected bool levelLoaded;
	protected bool access = true;
	protected string[] levelNames;

	int levelCounter = 0;
	int playerCounter = 0;

	public virtual void loadLevel()	{}
	public virtual void setLevels()	{}

	// textures for the interface:

	public override void updateLevel()
	{

		// finds the texture for the buttons
		setMainVars();

		levelCounter = swipeScript.NumberOfSwipes;
		playerCounter = script.levelsCompleted;
		access = levels[levelCounter].canLoad(playerCounter);
		if(!completed ){
		
		}else{

		}
		if(planetState == "Home"){
			if(levelLoaded == false){
				levelLoaded = true;
				loadLevel();
			}
		}


		if(planetState == levelNames[swipeScript.NumberOfSwipes]){
			if(levelLoaded == false &&  access){
				closeLevel();
				levels[levelCounter].loadLevel();
				levelLoaded = true;
			}else if (levels[levelCounter].Completed) {
				planetState = "Home";
				levelLoaded = false;
			}else{
				levels[levelCounter].updateLevel();
			}
	
		}else { 
			planetState = "Home";
		}

	}
	public virtual void levelGUI()
	{
		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;

		if(planetState == "Home" && levels.Count != 0)
		{
			placementX = Screen.width - buttonWidth; 
			placementY = 0;
			if(access){
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
					planetState = levelNames[swipeScript.NumberOfSwipes];
					levelLoaded = false;
				}
				scaleFont = buttonHeight/3;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), levelNames[swipeScript.NumberOfSwipes], myGUIStyle);
				GUI.EndGroup();
			}
			placementX = 0; 
			placementY = 0;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none)){
				levels.Clear();
				completed = true;	
				closeLevel();
			}
			scaleFont = buttonHeight/3;
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Back", myGUIStyle);
			GUI.EndGroup();
		}
		else
		{
			if(levelLoaded && levels.Count != 0){
				levels[swipeScript.NumberOfSwipes].levelGUI();
			}

		}
		if (!access)
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





}
