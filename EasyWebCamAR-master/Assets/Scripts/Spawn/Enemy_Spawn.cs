using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {

	public Stack<Spaceship_Enemy> enemyStack = new Stack<Spaceship_Enemy>();
	private float objScale = 10f;
	public bool enemySpawning;
	private Vector3 spawnPosition;
	private float portalTime;

	public int enemiesToSpawn;

	public int deadEnemy = 0;

	// Use this for initialization
	public override void Start () {
		enemySpawning = false;
		spawnObject = new GameObject[3];
		spawnObject[0] = (GameObject)Resources.Load("EnemySecondClass");
		spawnObject[1] = (GameObject)Resources.Load("Portal");
		spawnObject[2] = (GameObject)Resources.Load("PortalFog");

		portalTime = 0.66f;
		stackSize = 25;
		/*
		for (int i = 0; i < stackSize; i++){
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.transform.localScale = new Vector3 (objScale,objScale,objScale);
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.Despawn();
			createdObject.shipInitialization();
			enemyStack.Push(createdObject);
			
		}
		*/
		
	}
	public void SpawnWing(Vector3 newPos , int num){

		Vector3 holdPosition = newPos;
		switch(num){
		case 0:
			spawnPosition  = new Vector3 (holdPosition.x ,holdPosition.y ,holdPosition.z);
			Spawn();
			break;
		case 1:
			spawnPosition  = new Vector3 (holdPosition.x + 75 ,holdPosition.y ,holdPosition.z);
			Spawn();
			spawnPosition  = new Vector3 (holdPosition.x - 75 ,holdPosition.y ,holdPosition.z);
			Spawn();
			break;
		case 2:
			spawnPosition  = new Vector3 (holdPosition.x + 150,holdPosition.y ,holdPosition.z);
			Spawn();
			spawnPosition  = new Vector3 (holdPosition.x - 150,holdPosition.y ,holdPosition.z);
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
		Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
		createdObject.Parent = this;
		createdObject.transform.parent = this.transform;
		enemiesToSpawn--;

		Instantiate(spawnObject[2], spawnPosition,transform.rotation);
		/*
		if(enemyStack.Count > 0){
			Spaceship_Enemy newObject = enemyStack.Pop();
			newObject.transform.localScale = new Vector3 (objScale,objScale,objScale);
			newObject.gameObject.transform.position = spawnPosition;
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.Spawn();
			enemiesToSpawn--;
		}
		else{


		}
		*/

	}

}
