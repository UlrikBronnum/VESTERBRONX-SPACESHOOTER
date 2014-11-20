using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_8 : LevelScript_Level {
	
	
	
	public override int getLevelNumber()
	{
		levelNumber = 8;
		return levelNumber;
	}
	
	public override void loadLevel( )
	{
		levelNumber = getLevelNumber();
		
		howManyEnemies = 30;
		
		setClassTargets();
		
		
		newScale = new Vector3(7,7,7);
		newPosition = new Vector3(0,32.5f,-115);
		newRotation = new Vector3(90,0,0);
		createPlayerSpaceship(script.hangar.hangarslots[script.shipChoise],newScale,newPosition,newRotation,background.transform,true,true);
		shipHealth = shipScr.shipHealth();
		shipShield = shipScr.shipShield();
		
		numberOfFireButtons = shipScr.CanonMountCapacity;
		loadButtons();
		
		newProp = "EnemySpawn";
		newScale = new Vector3(1,1,1);
		newRotation = new Vector3(-90,0,180);
		createSceneObject(newProp,newScale,spawnPoint,newRotation,background.transform);
		spwnScr = props[0].GetComponent<SpawnControl_Enemy>();
		
		int[] enemyTypeSelection = new int[6]{		0,1,2,1,3,3
		}; 
		
		spwnScr.setSpawnBase(levelNumber , howManyEnemies, enemyTypeSelection, 5.5f);

		newProp = "LevelProps/Particle System";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-1450,0);
		newRotation = new Vector3(0,90,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,background.transform);
		
	
		
		newProp = "SunLight";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,0,0);
		newRotation = new Vector3(0,180,90);
		createDirectionalLightInScene(newProp,newScale,newPosition ,newRotation,
		                              background.transform, new Color (0.8f,0.3f,0.0f,1.0f));
		
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
