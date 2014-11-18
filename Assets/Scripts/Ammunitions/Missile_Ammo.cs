using UnityEngine;
using System.Collections;

public class Missile_Ammo : Projectile_Base {
	
	protected Transform enemyPos;
	protected Vector3 pos;


	// Use this for initialization
	public override void Start () {

		flyTime = 5f;
		projectileVelocity = 1000;
		timer = new EventTimer_Base(flyTime);
		rigidbody.velocity = transform.forward * projectileVelocity;
		Debug.Log(damage);

		
	}

	
}
