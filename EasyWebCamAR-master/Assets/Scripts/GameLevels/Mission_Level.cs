using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	protected CameraSwipe swipeScript;

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	protected string planetState;
	protected bool levelLoaded;

	protected string[] levelNames;
	protected Texture[] levelTex;
	public virtual void loadLevel()	{}
	public virtual void setLevels()	{}

	// textures for the interface:
	public Texture level1;
	public Texture level2;
	public Texture level3;
	public Texture level4;
	public Texture level5;
	public Texture level6;
	public Texture level7;
	public Texture level8;
	

	public override void updateLevel()
	{
		// finds the texture for the buttons
		level1 = Resources.Load("Interface/MissionLevelScreen/Level1") as Texture;
		level2 = Resources.Load("Interface/MissionLevelScreen/Level2") as Texture;
		level3 = Resources.Load("Interface/MissionLevelScreen/Level3") as Texture;
		level4 = Resources.Load("Interface/MissionLevelScreen/Level4") as Texture;
		level5 = Resources.Load("Interface/MissionLevelScreen/Level5") as Texture;
		level6 = Resources.Load("Interface/MissionLevelScreen/Level6") as Texture;
		level7 = Resources.Load("Interface/MissionLevelScreen/Level7") as Texture;
		level8 = Resources.Load("Interface/MissionLevelScreen/Level8") as Texture;
		backTex = Resources.Load("Interface/HangerScreen/Back button") as Texture;

		levelTex = new Texture[8];
		levelTex [0] = level1;
		levelTex [1] = level2;
		levelTex [2] = level3;
		levelTex [3] = level4;
		levelTex [4] = level5;
		levelTex [5] = level6;
		levelTex [6] = level7;
		levelTex [7] = level8;




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
	public virtual void levelGUI(){

		if(planetState == "Home"){
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/7),levelTex[swipeScript.NumberOfSwipes], GUIStyle.none)){
				planetState = levelNames[swipeScript.NumberOfSwipes];
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,0,Screen.width/4,Screen.height/7),backTex, GUIStyle.none)){
				levels.Clear();
				completed = true;	
				closeLevel();
			}
		}else{
			levels[swipeScript.NumberOfSwipes].levelGUI();
		}
	
	}


}
