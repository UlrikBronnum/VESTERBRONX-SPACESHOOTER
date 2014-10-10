using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawn : SpawnClass_Base {

	public Stack<Spaceship_Enemy> enemyStack = new Stack<Spaceship_Enemy>();
	// Use this for initialization
	public override void Start () {

		spawnObject = new GameObject[1];
		spawnObject[0] = (GameObject)Resources.Load("EnemySecondClass");

		stackSize = 50;
		for (int i = 0; i < stackSize; i++){
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.initTimer(15f,Random.Range(-20f,-50f));
			createdObject.transform.Rotate(new Vector3 (0,1,0) * 180);
			createdObject.transform.Rotate(new Vector3 (1,0,0) * 30);
			createdObject.Despawn();
			createdObject.shipInitialization();
			enemyStack.Push(createdObject);
			
		}
		
	}
	public override  void Spawn(){
		if(enemyStack.Count > 0){
			Spaceship_Enemy newObject = enemyStack.Pop();
			newObject.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			newObject.gameObject.transform.position = new Vector3 (transform.position.x + Random.Range(-50f,50f),transform.position.y,transform.position.z);
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.transform.Rotate(new Vector3 (0,1,0) * 180);
			newObject.transform.Rotate(new Vector3 (1,0,0) * 30);
			newObject.initTimer(15f,Random.Range(-20f,-50f));
			newObject.Spawn();
		}else{
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			go.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			go.transform.position = new Vector3 (transform.position.x + Random.Range(-50f,50f),transform.position.y,transform.position.z);
			go.transform.rotation = transform.rotation;
			Spaceship_Enemy createdObject = go.GetComponent<Spaceship_Enemy>();
			createdObject.initTimer(15f,Random.Range(-20f,-50f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.transform.Rotate(new Vector3 (0,1,0) * 180);
			createdObject.transform.Rotate(new Vector3 (1,0,0) * 30);
		}
	}

}
