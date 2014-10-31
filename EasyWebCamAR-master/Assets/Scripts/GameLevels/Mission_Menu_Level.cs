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
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		GameObject image = GameObject.Find ("ImageTarget");
		
		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;
		
		newProp = "PlanetChainMenu";
		newScale = new Vector3(150,150,150);
		newPosition = new Vector3(0,0,0);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);
		transform.parent = transform;

		
		swipeScript = props[0].GetComponent<CameraSwipe>();
		
	}


	public override void updateLevel()
	{
		// finds the texture for the buttons
		backTex = Resources.Load("Interface/Hanger Screen/Back button") as Texture;

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
	public override void levelGUI(){
		
		
		if(missionState == "Home"){
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/8, Screen.height/10,Screen.width/4,Screen.height/4),levelNames[swipeScript.NumberOfSwipes])){
				missionState = levelNames[swipeScript.NumberOfSwipes];
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,0,Screen.width/4,Screen.height/7),backTex,GUIStyle.none)){
				levels.Clear();
				completed = true;	
				closeLevel();
			}
		}else {
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
