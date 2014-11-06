using UnityEngine;
using System.Collections;

public class Plasma_Gun_Ammo : Projectile_Base {

	
	// Use this for initialization
	public override void Start () {

		flyTime = 5f;
		projectileVelocity = 1000;
		timer = new EventTimer_Base(flyTime);
		rigidbody.velocity = transform.forward * projectileVelocity;
	}
	

}
