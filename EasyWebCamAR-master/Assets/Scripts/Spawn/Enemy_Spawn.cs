using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {

	public Stack<Spaceship_Enemy> enemyStack = new Stack<Spaceship_Enemy>();
	private float objScale = 10f;
	// Use this for initialization
	public override void Start () {

		spawnObject = new GameObject[1];
		spawnObject[0] = (GameObject)Resources.Load("EnemySecondClass");

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
	public override  void Spawn(){
		if(enemyStack.Count > 0){
			Spaceship_Enemy newObject = enemyStack.Pop();
			newObject.transform.localScale = new Vector3 (objScale,objScale,objScale);
			newObject.gameObject.transform.position = new Vector3 (transform.position.x + Random.Range(-150f,150f),transform.position.y,transform.position.z);
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.resetTimer();
			newObject.Spawn();
		}else{
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			go.transform.localScale = new Vector3 (objScale,objScale,objScale);
			go.transform.position = new Vector3 (transform.position.x + Random.Range(-150f,150f),transform.position.y,transform.position.z);
			go.transform.rotation = transform.rotation;
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.initTimer(5f);
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
		}
	}

}
