using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Meteor_Spawn : SpawnClass_Base {

	public Stack<UFO_Base>  meteorStack = new Stack<UFO_Base>();
	// Use this for initialization
	public override void Start () {

		spawnObject = new GameObject[2];
		spawnObject[0] = (GameObject)Resources.Load("LevelProps/Meteor");
		spawnObject[1] = (GameObject)Resources.Load("LevelProps/meteor_sign");
		stackSize = 100;

		
		for (int i = 0; i < stackSize; i++){
			GameObject go = (GameObject)Object.Instantiate(spawnObject[(int)Random.Range(0.5f , 1.5f)]);
			UFO_Base createdObject = go.GetComponent<UFO_Base>();
			createdObject.transform.localScale = new Vector3 (Random.Range(3f,5f),Random.Range(3f,5f),Random.Range(3f,5f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.initTimer(10f,Random.Range(-300f,-500f));
			createdObject.Despawn();
			meteorStack.Push(createdObject);
			
		}
		
	}
	public override void Spawn(int type){
		if(meteorStack.Count > 0){
			UFO_Base newObject = meteorStack.Pop();
			newObject.transform.localScale = new Vector3 (Random.Range(3f,5f),Random.Range(3f,5f),Random.Range(3f,5f));
			newObject.gameObject.transform.position = new Vector3 (transform.position.x + Random.Range(-250f,250f),transform.position.y,transform.position.z);
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.resetTimer();
			newObject.Spawn();
		}else{
			GameObject go = (GameObject)Object.Instantiate(spawnObject[type]);
			go.transform.localScale = new Vector3 (Random.Range(3f,3f),Random.Range(3f,5f),Random.Range(3f,5f));
			go.transform.position = new Vector3 (transform.position.x + Random.Range(-250f,250f),transform.position.y,transform.position.z);
			go.transform.rotation = transform.rotation;
			UFO_Base createdObject = go.GetComponent<UFO_Base>();
			createdObject.initTimer(5f,Random.Range(-300f,-500f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
		}
	}

}
