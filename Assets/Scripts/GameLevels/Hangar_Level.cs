using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hangar_Level : LevelScript_Base {
		
	private string cameraName = "ARCamera";
	private int countMountOne = 0;
	private int countMountTwo = 0;
	private int selectedGun = 0;
	private int canonLimit = 0;
	private Spaceship_Player shipScript; 

	// textures for the interface:

	protected newSwipe_Levels swipeControl;

	private string[] buildingProps = new string[1];

	private void deleteAllProps(){

		closeLevel();
	}

	public override void loadLevel()
	{
		swipeControl = gameObject.AddComponent("newSwipe_Levels") as newSwipe_Levels;
		// finds the texture for the buttons
		setMainVars();

		swipeControl.setUpSwipeLimits(script.hangar.hangarslots.Count,true);

		completed = false;

		buildingProps[0] = "Buildings/SpaceHangar"; 

		string newProp = buildingProps[0];
		Vector3 newScale = new Vector3(5,10,5);
		Vector3 newPosition = new Vector3(0,0,-125);
		Vector3 newRotation = new Vector3(90,180,0);
		createScaleSceneObject(newProp,newScale,newPosition,newRotation,background.transform);


		newScale = new Vector3(10,10,10);
		newPosition = new Vector3(0,0,50);
		newRotation = new Vector3(0,0,0);


		for (int i = 0; i < script.hangar.hangarslots.Count; i++){
			if(i == script.shipChoise){
				createPlayerSpaceship(script.hangar.hangarslots[i],newScale,newPosition,newRotation,props[0].transform.FindChild("buyableObjectSpawn").transform,false,true);
			}else{
				createPlayerSpaceship(script.hangar.hangarslots[i],newScale,newPosition,newRotation,props[0].transform.FindChild("buyableObjectSpawn").transform,false,false);
			}
		}

		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,15,-15);
		newRotation = new Vector3(45,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.yellow);

	}

	public override void updateLevel()
	{


		if(completed){
		}else{
			script.hangar.hangarslots[script.shipChoise].SetActive(false);
			script.shipChoise = swipeControl.SwipeCounter;
			shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
			canonLimit = shipScript.CanonMountCapacity;
			script.hangar.hangarslots[script.shipChoise].SetActive(true);
			script.hangar.hangarslots[script.shipChoise].transform.Rotate(new Vector3(0,1,0) * Time.deltaTime * 45); 

		}
	}


	public override void levelGUI(){
		//
		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;

		if(canonLimit >= 2)
		{
			placementX = Screen.width - Screen.width/4; 
			placementY = (Screen.height/5)/2;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
			{
				selectedGun = 0;
				countMountOne++;

				if(countMountOne > script.hangar.canonTypes.Count - 1){
					countMountOne = 0;
				}

				shipScript.removeCanon(selectedGun);

				string newarr = script.hangar.canonTypes[countMountOne];
				shipScript.gunSetting(newarr,selectedGun);
				shipScript.mountCanon(selectedGun);
			}
			scaleFont = buttonHeight/4;
			myGUIStyle.fontSize = scaleFont;
			string[] getLine = script.hangar.canonTypes[countMountOne].ToString().Split('/');
			GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Change Weapon", myGUIStyle);
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), getLine[1], myGUIStyle);
			GUI.EndGroup();
		}
		if(canonLimit >= 4)
		{
			placementX = Screen.width - Screen.width/4; 
			placementY = (Screen.height/5)/2;

			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
			if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
			{
				selectedGun = 1;
				countMountTwo++;
				if(countMountTwo > script.hangar.canonTypes.Count - 1){
					countMountTwo = 0;
				}

				shipScript.removeCanon(selectedGun);
				string newarr = script.hangar.canonTypes[countMountTwo];
				shipScript.gunSetting(newarr,selectedGun);
				shipScript.mountCanon(selectedGun);
			}
			scaleFont = buttonHeight/4;
			myGUIStyle.fontSize = scaleFont;
			string[] getLine = script.hangar.canonTypes[countMountOne].ToString().Split('/');
			GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Change Weapon", myGUIStyle);
			GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), getLine[1], myGUIStyle);
			GUI.EndGroup();
		}

		placementX = 0; 
		placementY = 0;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
		{
			completed = true;
			for(int i = 0 ; i < script.hangar.hangarslots.Count; i++){
				Spaceship_Player ship = script.hangar.hangarslots[i].GetComponent<Spaceship_Player>();
				ship.IsActive = false;
				ship.gameObject.SetActive(false);
			}
			deleteAllProps();
		}
		scaleFont = buttonHeight/3;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-buttonHeight/6,buttonWidth,buttonHeight), "Back", myGUIStyle )  ;
		GUI.EndGroup();
	}


}
