using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_One : LevelScript_Level {





	public override void loadLevel( )
	{
		setClassTargets();

		levelNumber = 1;
		howManyEnemies = 50;





		newScale = new Vector3(10,10,10);
		newPosition = new Vector3(0,0,-115);
		newRotation = new Vector3(90,0,0);
		createPlayerSpaceship(script.hangar.hangarslots[script.shipChoise],newScale,newPosition,newRotation,background.transform,true,true);
		shipHealth = shipScr.shipHealth();
		shipShield = shipScr.shipShield();

		numberOfFireButtons = shipScr.CanonMountCapacity;
		loadButtons();	


		newProp = "EnemySpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-6000,-115);
		newRotation = new Vector3(-90,0,180);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		spwnScr = props[0].GetComponent<SpawnControl_Enemy>();

		int[] enemyTypeSelection = new int[20]{		0,0,1,0,0,
													0,1,0,0,1,
													0,0,1,0,1,
													1,0,1,0,1
												};

		spwnScr.setSpawnBase(levelNumber , 100, enemyTypeSelection);
		spwnScr.numberOfEnemies = howManyEnemies;



		newProp = "MeteorSpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-1000,-150);
		newRotation = new Vector3(90,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);



		newProp = "Sun";
		newScale = new Vector3(100,100,100);
		newPosition = new Vector3(0,-9000,0);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);

		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,0,0);
		newRotation = new Vector3(0,180,90);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, Color.yellow);

		newProp = "LevelProps/Vortex1";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,0,-27);
		newRotation = new Vector3(0,90,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);

		newProp = "DepthMaskPlane";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,0,-228);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);

	}




}
