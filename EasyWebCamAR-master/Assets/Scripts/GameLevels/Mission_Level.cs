using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission_Level : LevelScript_Base {



	public override void loadLevel()
	{
		player = GameObject.Find("Main Camera");
		script = player.GetComponent<Player_Charactor>();

		string newProp = "SolarSystem";
		Vector3 newScale = new Vector3(20,20,20);
		Vector3 newPosition = new Vector3(0,-50,200);
		Vector3 newRotation = new Vector3(125,-30,-75);
		createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);
		Solar_System solarSystem = props[0].GetComponent<Solar_System>();
//		script.loadSolarSystem();
	}

	public override void updateLevel()
	{
		if(!completed ){

		}else{
			closeLevel();
		}
	}
	public override void levelGUI(){

	}
}
