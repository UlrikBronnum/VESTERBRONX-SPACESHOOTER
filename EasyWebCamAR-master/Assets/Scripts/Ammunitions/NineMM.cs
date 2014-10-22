using UnityEngine;
using System.Collections;

public class NineMM : Projectile_Base {

	
	// Use this for initialization
	public override void Start () {



		flyTime = 5f;
		projectileVelocity = 200;
		timer = new EventTimer_Base(flyTime);
		rigidbody.velocity = transform.forward * projectileVelocity;
	}
	

}
