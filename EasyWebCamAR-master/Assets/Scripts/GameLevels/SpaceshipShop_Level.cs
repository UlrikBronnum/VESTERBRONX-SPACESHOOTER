using UnityEngine;
using System.Collections;

public class SpaceshipShop_Level : LevelScript_Base {
	
	private int shipSelected = 0;
	private string[] ships = new string[4];
	private bool hasShip = false;
	private int shipPos = 0;

	private int price;

	// textures for the interface:
	public Texture switchShipTex;
	public Texture switchShipTex2;
	public Texture upgradeHealthTex;
	public Texture upgradeShieldTex;
	public Texture upgradeSpeedTex;
	public Texture buyShipTex;
	private GUIStyle myGUIStyle = new GUIStyle();
	
	public override void loadLevel()
	{
		// finds the texture for the buttons
		backTex = Resources.Load("Interface/CannonShop/Back button") as Texture;
		switchShipTex = Resources.Load("Interface/ShipShop/<Switch Ship") as Texture;
		switchShipTex2 = Resources.Load("Interface/ShipShop/Switch Ship_") as Texture;
		upgradeHealthTex = Resources.Load("Interface/ShipShop/Upgrade Health") as Texture;
		upgradeShieldTex = Resources.Load("Interface/ShipShop/UpgradeShield") as Texture;
		upgradeSpeedTex = Resources.Load("Interface/ShipShop/UpgradeSpeed") as Texture;
		buyShipTex = Resources.Load("Interface/ShipShop/BuyShip") as Texture;


		// sets the font style for the price, which varies
		myGUIStyle.normal.textColor =  new Color(0F, 1F, 3F, 0.6F);
		myGUIStyle.fontSize = 15;
		myGUIStyle.alignment = TextAnchor.MiddleCenter;

		ships[0] = "TurdClass";
		ships[1] = "SecondClass";
		ships[2] = "TurdClass";
		ships[3] = "TurdClass";
		
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		
		GameObject background = GameObject.Find("ImageTarget");
		
		for (int i = 0; i < 4; i++){
			
			string newProp = ships[i];
			Vector3 newScale = new Vector3(30,30,30);
			Vector3 newPosition = new Vector3(0,-20,0);
			Vector3 newRotation = new Vector3(90,180,0);
			createShopSpaceship((GameObject)Object.Instantiate(Resources.Load(newProp)),newScale,newPosition,newRotation,background.transform, false);

			if(i == shipSelected){
				props[i].SetActive(true);
			}else{
				props[i].SetActive(false);
			}
			
		}

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
			price = props[shipSelected].GetComponent<Spaceship_Player>().shipValue;
		}else{
			
		}
	}
	
	private int calcUpgradePrice(int modify){
		return (int)(price * (modify/10.0f));
	}
	
	public override void levelGUI(){
		if(GUI.Button(new Rect(Screen.width/10,Screen.height-Screen.height/4,Screen.width/4,Screen.height/7),switchShipTex,GUIStyle.none)){
			props[shipSelected].SetActive(false);
			shipSelected--;
			if(shipSelected < 0){
				shipSelected = 3;
			}
			props[shipSelected].SetActive(true);
			price = props[shipSelected].GetComponent<Spaceship_Player>().shipValue;
			hasShip = false;
			
			for(int i = 0 ; i < script.hangar.shipTypes.Count ; i++){
				if(ships[shipSelected] == script.hangar.shipTypes[i]){
					hasShip = true;
					shipPos = i;
				}
			}
		}
		
		if(GUI.Button(new Rect(Screen.width-Screen.width/4-Screen.width/10,Screen.height-Screen.height/4,Screen.width/4,Screen.height/7),switchShipTex2,GUIStyle.none)){
			props[shipSelected].SetActive(false);
			shipSelected++;
			if(shipSelected > 3){
				shipSelected = 0;
			}
			props[shipSelected].SetActive(true);
			price = props[shipSelected].GetComponent<Spaceship_Player>().shipValue;
			hasShip = false;
			
			for(int i = 0 ; i < script.hangar.shipTypes.Count ; i++){
				if(ships[shipSelected] == script.hangar.shipTypes[i]){
					hasShip = true;
					shipPos = i;
				}
			}
			
		}

		if(hasShip){
			if(script.hangar.shipUpgrade1[shipPos] < 3 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1)){
				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2),Screen.width/4,Screen.height/7), upgradeHealthTex, GUIStyle.none ))
				{
					script.hangar.shipUpgrade1[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1).ToString(), myGUIStyle);
			}
			if(script.hangar.shipUpgrade2[shipPos] < 3 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1)){
				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+(Screen.height/5),Screen.width/4,Screen.height/7), upgradeShieldTex, GUIStyle.none ))
				{
					script.hangar.shipUpgrade2[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+(Screen.height/5),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.shipUpgrade2[shipPos]+1).ToString(), myGUIStyle);

			}
			if(script.hangar.shipUpgrade3[shipPos] < 3 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1)){
				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+2*(Screen.height/5),Screen.width/4,Screen.height/7), upgradeSpeedTex, GUIStyle.none ))
				{
					script.hangar.shipUpgrade3[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+2*(Screen.height/5),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.shipUpgrade3[shipPos]+1).ToString(), myGUIStyle);
			}
		}else {
			if(script.credits > price){
				if(GUI.Button(new Rect(Screen.width/2 - Screen.width/8,Screen.height/2-Screen.height/14,Screen.width/4,Screen.height/7),buyShipTex, GUIStyle.none))
				{
					script.hangar.addToShipUpgrades();
					script.hangar.addSpaceshipToHangar(ships[shipSelected]);
					script.credits -= price;
					hasShip = true;
					shipPos = shipPos + 1;
				}
				GUI.Box (new Rect(Screen.width/2 - Screen.width/8,Screen.height/2-Screen.height/14,Screen.width/4,Screen.height/7), price.ToString(), myGUIStyle);

			}
			
		}
		
		
		if(GUI.Button(new Rect(0,0,Screen.width/4,Screen.height/7),backTex,GUIStyle.none)){
			completed = true;
			closeLevel();
		}
	}
}
