using UnityEngine;
using System.Collections;

public class SpaceshipShop_Level : LevelScript_Base {
	
	private int shipSelected = 0;
	private string[] ships = new string[4];
	private bool hasShip = false;
	private int shipPos = 0;
	
	
	private int price;
	
	public override void loadLevel()
	{
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
			Debug.Log(price );
		}else{
			
		}
	}
	
	private int calcUpgradePrice(int modify){
		return (int)(price * (modify/10.0f));
	}
	
	public override void levelGUI(){
		if(GUI.Button(new Rect(Screen.width/10 * 1,Screen.height/10 * 9 ,200,100),"Switch Ship -")){
			props[shipSelected].SetActive(false);
			shipSelected--;
			if(shipSelected < 0){
				shipSelected = 3;
			}
			props[shipSelected].SetActive(true);
			price = props[shipSelected].GetComponent<Spaceship_Player>().shipValue;
			Debug.Log(price );
			hasShip = false;
			
			for(int i = 0 ; i < script.hangar.shipTypes.Count ; i++){
				if(ships[shipSelected] == script.hangar.shipTypes[i]){
					hasShip = true;
					shipPos = i;
				}
			}
		}
		
		if(GUI.Button(new Rect(Screen.width/10 *9-100,Screen.height/10 * 9 ,200,100),"Switch Ship + ")){
			props[shipSelected].SetActive(false);
			shipSelected++;
			if(shipSelected > 3){
				shipSelected = 0;
			}
			props[shipSelected].SetActive(true);
			price = props[shipSelected].GetComponent<Spaceship_Player>().shipValue;
			Debug.Log(price );
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
				if(GUI.Button(new Rect(Screen.width/2 - 200,0 ,200,100),"Upgrade: Rate of fire" + "\n" + "Price: " + calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1) ))
				{
					script.hangar.shipUpgrade1[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
					Debug.Log(script.hangar.shipUpgrade1[shipPos]);
				}
			}
			if(script.hangar.shipUpgrade2[shipPos] < 3 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1)){
				if(GUI.Button(new Rect(Screen.width/2 ,0,200,100),"Upgrade: Damage" + "\n"  + "Price: " + calcUpgradePrice(script.hangar.shipUpgrade2[shipPos]+1) ))
				{
					script.hangar.shipUpgrade2[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
					Debug.Log(script.hangar.shipUpgrade2[shipPos]);
				}
			}
			if(script.hangar.shipUpgrade3[shipPos] < 3 && script.credits > calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1)){
				if(GUI.Button(new Rect(Screen.width/2 + 200, 0 ,200,100),"Upgrade: Magasin Capacity" + "\n" + "Price: " + calcUpgradePrice(script.hangar.shipUpgrade3[shipPos]+1)))
				{
					script.hangar.shipUpgrade3[shipPos]++;
					script.credits -= calcUpgradePrice(script.hangar.shipUpgrade1[shipPos]+1);
					Debug.Log(script.hangar.shipUpgrade3[shipPos]);
				}
			}
		}else {
			if(script.credits > price){
				if(GUI.Button(new Rect(Screen.width/2 - 200,0 ,200,100),"Buy Ship" + "\n" + "Price: " + price))
				{
					script.hangar.addToShipUpgrades();
					script.hangar.addSpaceshipToHangar(ships[shipSelected]);
					script.credits -= price;
					Debug.Log("Buy Ship");
					hasShip = true;
					shipPos = shipPos + 1;
				}
			}
			
		}
		
		
		if(GUI.Button(new Rect(0,0,200,100),"Back")){
			completed = true;
			closeLevel();
		}
	}
}
