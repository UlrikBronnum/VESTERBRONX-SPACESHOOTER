using UnityEngine;
using System.Collections;

public class AmmunitionShop_Level : LevelScript_Base {

	private int canonSelected = 0;
	private string[] canons = new string[4];

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



	}
	
	public override void updateLevel()
	{
		if(!completed ){
			
		}else{

		}
	}
	public override void levelGUI(){
		if(GUI.Button(new Rect(Screen.width/10 * 1,Screen.height/10 * 9 ,200,100),"Switch Canon -")){
			props[canonSelected].SetActive(false);
			canonSelected--;
			if(canonSelected < 0){
				canonSelected = 3;
			}
			props[canonSelected].SetActive(true);
		}
		
		if(GUI.Button(new Rect(Screen.width/10 *9-100,Screen.height/10 * 9 ,200,100),"Switch Canon + ")){
			props[canonSelected].SetActive(false);
			canonSelected++;
			if(canonSelected > 3){
				canonSelected = 0;
			}
			props[canonSelected].SetActive(true);
			
		}

		if(GUI.Button(new Rect(Screen.width/10 *9-100,Screen.height/10 * 1 ,200,100),"Buy Canon"))
		{
			script.hangar.addGunToHangar(canons[canonSelected]);
			script.hangar.addToCanonUpgrades();
		}
		if(GUI.Button(new Rect(0,0,200,100),"Back")){
			completed = true;
			closeLevel();
		}
	}
}
