using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceshipShop_Level : LevelScript_Base {
	
	private int shipSelected = 0;
	private string[] ships;
	private string buildingProps;
	private bool hasShip = false;
	private int shipPos = 0;

	private int price;
	private List<GameObject> buyableObjects = new List<GameObject>();

	protected newSwipe_Levels swipeControl;


	// textures for the interface:
	private void deleteAllProps(){
		foreach(GameObject element in buyableObjects){
			Destroy(element);
		}
		buyableObjects.Clear();
		Destroy(swipeControl);
		closeLevel();
	}
	
	public override void loadLevel()
	{
		swipeControl = gameObject.AddComponent("newSwipe_Levels") as newSwipe_Levels;

		setMainVars();

		swipeControl.setUpSwipeLimits(2,true);
		ships = script.playerVersion;
	
		
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

		for (int i = 0; i < 2; i++)
		{			
			newProp = ships[i];
			newScale = new Vector3(10,10,10);
			newPosition = new Vector3(0,0,50);
			newRotation = new Vector3(90,0,0);
			createGoodsObject(newProp,newScale,newPosition,newRotation,props[0].transform.FindChild("buyableObjectSpawn").transform);
			buyableObjects[i].GetComponent<Spaceship_Player>().shipInitialization();
			if(i == shipSelected){
				buyableObjects[i].SetActive(true);
			}else{
				buyableObjects[i].SetActive(false);
			}			
		}

		
		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,15,-15);
		newRotation = new Vector3(125,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.white);


		completed = false;
		hasShip = false;
		
		for(int i = 0 ; i < script.hangar.shipTypes.Count ; i++){
			if(ships[shipSelected] == script.hangar.shipTypes[i]){
				hasShip = true;
				shipPos = i;
				
			}
		}
		
		
	}
	
	public override void updateLevel()
	{
		if(!completed ){
			buyableObjects[shipSelected].SetActive(false);
			shipSelected = swipeControl.SwipeCounter;

			hasShip = false;
			
			for(int i = 0 ; i < script.hangar.shipTypes.Count ; i++){
				if(ships[shipSelected] == script.hangar.shipTypes[i]){
					hasShip = true;
					shipPos = i;
				}
			}

			price = buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipValue;
			newFont = script.newFont;
			buttonTexture = script.buttonTexture;
			myGUIStyle.font = newFont ;
			myGUIStyle.normal.textColor = script.textColor;
			buyableObjects[shipSelected].SetActive(true);
			buyableObjects[shipSelected].transform.Rotate(new Vector3(0,1,0) * Time.deltaTime * 45); 

		}
		else{
			
		}
	}
	
	private int calcUpgradePrice(int modify){
		return (int)(price * (modify/10.0f));
	}
	
	public override void levelGUI()
	{

		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;

		if(hasShip){
			if(script.hangar.shipUpgrade1[shipPos] < 8 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2;

				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.shipUpgrade1[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]);
					script.hangar.setNewUpgradesHangar(shipPos);
				}
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Health Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();
			}
			if(script.hangar.shipUpgrade2[shipPos] < 8 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade2[shipPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2 + Screen.height/5;

				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.shipUpgrade2[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade2[shipPos]);
					script.hangar.setNewUpgradesHangar(shipPos);
				}
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Shield Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), calcUpgradePrice(script.hangar.shipUpgrade2[shipPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();
			}
			if(script.hangar.shipUpgrade3[shipPos] < 8 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade3[shipPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2 + 2*Screen.height/5;

				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.shipUpgrade3[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade3[shipPos]);
					script.hangar.setNewUpgradesHangar(shipPos);
				}
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Speed Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), calcUpgradePrice(script.hangar.shipUpgrade3[shipPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();

			}
		}else {
			if(script.credits >= price)
			{
				placementX = Screen.width - buttonWidth; 
				placementY = 0;

				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
				{
					script.hangar.addToShipUpgrades();
					script.hangar.addSpaceshipToHangar(ships[shipSelected]);
					script.credits -= price;
					hasShip = true;
					shipPos = shipPos + 1;
					script.hangar.setHangar();

				}
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Buy Ship: ", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  price.ToString() , myGUIStyle);
				GUI.EndGroup();
			}
			else{
				GUI.Box(new Rect(Screen.width - buttonWidth,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none);
				GUI.Box (new Rect(Screen.width - buttonWidth,-buttonHeight/3,buttonWidth,buttonHeight), "Costs: ", myGUIStyle);
				GUI.Box (new Rect(Screen.width - buttonWidth,0,buttonWidth,buttonHeight), price.ToString(), myGUIStyle);

			}

		}
	
		placementX = Screen.width/2 - buttonWidth/2; 
		placementY = Screen.height - buttonHeight;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-buttonHeight/3 ,Screen.width/4,Screen.height/7), "Credits Available", myGUIStyle )  ;
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), script.credits.ToString(), myGUIStyle )  ;
		GUI.EndGroup();


		placementX = 0; 
		placementY = 0;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
		{
			completed = true;
			deleteAllProps();
		}
		scaleFont = buttonHeight/3;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Back", myGUIStyle )  ;
		GUI.EndGroup();


		placementX = 0; 
		placementY = Screen.height - buttonHeight * 2;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight * 2));
		GUI.Box (new Rect(0,0,buttonWidth*1.5f,buttonHeight * 2),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;
		if(hasShip && !completed ){
			GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Health: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipHealth(script.hangar.shipUpgrade1[shipPos]), myGUIStyle )  ;
			GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), "Shield: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipShield(script.hangar.shipUpgrade2[shipPos]), myGUIStyle )  ;
			GUI.Box (new Rect(0,buttonHeight/3 ,buttonWidth,buttonHeight), "Maneuver speed: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipManeuverSpeed(script.hangar.shipUpgrade3[shipPos]), myGUIStyle )  ;
		}else if (!hasShip && !completed ){
			GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Health: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipHealth(0), myGUIStyle )  ;
			GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), "Shield: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipShield(0), myGUIStyle )  ;
			GUI.Box (new Rect(0,buttonHeight/3 ,buttonWidth,buttonHeight), "Maneuver speed: " + buyableObjects[shipSelected].GetComponent<Spaceship_Player>().shipManeuverSpeed(0), myGUIStyle )  ;
		}
		GUI.EndGroup();

	}

	protected void createGoodsObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		//tmp.transform.localScale = new Vector3(scale.x , scale.y , scale.z);
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		//tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		buyableObjects.Add(tmp);
	}
}
