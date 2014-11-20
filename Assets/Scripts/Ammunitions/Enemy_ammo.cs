using UnityEngine;
using System.Collections;

public class Enemy_ammo : EnemyProjectile_Base {
	
	
	// Use this for initialization
	public override void Start () {
		
		flyTime = 5f;
		projectileVelocity = 1000;
		timer = new EventTimer_Base(flyTime);
		rigidbody.velocity = transform.up * projectileVelocity;
	}
}
