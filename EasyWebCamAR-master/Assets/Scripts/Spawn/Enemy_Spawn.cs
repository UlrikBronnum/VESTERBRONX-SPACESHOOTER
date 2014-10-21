using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {

	public Stack<Spaceship_Enemy> enemyStack = new Stack<Spaceship_Enemy>();
	private float objScale = 10f;
	public bool enemySpawning;
	private Vector3 spawnPosition;
	private float portalTime;

	// Use this for initialization
	public override void Start () {
		enemySpawning = false;
		spawnObject = new GameObject[3];
		spawnObject[0] = (GameObject)Resources.Load("EnemySecondClass");
		spawnObject[1] = (GameObject)Resources.Load("Portal");
		spawnObject[2] = (GameObject)Resources.Load("PortalFog");

		portalTime = 0.66f;
		stackSize = 10;

		for (int i = 0; i < stackSize; i++){
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.transform.localScale = new Vector3 (objScale,objScale,objScale);
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.initTimer(5f);
			createdObject.Despawn();
			createdObject.shipInitialization();
			enemyStack.Push(createdObject);
			
		}
		
	}
	public override void SpawnPortal(){
		if(!enemySpawning){
			spawnPosition  = new Vector3 (transform.position.x + Random.Range(-150f,150f),transform.position.y,transform.position.z);
			Instantiate (spawnObject[2], new Vector3 (spawnPosition.x,spawnPosition.y + 5,spawnPosition.z) , Quaternion.identity);
			enemySpawning = true;
		}

		portalTime -= Time.deltaTime;
		if ( portalTime < 0) {
			Instantiate (spawnObject[1],spawnPosition , Quaternion.identity);
			portalTime = 0.25f;
		}

	}
	public override  void Spawn()
	{

		enemySpawning = false;
		if(enemyStack.Count > 0){
			Spaceship_Enemy newObject = enemyStack.Pop();
			newObject.transform.localScale = new Vector3 (objScale,objScale,objScale);

			newObject.gameObject.transform.position = spawnPosition;
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.resetTimer();
			newObject.Spawn();
		}else{
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			go.transform.localScale = new Vector3 (objScale,objScale,objScale);
			//spawnPosition  = new Vector3 (transform.position.x + Random.Range(-150f,150f),transform.position.y,transform.position.z);
			go.transform.position = spawnPosition ;
			go.transform.rotation = transform.rotation;
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.initTimer(5f);
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
		}
	}

}
