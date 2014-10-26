using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {

	public string[] enemyShipScipt;

	private float objScale = 10f;
	public bool enemySpawning;
	private Vector3 spawnPosition;
	private float portalTime;

	public string enemyType;

	public int enemiesToSpawn;

	public int deadEnemy = 0;

	// Use this for initialization
	public override void Start () {

		enemySpawning = false;
		enemyShipScipt = new string[2];
		enemyShipScipt[0] = "EnemyFirstClass";
		enemyShipScipt[1] = "EnemySecondClass";

		spawnObject = new GameObject[3];
		spawnObject[0] = (GameObject)Resources.Load("xxx");
		spawnObject[1] = (GameObject)Resources.Load("Portal");
		spawnObject[2] = (GameObject)Resources.Load("PortalFog");

		portalTime = 0.66f;
		stackSize = 25;

	}
	public void SpawnWing(Vector3 newPos , int num ){

		switch(num){
		case 0:
			spawnPosition  = new Vector3 (newPos.x ,newPos.y ,newPos.z);
			Spawn();
			break;
		case 1:
			spawnPosition  = new Vector3 (newPos.x + 75 ,newPos.y ,newPos.z);
			Spawn();
			spawnPosition  = new Vector3 (newPos.x - 75 ,newPos.y ,newPos.z);
			Spawn();
			break;
		case 2:
			spawnPosition  = new Vector3 (newPos.x + 150,newPos.y ,newPos.z);
			Spawn();
			spawnPosition  = new Vector3 (newPos.x - 150,newPos.y ,newPos.z);
			Spawn();
			break;
		default:
			break;
		}

	}

	public override  void Spawn()
	{
		GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
		go.transform.localScale = new Vector3 (objScale,objScale,objScale);
		go.transform.position = spawnPosition ;
		go.transform.rotation = transform.rotation;
		int ls = (int)Random.Range(0.5f , 1.5f);
		Debug.Log(ls);
		go.AddComponent(enemyShipScipt[ls]);
		Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
		createdObject.Parent = this;
		createdObject.transform.parent = this.transform;
		enemiesToSpawn--;
		Instantiate(spawnObject[2], spawnPosition,transform.rotation);

	}

}
