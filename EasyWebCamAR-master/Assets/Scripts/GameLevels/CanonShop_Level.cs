using UnityEngine;
using System.Collections;

public class CanonShop_Level : LevelScript_Base {
	
	private int canonSelected = 0;
	private string[] canons = new string[4];
	private bool hasGun = false;
	private int gunPos = 0;
	private int price;


	// textures for the interface:
	public Texture switchCanTex;
	public Texture switchCanTex2;
	public Texture upgradeDamTex;
	public Texture upgradeMagTex;
	public Texture upgradeRateTex;
	public Texture buyCannonTex;
	private GUIStyle myGUIStyle = new GUIStyle();
	
	public override void loadLevel()
	{
		// finds the texture for the buttons
		backTex = Resources.Load("Interface/CannonShop/Back button") as Texture;
		switchCanTex = Resources.Load("Interface/CannonShop/<Switch Cannon") as Texture;
		switchCanTex2 = Resources.Load("Interface/CannonShop/Switch cannon>") as Texture;
		upgradeDamTex = Resources.Load("Interface/CannonShop/Upgrade Damage") as Texture;
		upgradeMagTex = Resources.Load("Interface/CannonShop/Upgrade Magasin Capacity") as Texture;
		upgradeRateTex = Resources.Load("Interface/CannonShop/Upgrade Rate Of fire") as Texture;
		buyCannonTex = Resources.Load("Interface/CannonShop/BuyCannon") as Texture;
		// sets the font style for the price, which varies
		myGUIStyle.normal.textColor =  new Color(0F, 1F, 3F, 0.6F);
		myGUIStyle.fontSize = 15;
		myGUIStyle.alignment = TextAnchor.MiddleCenter;

		canons[0] = "projectileCanon";
		canons[1] = "ionCanon";
		canons[2] = "projectileCanon";
		canons[3] = "ionCanon";
		
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		
		GameObject background = GameObject.Find("ImageTarget");
		
		for (int i = 0; i < 4; i++){
			
			string newProp = canons[i];
			Vector3 newScale = new Vector3(100,100,100);
			Vector3 newPosition = new Vector3(0,-20,0);
			Vector3 newRotation = new Vector3(0,0,90);
			createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
			
			if(i == canonSelected){
				props[i].SetActive(true);
			}else{
				props[i].SetActive(false);
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
		
		
	}
	
	public override void updateLevel()
	{
		if(!completed ){
			price = props[canonSelected].GetComponent<Weapons_Base>().weaponValue;
		}else{
			
		}
	}
	
	private int calcUpgradePrice(int modify){
		return (int)(price * (modify/10.0f));
	}
	
	public override void levelGUI(){
		if(GUI.Button(new Rect(Screen.width/10,Screen.height-Screen.height/4,Screen.width/4,Screen.height/7),switchCanTex,GUIStyle.none)){
			props[canonSelected].SetActive(false);
			canonSelected--;
			if(canonSelected < 0){
				canonSelected = 3;
			}
			props[canonSelected].SetActive(true);
			price = props[canonSelected].GetComponent<Weapons_Base>().weaponValue;
			
			hasGun = false;
			
			for(int i = 0 ; i < script.hangar.canonTypes.Count ; i++){
				if(canons[canonSelected] == script.hangar.canonTypes[i]){
					hasGun = true;
					gunPos = i;
				}
			}
		}
		
		if(GUI.Button(new Rect(Screen.width-Screen.width/4-Screen.width/10,Screen.height-Screen.height/4,Screen.width/4,Screen.height/7),switchCanTex2,GUIStyle.none)){
			props[canonSelected].SetActive(false);
			canonSelected++;
			if(canonSelected > 3){
				canonSelected = 0;
			}
			props[canonSelected].SetActive(true);
			price = props[canonSelected].GetComponent<Weapons_Base>().weaponValue;
			
			hasGun = false;
			
			for(int i = 0 ; i < script.hangar.canonTypes.Count ; i++){
				if(canons[canonSelected] == script.hangar.canonTypes[i]){
					hasGun = true;
					gunPos = i;
				}
			}
			
		}

		if(hasGun){
			if(script.hangar.canonUpgrade1[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1)){

				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2),Screen.width/4,Screen.height/7),upgradeRateTex, GUIStyle.none))
				{
					script.hangar.canonUpgrade1[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade1[gunPos]);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1).ToString(), myGUIStyle);
			}
			if(script.hangar.canonUpgrade2[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1)){
				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+(Screen.height/5),Screen.width/4,Screen.height/7),upgradeDamTex, GUIStyle.none ))
				{
					script.hangar.canonUpgrade2[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade2[gunPos]);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+(Screen.height/5),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1).ToString(), myGUIStyle);

			}
			if(script.hangar.canonUpgrade3[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1)){
				if(GUI.Button(new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+2*(Screen.height/5),Screen.width/4,Screen.height/7),upgradeMagTex, GUIStyle.none))
				{
					script.hangar.canonUpgrade3[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade3[gunPos]);
				}
				// the box containing the varying price of the upgrade
				GUI.Box (new Rect(Screen.width - Screen.width/4,((Screen.height/5)/2)+2*(Screen.height/5),Screen.width/4,Screen.height/7), calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1).ToString(), myGUIStyle);

			}
		}else {
			if(script.credits > price){
				if(GUI.Button(new Rect(Screen.width/2 - Screen.width/8,Screen.height/2-Screen.height/14,Screen.width/4,Screen.height/7), buyCannonTex, GUIStyle.none))
				{
					script.hangar.addToCanonUpgrades();
					script.hangar.addGunToHangar(canons[canonSelected]);
					script.credits -= price;
					Debug.Log("Buy Canon");
					hasGun = true;
					gunPos = gunPos+1;
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
