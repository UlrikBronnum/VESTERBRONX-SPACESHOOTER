﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Charactor : MonoBehaviour 
{

	// timer to control internal behavior
	public char gameSetting;


	private bool levelLoaded;
	private string systemState;
	private List<LevelScript_Base> levels = new List<LevelScript_Base>();

	public Hangar_Base hangar;
	protected int hangarCapacity;
	public int shipChoise;

	private int kredits;

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

		Debug.Log(systemState);
		if(systemState == "Menu"){

		}
	


		if(systemState == "Hangar"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[0].loadLevel();
			}else{
				levels[0].updateLevel();
			}
		}

		if(systemState == "MissionLevel"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[1].loadLevel();
			}else{
				levels[1].updateLevel();
			}
		}

		if(systemState == "AmmoShop"){
			if(levelLoaded == false){
				levelLoaded = true;
				//levels[2].loadLevel();
			}else{
				levels[2].updateLevel();
				
			}
		}
	}

	public void OnGUI(){
		if(systemState == "Menu"){
			if(GUI.Button(new Rect(0,0,200,100),"Hangar")){
				systemState = "Hangar";
				levelLoaded = false;
				levels[0].loadLevel();
				Debug.Log("0");
			}
			if(GUI.Button(new Rect(0,100,200,100),"MissionLevel")){
				systemState = "MissionLevel";
				levelLoaded = false;
				levels[1].loadLevel();
				Debug.Log("1");
			}
			if(GUI.Button(new Rect(0,200,200,100),"AmmoShop")){
				systemState = "AmmoShop";
				levelLoaded = false;
				levels[2].loadLevel();
				Debug.Log("2");
			}
		}
		else if(systemState == "Hangar"){
			if(levels[0].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[0].levelGUI();
			}
		}
		else if(systemState == "MissionLevel"){
			if(levels[1].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else {
				levels[1].levelGUI();
			}
		}
		else if(systemState == "AmmoShop"){
			if(levels[2].Completed){
				systemState = "Menu";
				levelLoaded = false;
			}else{
				levels[2].levelGUI();
			}
		}
		
	}

	private void setLevels(){
		Hangar_Level newHangarLevel = gameObject.AddComponent("Hangar_Level") as Hangar_Level;
		levels.Add (newHangarLevel);
		Mission_Level newMissionLevel = gameObject.AddComponent("Mission_Level") as Mission_Level;
		levels.Add(newMissionLevel);
		AmmunitionShop_Level newAmmoLevel = gameObject.AddComponent("AmmunitionShop_Level") as AmmunitionShop_Level;
		levels.Add(newAmmoLevel);
	
	}


}

