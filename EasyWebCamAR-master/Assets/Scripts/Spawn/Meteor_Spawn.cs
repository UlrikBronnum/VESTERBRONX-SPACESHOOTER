using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Meteor_Spawn : SpawnClass_Base {

	public Stack<UFO_Base>  meteorStack = new Stack<UFO_Base>();
	// Use this for initialization
	public override void Start () {

		spawnObject = new GameObject[1];
		spawnObject[0] = (GameObject)Object.Instantiate(Resources.Load("Meteor"));
		stackSize = 50;
		
		for (int i = 0; i < stackSize; i++){
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			UFO_Base createdObject = go.GetComponent<UFO_Base>();
			createdObject.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
			createdObject.initTimer(15f,Random.Range(-20f,-50f));
			createdObject.Despawn();
			meteorStack.Push(createdObject);
			
		}
		
	}
	public override void Spawn(){
		if(meteorStack.Count > 0){
			UFO_Base newObject = meteorStack.Pop();
			newObject.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			newObject.gameObject.transform.position = new Vector3 (transform.position.x + Random.Range(-50f,50f),transform.position.y,transform.position.z);
			newObject.gameObject.transform.rotation = transform.rotation;
			newObject.initTimer(15f,Random.Range(-20f,-50f));
			newObject.Spawn();
		}else{
			GameObject go = (GameObject)Object.Instantiate(spawnObject[0]);
			go.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			go.transform.position = new Vector3 (transform.position.x + Random.Range(-50f,50f),transform.position.y,transform.position.z);
			go.transform.rotation = transform.rotation;
			UFO_Base createdObject = go.GetComponent<UFO_Base>();
			createdObject.initTimer(15f,Random.Range(-20f,-50f));
			createdObject.Parent = this;
			createdObject.transform.parent = this.transform;
		}
	}

}
