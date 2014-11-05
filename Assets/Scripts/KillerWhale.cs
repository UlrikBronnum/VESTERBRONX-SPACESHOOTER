using UnityEngine;
using System.Collections;

public class KillerWhale : EnemyScript {

	// Use this for initialization
	void Start () {

		rigidbody.velocity = transform.TransformDirection(Vector3.forward * 60);
		Health = 75;
		ShootSpeed = 2;
		time = 0f;
		time2 = 0f;
		projectile = (GameObject)Resources.Load ("Cube");
		//Enemy = (GameObject)Resources.Load ("KillerWhale");
	
	}
	
	// Update is called once per frame
	void Update () {

		CameraPos = GameObject.Find ("ARCamera");

		CheckPosition ();

		time2 += Time.deltaTime;
		//time2 += Time.deltaTime;
		
		if (time2 > ShootSpeed) {
			time2 = 0f;
			Shoot();
		}
	
	}
}
