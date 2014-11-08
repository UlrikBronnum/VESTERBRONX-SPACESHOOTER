using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	protected string planetState;
	protected bool levelLoaded;
	protected bool noAccess;
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
		noAccess = levels[levelCounter].canLoad(playerCounter);
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
			if(levelLoaded == false &&  noAccess){
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
			if(noAccess){
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
			if(levelLoaded  && levels.Count != 0){
				levels[swipeScript.NumberOfSwipes].levelGUI();
			}

		}
		if (!noAccess)
		{
			buttonHeight = Screen.height/10 * 5;
			buttonWidth = Screen.width/10 * 5;
			placementX =  (int)(Screen.height/10 * 0.5f);
			placementY =  (int)(Screen.width/10 * 0.5f); 
			
			scaleFont = 40;
			myGUIStyle.alignment = TextAnchor.MiddleCenter;
			
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),Resources.Load("Interface/NOAccess") as Texture);
			myGUIStyle.fontSize = scaleFont;
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), "No Access", myGUIStyle);
			GUI.EndGroup();
		}
	}





}
