using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {


	private int profileSet;
	private int levelNum;

	public string[] enemyShipScipt;

	private float objScale = 5f;
	public bool enemySpawning;
	private Vector3 spawnPosition;
	private float portalTime;

	public string enemyType;

	public int enemiesToSpawn;

	public int deadEnemy = 0;

	private GameObject portal;

	private string[] versionModifier;

	public void forceStart(int leveln){
		levelNum = leveln;
		profileSet = GameObject.Find("ARCamera").GetComponent<Player_Charactor>().gameSetting;
		versionModifier = GameObject.Find("ARCamera").GetComponent<Player_Charactor>().enemyVersion;

		spawnObject = new GameObject[3];
		spawnObject[0] = (GameObject)Resources.Load(versionModifier[0]);
		spawnObject[1] = (GameObject)Resources.Load(versionModifier[1]);
		spawnObject[2] = (GameObject)Resources.Load(versionModifier[2]);
		/*
		if(profileSet == 0){
			spawnObject = new GameObject[3];
			spawnObject[0] = (GameObject)Resources.Load("Enemies/Mustang");
			spawnObject[1] = (GameObject)Resources.Load("Enemies/Needle");
			spawnObject[2] = (GameObject)Resources.Load("Enemies/Spike");
		}else{
			spawnObject = new GameObject[3];
			spawnObject[0] = (GameObject)Resources.Load("Enemies/Mustang");
			spawnObject[1] = (GameObject)Resources.Load("Enemies/Needle");
			spawnObject[2] = (GameObject)Resources.Load("Enemies/Spike");
		}
		*/
		portal = (GameObject)Resources.Load("PortalFog");
		
		enemySpawning = false;
		enemyShipScipt = new string[5];
		enemyShipScipt[0] = "EnemyFirstClass";
		enemyShipScipt[1] = "EnemySecondClass";
		enemyShipScipt[2] = "EnemySecondClass";
		enemyShipScipt[3] = "EnemySecondClass";
		enemyShipScipt[4] = "EnemySecondClass";
		
		portalTime = 0.66f;
		stackSize = 25;
	}
	// Use this for initialization
	/*public override void Start () {

		portal = (GameObject)Resources.Load("PortalFog");

		enemySpawning = false;
		enemyShipScipt = new string[2];
		enemyShipScipt[0] = "EnemyFirstClass";
		enemyShipScipt[1] = "EnemySecondClass";

		spawnObject = new GameObject[1];
		spawnObject[0] = (GameObject)Resources.Load("xxx");


		portalTime = 0.66f;
		stackSize = 25;

	}*/

	public void SpawnWing(Vector3 newPos , int num, int type ){

		switch(num){
		case 0:
			spawnPosition  = new Vector3 (newPos.x ,newPos.y ,newPos.z);
			Spawn(type);
			break;
		case 1:
			spawnPosition  = new Vector3 (newPos.x + 75 ,newPos.y ,newPos.z);
			Spawn(type);
			spawnPosition  = new Vector3 (newPos.x - 75 ,newPos.y ,newPos.z);
			Spawn(type);
			break;
		case 2:
			spawnPosition  = new Vector3 (newPos.x + 150,newPos.y ,newPos.z);
			Spawn(type);
			spawnPosition  = new Vector3 (newPos.x - 150,newPos.y ,newPos.z);
			Spawn(type);
			break;
		default:
			break;
		}

	}

	public override  void Spawn(int type)
	{
		GameObject go = (GameObject)Object.Instantiate(spawnObject[type]);
		go.transform.localScale = new Vector3 (objScale,objScale,objScale);
		go.transform.position = spawnPosition ;
		go.transform.rotation = transform.rotation;
		//int ls = (int)Random.Range(0.5f , 1.5f);
		go.AddComponent(enemyShipScipt[type]);
		Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
		createdObject.forceStart();
		createdObject.modifyEnemy(levelModifier(10), levelModifier(5),(float) levelModifier(5),0f,levelModifier(1));
		createdObject.Parent = this;
		createdObject.transform.parent = this.transform;
		enemiesToSpawn--;
		Instantiate(portal, spawnPosition,transform.rotation);

	}

	private int levelModifier(int modifier){
		return modifier * levelNum;
	}

}
