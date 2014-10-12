using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{
	



	// timer to control internal behavior
	public char gameSetting;

	private bool levelLoaded;
	private string systemState;

	public Hangar_Base hangar;
	protected int hangarCapacity;

	public int shipChoise;
	private List<LevelScript_Base> levels = new List<LevelScript_Base>();

	public void Start () 
	{

		shipChoise = 0;
		systemState = "Menu";
		levelLoaded = false;
		hangar = gameObject.AddComponent("Hangar_Base") as Hangar_Base;
		hangar.addGunToHangar("projectileCanon");
		hangar.addGunToHangar("ionCanon");
		hangar.addSpaceshipToHangar("TurdClass");
		hangar.addSpaceshipToHangar("SecondClass");

		setLevels();
		hangar.setHangar();

	}

	public  void Update () 
	{
		if(systemState == "Level1"){
			if(levelLoaded == false){

				Debug.Log(Application.platform);
			}

		}





		if(systemState == "Menu"){

		}
		/*
		if(systemState == "Missions"){

				levelLoaded = true;
				levels[0].loadLevel();
			}else if(levelLoaded == true){
				levels[0].updateLevel();

			}
		}
*/
		if(systemState == "Hangar"){

			if(levelLoaded == false){
				levelLoaded = true;
				levels[0].loadLevel();
			}else{
				levels[0].updateLevel();
			}
		}

		if(systemState == "Level1"){
			if(levelLoaded == false){
				levelLoaded = true;
				levels[1].loadLevel();
			}else{
				levels[1].updateLevel();
			}
		}


	}

	public void OnGUI(){
		if(systemState == "Menu"){
			if(GUI.Button(new Rect(0,0,80,50),"Hangar")){
				systemState = "Hangar";
				levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,50,80,50),"Missions")){
				//systemState = "Missions";
				//levelLoaded = false;
			}
			if(GUI.Button(new Rect(0,100,80,50),"Level1")){
				systemState = "Level1";
				levelLoaded = false;
			}
		}else if(systemState == "Missions"){
			if(levels[0].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[0].levelGUI();
			}
			if(GUI.Button(new Rect(0,0,80,50),"Back")){
				systemState = "Menu";
				levels[0].Completed = true;
				levelLoaded = false;
				levels[0].updateLevel();
			}

		}else if(systemState == "Hangar"){
			if(levels[0].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[0].levelGUI();
			}
		}else if(systemState == "Level1"){
			if(levels[1].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else if(levelLoaded == true){
				levels[1].levelGUI();
			}
		}
		
	}

	private void setLevels(){

	//	Mission_Level newMissionLevel = gameObject.AddComponent("Mission_Level") as Mission_Level;
	//	levels.Add(newMissionLevel);
		Hangar_Level newHangarLevel = gameObject.AddComponent("Hangar_Level") as Hangar_Level;
		levels.Add (newHangarLevel);
		Level_One newLevel = gameObject.AddComponent("Level_One") as Level_One;
		levels.Add(newLevel);
	
	}


}

