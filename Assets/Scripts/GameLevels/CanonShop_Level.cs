using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanonShop_Level : LevelScript_Base {
	
	private int canonSelected = 0;
	private string[] canons ;
	private string buildingProp;
	private bool hasGun = false;
	private int gunPos = 0;
	private int price;

	private List<GameObject> buyableObjects = new List<GameObject>();
	// textures for the interface:
	protected newSwipe_Levels swipeControl;

	public override void loadLevel()
	{
		swipeControl = gameObject.AddComponent("newSwipe_Levels") as newSwipe_Levels;

		setMainVars();

		swipeControl.setUpSwipeLimits(6,true);

		canons = script.playerArmory;

		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation ;
		
		if(script.gameSetting == 1){
			buildingProp = "Buildings/Gunshop";
			newProp = buildingProp;
			newScale = new Vector3(10,10,10);
			newPosition = new Vector3(0,-50,-54);
			newRotation = new Vector3(0,270,270);
			createScaleSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}else{
			buildingProp = "Buildings/Kihoskh";
			newProp = buildingProp;
			newScale = new Vector3(10,10,10);
			newPosition = new Vector3(0,-50,-125);
			newRotation = new Vector3(0,270,270);
			createScaleSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		}
		 

		for (int i = 0; i < 6; i++){
			
			newProp = canons[i];
			newScale = new Vector3(15,15,15);
			newPosition = new Vector3(0,0,50);
			newRotation = new Vector3(75,0,0);
			createGoodsObject(newProp,newScale,newPosition,newRotation,props[0].transform.FindChild("buyableObjectSpawn").transform);
			float objHeight = buyableObjects[i].transform.localScale.x;
			Debug.Log(i);
			buyableObjects[i].GetComponent<Weapons_Base>().forceStart();
			if(i == canonSelected){
				buyableObjects[i].SetActive(true);
			}else{
				buyableObjects[i].SetActive(false);
			}

			
		}

		completed = false;
		hasGun = false;
		
		for(int i = 0 ; i < script.hangar.canonTypes.Count ; i++){
			if(canons[canonSelected] == script.hangar.canonTypes[i]){
				hasGun = true;
				gunPos = i;
				
			}
		}

		
		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,15,-15);
		newRotation = new Vector3(125,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.white);


		
	}
	private void deleteAllProps(){
		foreach(GameObject element in buyableObjects){
			Destroy(element);
		}
		buyableObjects.Clear();
		Destroy(swipeControl);
		closeLevel();
	}
	public override void updateLevel()
	{


		if(!completed ){
			buyableObjects[canonSelected].SetActive(false);
			canonSelected = swipeControl.SwipeCounter;

			hasGun = false;
			
			for(int i = 0 ; i < script.hangar.canonTypes.Count ; i++){
				if(canons[canonSelected] == script.hangar.canonTypes[i]){
					hasGun = true;
					gunPos = i;
				}
			}

			newFont = script.newFont;
			buttonTexture = script.buttonTexture;
			myGUIStyle.font = newFont ;
			myGUIStyle.normal.textColor = script.textColor;
			price = buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponValue;
			buyableObjects[canonSelected].SetActive(true);
			buyableObjects[canonSelected].transform.Rotate(new Vector3(0,1,0) * Time.deltaTime * 45); 
		}else{

		}
	}
	
	private int calcUpgradePrice(int modify){
		return (int)(price * (modify/10.0f));
	}
	
	public override void levelGUI()
	{
		int buttonHeight = Screen.height/7 , buttonWidth = Screen.width/4, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;

		placementX = Screen.width/2 - buttonWidth/2; 
		placementY = Screen.height - buttonHeight;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Credits Available", myGUIStyle )  ;
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), script.credits.ToString(), myGUIStyle )  ;
		GUI.EndGroup();


		if(hasGun){
			if(script.hangar.canonUpgrade1[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2;
				
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.canonUpgrade1[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]);
				}

				// the box containing the varying price of the upgrade
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Ratefire Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();
			}
			if(script.hangar.canonUpgrade2[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2 + Screen.height/5;
				
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.canonUpgrade2[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]);
				}

				// the box containing the varying price of the upgrade
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Damage Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();
			}
			if(script.hangar.canonUpgrade3[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1))
			{
				placementX = Screen.width - Screen.width/4; 
				placementY = (Screen.height/5)/2 + 2*Screen.height/5;
				
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight), buttonTexture, GUIStyle.none ))
				{
					script.hangar.canonUpgrade3[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]);
				}
				// the box containing the varying price of the upgrade
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-buttonHeight/3,buttonWidth,buttonHeight), "Magasin Upgrade", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1).ToString(), myGUIStyle);
				GUI.EndGroup();
			}

			placementX = 0; 
			placementY = Screen.height - buttonHeight * 2;
			
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight * 2));
			GUI.Box (new Rect(0,0,buttonWidth*1.5f,buttonHeight * 2),  buttonTexture, GUIStyle.none )  ;
			scaleFont = buttonHeight/4;
			myGUIStyle.fontSize = scaleFont;
			if(!completed ){
				GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Weapon Firerate: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponRateOfFire(script.hangar.canonUpgrade1[gunPos]), myGUIStyle )  ;
				GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), "Weapon Damage: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponDamage(script.hangar.canonUpgrade2[gunPos]), myGUIStyle )  ;
				GUI.Box (new Rect(0,buttonHeight/3 ,buttonWidth,buttonHeight), "Weapon Magasin: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponCapacity(script.hangar.canonUpgrade3[gunPos]), myGUIStyle )  ;
			}
			GUI.EndGroup();

		}else {
			if(script.credits >= price)
			{
				placementX = Screen.width - buttonWidth; 
				placementY = 0;
				
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none))
				{
					script.hangar.addToCanonUpgrades();
					script.hangar.addGunToHangar(canons[canonSelected]);
					script.credits -= price;
					hasGun = true;
					gunPos = gunPos+1;
				}
				scaleFont = buttonHeight/4;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Buy Weapon: ", myGUIStyle);
				GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  price.ToString() , myGUIStyle);
				GUI.EndGroup();
			}
			else{
				GUI.Box(new Rect(Screen.width - buttonWidth,0,buttonWidth,buttonHeight),buttonTexture, GUIStyle.none);
				GUI.Box (new Rect(Screen.width - buttonWidth,-buttonHeight/3,buttonWidth,buttonHeight), "Costs: ", myGUIStyle);
				GUI.Box (new Rect(Screen.width - buttonWidth,0,buttonWidth,buttonHeight), price.ToString(), myGUIStyle);

			}

			placementX = 0; 
			placementY = Screen.height - buttonHeight * 2;
			
			GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight * 2));
			GUI.Box (new Rect(0,0,buttonWidth*1.5f,buttonHeight * 2),  buttonTexture, GUIStyle.none )  ;
			scaleFont = buttonHeight/4;
			myGUIStyle.fontSize = scaleFont;
			if (!completed ){
				GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Weapon Firerate: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponRateOfFire(0), myGUIStyle )  ;
				GUI.Box (new Rect(0,0 ,buttonWidth,buttonHeight), "Weapon Damage: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponDamage(0), myGUIStyle )  ;
				GUI.Box (new Rect(0,buttonHeight/3 ,buttonWidth,buttonHeight), "Weapon Magasin: " + buyableObjects[canonSelected].GetComponent<Weapons_Base>().weaponCapacity(0), myGUIStyle )  ;
			}
			//Debug.Log("2 " + buyableObjects[gunPos].name);
			GUI.EndGroup();
		}
		placementX = Screen.width/2 - buttonWidth/2; 
		placementY = Screen.height - buttonHeight;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight),  buttonTexture, GUIStyle.none )  ;
		scaleFont = buttonHeight/4;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-buttonHeight/3 ,buttonWidth,buttonHeight), "Credits Available", myGUIStyle )  ;
		GUI.Box (new Rect(0,0,buttonWidth,buttonHeight), script.credits.ToString(), myGUIStyle )  ;
		GUI.EndGroup();

		placementX = 0; 
		placementY = 0;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight),buttonTexture,GUIStyle.none))
		{
			completed = true;
			deleteAllProps();
			script.profileMan.gameSave();
		}
		scaleFont = buttonHeight/3;
		myGUIStyle.fontSize = scaleFont;
		GUI.Box (new Rect(0,-scaleFont/2,buttonWidth,buttonHeight), "Back", myGUIStyle );
		GUI.EndGroup();




	}

	protected void createGoodsObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		tmp.transform.localScale = new Vector3(scale.x , scale.y , scale.z);
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
