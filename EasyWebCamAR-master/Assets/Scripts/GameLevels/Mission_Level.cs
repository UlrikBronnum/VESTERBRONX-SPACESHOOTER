using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	protected string planetState;
	protected bool levelLoaded;

	protected string[] levelNames;

	public virtual void loadLevel()	{}
	public virtual void setLevels()	{}
	

	public override void updateLevel()
	{
		// finds the texture for the buttons
		backTex = Resources.Load("Interface/Hanger Screen/Back button") as Texture;

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
			if(levelLoaded == false){
				closeLevel();
				levelLoaded = true;
				levels[swipeScript.NumberOfSwipes].loadLevel();
			}else if (levels[swipeScript.NumberOfSwipes].Completed) {
				planetState = "Home";
				levelLoaded = false;
			}else{
				levels[swipeScript.NumberOfSwipes].updateLevel();
			}
		}

	}
	public override void levelGUI(){


		if(planetState == "Home"){
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/4),levelNames[swipeScript.NumberOfSwipes])){
				planetState = levelNames[swipeScript.NumberOfSwipes];
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,0,Screen.width/4,Screen.height/7),backTex, GUIStyle.none)){
				levels.Clear();
				completed = true;	
				closeLevel();
			}
		}

	}


}
