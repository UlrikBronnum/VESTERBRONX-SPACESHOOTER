using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_One : LevelScript_Base {

	public GameObject[] button ;
	public AButton[] buttonScript;
	protected Spaceship_Player shipScr ;
	private string cameraName = "ARCamera";

	protected override void loadButtons(){
		button  = new GameObject[3];
		buttonScript = new AButton[3];
		button[0] = (GameObject)Object.Instantiate(Resources.Load ("AButton"));
		button[0].SetActive(true);
		buttonScript[0] = button[0].GetComponent<AButton>();
		button[1] = (GameObject)Object.Instantiate(Resources.Load ("Red_Arrow_Left"));
		button[1].SetActive(true);
		buttonScript[1] = button[1].GetComponent<AButton>();
		button[2] = (GameObject)Object.Instantiate(Resources.Load ("Red_Arrow_Right"));
		button[2].SetActive(true);
		buttonScript[2] = button[2].GetComponent<AButton>();
	}
	protected override void unloadButtons(){
		for(int i = 0; i < 3 ; i++){
			Destroy(button[i]);
		}
	}

	protected void sentButtonInput(){
		shipScr.getButtonInput(buttonScript[0].touch, buttonScript[1].touch, buttonScript[2].touch);
	}


	public override void loadLevel( )
	{

		player = GameObject.Find(cameraName);
		script = player.GetComponent<Player_Charactor>();
		shipScr = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();

		completed = false;

		GameObject image = GameObject.Find("ImageTarget");

		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;


		loadButtons();	
		
		newScale = new Vector3(10,10,10);
		newPosition = new Vector3(0,0,-20);
		newRotation = new Vector3(90,0,0);
		createPlayerSpaceship(script.hangar.hangarslots[script.shipChoise],newScale,newPosition,newRotation,image.transform,true,true);
		

		string newProp = "MeteorSpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-1000,-20);
		newRotation = new Vector3(90,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);

		newProp = "EnemySpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-2000,-20);
		newRotation = new Vector3(-90,0,180);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);

		newProp = "Sun";
		newScale = new Vector3(100,100,100);
		newPosition = new Vector3(0,-3000,0);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);

		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-3000,0);
		newRotation = new Vector3(0,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              image.transform, Color.yellow);


	}
	public override void updateLevel(){

		sentButtonInput();

	}


	public override void levelGUI(){
		if(GUI.Button(new Rect(0,0,200,100),"Back")){
			completed = true;	
			closeLevel();
			unloadButtons();
			Spaceship_Player shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
			shipScript.gameObject.SetActive(false);
			shipScript.IsActive = false;
		}

	}
}
