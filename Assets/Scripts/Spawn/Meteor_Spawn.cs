using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Meteor_Spawn : SpawnClass_Base {

	// Use this for initialization
	public override void Start () {

		spawnObject = new GameObject[3];
		spawnObject[0] = (GameObject)Resources.Load("LevelProps/Meteor");
		spawnObject[1] = (GameObject)Resources.Load("LevelProps/meteor_sign");
		spawnObject[2] = (GameObject)Resources.Load("LevelProps/Asteroid");


		
	}
	public override void Spawn(int type, int version)
	{
		GameObject go = (GameObject)Object.Instantiate(spawnObject[type]);
		if(type == 2){
			if(version == 0){
				go.renderer.materials[2].mainTexture = Resources.Load("LevelProps/VEGA") as Texture;
			}else{
				go.renderer.materials[2].mainTexture = Resources.Load("LevelProps/SPACE") as Texture;
			}
		}
		go.transform.localScale = new Vector3 (Random.Range(3f,5f),Random.Range(3f,5f),Random.Range(3f,5f));
		go.transform.position = new Vector3 (transform.position.x + Random.Range(-250f,250f),transform.position.y,transform.position.z);
		go.transform.rotation = transform.rotation;
		go.AddComponent("UFO_Base");
		go.rigidbody.useGravity = false;

		UFO_Base createdObject = go.GetComponent<UFO_Base>();
		createdObject.initTimer(5f,Random.Range(300f,500f));
		createdObject.transform.parent = this.transform;
	}

}
