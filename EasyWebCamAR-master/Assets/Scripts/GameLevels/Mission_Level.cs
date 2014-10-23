using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {

	public List<LevelScript_Level> levels = new List<LevelScript_Level>();
	private string planetState;
	private bool levelLoaded;

	public override void loadLevel()
	{
		if(levels.Count == 0){
			setLevels();
		}
		levelLoaded = false;
		completed = false;

		planetState = "Home";
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();


	
	}

	public override void updateLevel()
	{
		if(!completed ){

		}else{
			closeLevel();
		}

		if(planetState == "Level1"){
			if(levelLoaded == false){
				levelLoaded = true;
				levels[0].loadLevel();
			}else{
				levels[0].updateLevel();
			}
		}
		
		if(planetState == "Level2"){
			if(levelLoaded == false){
				levelLoaded = true;
				levels[1].loadLevel();
			}else{
				levels[1].updateLevel();
			}
		}
		
		if(planetState == "Level3"){
			if(levelLoaded == false){
				levelLoaded = true;
				levels[2].loadLevel();
			}else{
				levels[2].updateLevel();
				
			}
		}
	}
	public override void levelGUI(){
		if(planetState == "Home"){
			if(GUI.Button(new Rect(Screen.width/10 + 0,Screen.height/10 * 6,200,100),"Level1")){
				planetState = "Level1";
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(Screen.width/10 + 200,Screen.height/10 * 6,200,100),"Level2")){
				planetState = "Level2";
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(Screen.width/10 + 400,Screen.height/10 * 6,200,100),"Level3")){
				planetState = "Level3";
				levelLoaded = false;
			}
		}
		else if(planetState == "Level1"){
			if(levels[0].Completed){
				planetState = "Home";
				levelLoaded = false;
			}else{
				levels[0].levelGUI();
			}
		}
		else if(planetState == "Level2"){
			if(levels[1].Completed){
				planetState = "Home";
				levelLoaded = false;
			}else{
				levels[1].levelGUI();
			}
		}
		else if(planetState == "Level3"){
			if(levels[2].Completed){
				planetState = "Home";
				levelLoaded = false;
			}else{
				levels[2].levelGUI();
			}
		}
		if(planetState == "Home"){
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
	}
}
