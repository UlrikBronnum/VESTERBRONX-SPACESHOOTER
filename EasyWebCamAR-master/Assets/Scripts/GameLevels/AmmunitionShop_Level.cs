using UnityEngine;
using System.Collections;

public class AmmunitionShop_Level : LevelScript_Base {

	private int canonSelected = 0;
	private string[] canons = new string[4];
	private bool hasGun = false;
	private int gunPos = 0;


	private int price;

	public override void loadLevel()
	{
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
		if(GUI.Button(new Rect(Screen.width/10 * 1,Screen.height/10 * 9 ,200,100),"Switch Canon -")){
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
		
		if(GUI.Button(new Rect(Screen.width/10 *9-100,Screen.height/10 * 9 ,200,100),"Switch Canon + ")){
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
				if(GUI.Button(new Rect(Screen.width/2 - 200,0 ,200,100),"Upgrade: Rate of fire" + "\n" + "Price: " + calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1) ))
				{
					script.hangar.canonUpgrade1[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade1[gunPos]);
				}
			}
			if(script.hangar.canonUpgrade2[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1)){
				if(GUI.Button(new Rect(Screen.width/2 ,0,200,100),"Upgrade: Damage" + "\n"  + "Price: " + calcUpgradePrice(script.hangar.canonUpgrade2[gunPos]+1) ))
				{
					script.hangar.canonUpgrade2[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade2[gunPos]);
				}
			}
			if(script.hangar.canonUpgrade3[gunPos] < 3 && script.credits > calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1)){
				if(GUI.Button(new Rect(Screen.width/2 + 200, 0 ,200,100),"Upgrade: Magasin Capacity" + "\n" + "Price: " + calcUpgradePrice(script.hangar.canonUpgrade3[gunPos]+1)))
				{
					script.hangar.canonUpgrade3[gunPos]++;
					script.credits -= calcUpgradePrice(script.hangar.canonUpgrade1[gunPos]+1);
					Debug.Log(script.hangar.canonUpgrade3[gunPos]);
				}
			}
		}else {
			if(script.credits > price){
				if(GUI.Button(new Rect(Screen.width/2 - 200,0 ,200,100),"Buy Canon" + "\n" + "Price: " + price))
				{
					script.hangar.addGunToHangar(canons[canonSelected]);
					script.hangar.addToCanonUpgrades();
					script.credits -= price;
					Debug.Log("Buy Canon");
					hasGun = true;
				}
			}

		}


		if(GUI.Button(new Rect(0,0,200,100),"Back")){
			completed = true;
			closeLevel();
		}
	}
}
