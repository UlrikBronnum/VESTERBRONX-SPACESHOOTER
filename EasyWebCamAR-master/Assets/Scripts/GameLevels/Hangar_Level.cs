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

	public override void loadLevel()
	{
		player = GameObject.Find(cameraName);
		script = player.GetComponent<Player_Charactor>();
		shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();

		completed = false;

		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;

		newScale = new Vector3(30,30,30);
		newPosition = new Vector3(0,50,0);
		newRotation = new Vector3(90,180,0);
		GameObject background = GameObject.Find("ImageTarget");

		
		for (int i = 0; i < script.hangar.hangarslots.Count; i++){
			if(i == script.shipChoise){
				createPlayerSpaceship(script.hangar.hangarslots[i],newScale,newPosition,newRotation,background.transform,false,true);
			}else{
				createPlayerSpaceship(script.hangar.hangarslots[i],newScale,newPosition,newRotation,background.transform,false,false);
			}
		}

		string newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,15,-15);
		newRotation = new Vector3(45,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.yellow);

	}

	public override void updateLevel()
	{
		if(completed){
			closeLevel();
		}else{
			shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
			canonLimit = shipScript.CanonMountCapacity;
		}
	}
	public override void levelGUI(){
		//
		if(GUI.Button(new Rect(Screen.width/10 * 1,Screen.height/10 * 9 ,200,100),"Switch Ship -")){

			script.hangar.hangarslots[script.shipChoise].SetActive(false);
			script.shipChoise--;
			if(script.shipChoise < 0){
				script.shipChoise = script.hangar.hangarslots.Count-1;
			}
			script.hangar.hangarslots[script.shipChoise].SetActive(true);
		}

		if(GUI.Button(new Rect(Screen.width/10 *9-100,Screen.height/10 * 9 ,200,100),"Switch Ship + ")){

			script.hangar.hangarslots[script.shipChoise].SetActive(false);
			script.shipChoise++;
			if(script.shipChoise > script.hangar.hangarslots.Count-1){
				script.shipChoise = 0;
			}
			script.hangar.hangarslots[script.shipChoise].SetActive(true);
		}



		if(canonLimit >= 2){
			if(GUI.Button(new Rect(Screen.width/4 + (Screen.width/10 * 4)-100,Screen.height/4 ,200,100),"GunMounts 1"))
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
		}
		if(canonLimit >= 4)
		{
			if(GUI.Button(new Rect(Screen.width/4 + (Screen.width/10 * 4)+100,Screen.height/4 ,200,100),"GunMounts 2"))
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
		}
		if(GUI.Button(new Rect(0,0,200,100),"Back")){
			completed = true;
			for(int i = 0 ; i < script.hangar.hangarslots.Count; i++){
				Spaceship_Player ship = script.hangar.hangarslots[i].GetComponent<Spaceship_Player>();
				ship.IsActive = false;
				ship.gameObject.SetActive(false);
			}


		}
	}
}
