using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_One : LevelScript_Base {
	
	public override void loadLevel( )
	{

		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();

		completed = false;

		Vector3 newScale = new Vector3(1,1,1);
		Vector3 newPosition = new Vector3(0,-20,25);
		Vector3 newRotation = new Vector3(-20,0,0);
		createPlayerSpaceship(script.hangar.hangarslots[script.shipChoise],newScale,newPosition,newRotation,player.transform,true,true);

		string newProp; /*= "MeteorSpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-20,200);
		newRotation = new Vector3(20,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);

		newProp = "EnemySpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-20,200);
		newRotation = new Vector3(20,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);

		newProp = "Sun";
		newScale = new Vector3(50,50,50);
		newPosition = new Vector3(100,-200,1000);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);
		*/
		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(100,-200,1000);
		newRotation = new Vector3(45,0,0);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              player.transform, Color.yellow);


	}
	public override void updateLevel(){
		if(completed){
			closeLevel();
			Spaceship_Player shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
			shipScript.gameObject.SetActive(false);
			shipScript.IsActive = false;

		}
	}


	public override void levelGUI(){
		if(GUI.Button(new Rect(0,0,80,50),"Back")){
			completed = true;
			
		}

	}
}
