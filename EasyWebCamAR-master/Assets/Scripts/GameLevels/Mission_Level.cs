using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	CameraSwipe swipeScript;

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	private string planetState;
	private bool levelLoaded;

	private string[] levelNames;



	public override void loadLevel()
	{
		levelNames = new string[8];
		levelNames[0] = "Level 1";
		levelNames[1] = "Level 2";
		levelNames[2] = "Level 3";
		levelNames[3] = "Level 4";
		levelNames[4] = "Level 5";
		levelNames[5] = "Level 6";
		levelNames[6] = "Level 7";
		levelNames[7] = "Level 8";

		if(levels.Count == 0){
			setLevels();
		}

		levelLoaded = true;
		completed = false;

		planetState = "Home";
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		GameObject image = GameObject.Find ("ImageTarget");

		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;

		newProp = "PlanetChain";
		newScale = new Vector3(25,25,25);
		newPosition = new Vector3(0,0,0);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);
		transform.parent = transform;


		swipeScript = props[0].GetComponent<CameraSwipe>();
	
	}

	public override void updateLevel()
	{
		if(!completed ){
			Debug.Log(swipeScript.NumberOfSwipes);

		}else{
			closeLevel();
		}
		Debug.Log(levelLoaded);
		if(planetState == "Home"){
			if(levelLoaded == false){
				levelLoaded = true;
				loadLevel();
				Debug.Log("I am silly");
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
			}
		}

	}

	public void setLevels()
	{
		Level_One newLevel0 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel0);
		Level_Two newLevel1 = gameObject.AddComponent("Level_Two") as Level_Two;
		levels.Add(newLevel1);
		Level_One newLevel2 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel2);
		Level_One newLevel3 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel3);
		Level_One newLevel4 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel4);
		Level_One newLevel5 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel5);
		Level_One newLevel6 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel6);
		Level_One newLevel7 = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel7);

	}
}
