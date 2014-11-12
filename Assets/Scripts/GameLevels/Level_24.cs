﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_24 : LevelScript_Level {
	
	
	
	public override int getLevelNumber()
	{
		levelNumber = 24;
		return levelNumber;
	}
	
	public override void loadLevel( )
	{
		levelNumber = getLevelNumber();
		
		howManyEnemies = 55;
		
		setClassTargets();
		
		
		newScale = new Vector3(5,5,5);
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
		
		int[] enemyTypeSelection = new int[11]{		1,3,3,2,3,3,1,1,2,2,3
		};
		
		spwnScr.setSpawnBase(levelNumber , howManyEnemies, enemyTypeSelection, 4.5f);

		
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
		                              background.transform, Color.white);
		
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
