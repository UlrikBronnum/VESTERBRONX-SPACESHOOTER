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
		if(!completed ){
		
		}else{

		}
		Debug.Log(levelLoaded);
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
			if(GUI.Button(new Rect(Screen.width - 200, 0 ,200,100),levelNames[swipeScript.NumberOfSwipes])){
				planetState = levelNames[swipeScript.NumberOfSwipes];
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,0,200,100),"Back")){
				levels.Clear();
				completed = true;	
				closeLevel();
			}
		}

	}


}
