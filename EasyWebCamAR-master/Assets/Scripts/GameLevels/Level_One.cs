using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_One : LevelScript_Level {

	public Texture lifeRemainingTexture;
	public Texture lifeRemainingBehindTexture;
	
	string endGame;
	float _unLoadTimer = 5f;
	int shipHealth;
	int shipShield;
	int gain;

	//Make these nice

	private float backGroundWidthLife;
	private float lifePercent;
	private float lifeWidth;
	private float left;
	private float top;
	private float height;

	public override void loadLevel( )
	{
		setClassTargets();
		loadButtons();	

		levelNumber = 1;
		howManyEnemies = 50;





		newScale = new Vector3(10,10,10);
		newPosition = new Vector3(0,0,-20);
		newRotation = new Vector3(90,0,0);
		createPlayerSpaceship(script.hangar.hangarslots[script.shipChoise],newScale,newPosition,newRotation,image.transform,true,true);
		shipHealth = shipScr.Health;
		shipShield = shipScr.Shield ;

		newProp = "EnemySpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-4000,-20);
		newRotation = new Vector3(-90,0,180);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);
		spwnScr = props[0].GetComponent<SpawnControl_Enemy>();
		spwnScr.numberOfEnemies = howManyEnemies;
		spwnScr.setSpawnBase();



		newProp = "MeteorSpawn";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,-1000,-100);
		newRotation = new Vector3(90,0,0);
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
		newProp = "Vortex";
		newScale = new Vector3(1,1,1);
		newPosition = new Vector3(0,2,0);
		newRotation = new Vector3(0,90,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);

	}
	public override void updateLevel(){



		if(useAxisInput) {
			// assigns the position of the joystick to h and v
			joystickInput = joystick.position.x;
		}
		else {
			joystickInput = Input.GetAxis("Horizontal");
		}

		sentButtonInput();

		 
		if (spwnScr.spawnEmpty){
			SpawnControl_Enemy tmpscr =  props[0].GetComponent<SpawnControl_Enemy>();
			enemiesDestroyed = tmpscr.EnemyDead;
			
			if( (float)enemiesDestroyed/howManyEnemies > 0.6f){
				endGame = "Complete";
				gain = priceCreditsTotal();
			}else {
				endGame = "Fail";
				gain = 0;
			}
			
			_unLoadTimer -= Time.deltaTime * 1f;
			
			if(_unLoadTimer < 0){
				if( (float)enemiesDestroyed/howManyEnemies > 0.6f){
					script.credits += priceCreditsTotal();
					
				}
				
				completed = true;	
				closeLevel();
				unloadButtons();
				Spaceship_Player shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
				shipScript.gameObject.SetActive(false);
				shipScript.IsActive = false;
			}
			
		}
	}


	public override void levelGUI(){

		if(spwnScr.spawnEmpty){
			GUI.TextField(new Rect(Screen.width/2 -Screen.width/8,Screen.height - Screen.height/4,Screen.width/4,Screen.height/4), endGame + "\nEnemy Kills: " + enemiesDestroyed.ToString() + " / " + howManyEnemies.ToString() + "\nCredits: " + gain);
		}

		left = Screen.width / 2;
		top = 8F;
		backGroundWidthLife = Screen.width / 4;
		lifeWidth = lifePercent * backGroundWidthLife;
		height = 12F;

		GUI.Box (new Rect ((Screen.width / 2) + (Screen.width / 4), top, Screen.width / 10, Screen.height / 40), "Health: " + shipHealth.ToString() + "/" + shipShield.ToString());
		GUI.DrawTexture (new Rect (left, top, backGroundWidthLife, height), lifeRemainingBehindTexture, ScaleMode.StretchToFill, true, 1.0F);
		GUI.DrawTexture (new Rect (left, top, lifeWidth, height), lifeRemainingTexture, ScaleMode.StretchToFill, true, 1.0F);

	}
}
