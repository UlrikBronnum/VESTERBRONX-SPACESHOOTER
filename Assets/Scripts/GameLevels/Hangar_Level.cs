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

	private string buildingProps;

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

		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation ;

		if(script.gameSetting == 1){
			buildingProps = "Buildings/SpaceHangar";
			newProp = buildingProps;
			newScale = new Vector3(5,10,5);
			newPosition = new Vector3(0,0,-125);
			newRotation = new Vector3(90,180,0);
			createScaleSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}else{
			buildingProps = "Buildings/Planetarium";
			newProp = buildingProps;
			newScale = new Vector3(2.9f,4.2f,2.9f);
			newPosition = new Vector3(0,0,-125);
			newRotation = new Vector3(90,180,0);
			createScaleSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}




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
		newRotation = new Vector3(125,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.white);

		shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
	}

	public override void updateLevel()
	{
		//Debug.Log(canonLimit);

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
			placementY = Screen.height/10;

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
			placementY = Screen.height/10 + buttonHeight;

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
			string[] getLine = script.hangar.canonTypes[countMountTwo].ToString().Split('/');
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

		placementX = 0; 
		placementY = Screen.height - buttonHeight * 2;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight * 2));
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight * 2),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;	
		GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), shipScript.getGunOne(), myGUIStyle )  ;

		GUI.EndGroup();

		
		placementX = 0; 
		placementY = Screen.height - buttonHeight * 1;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight * 2));
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight * 2),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;	
		GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), shipScript.getShipStats(), myGUIStyle )  ;
		
		GUI.EndGroup();
	}


}
