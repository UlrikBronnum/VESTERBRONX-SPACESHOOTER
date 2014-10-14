using UnityEngine;
using System.Collections;

public class Projectile_Base : MonoBehaviour {

	protected float projectileVelocity;
	protected EventTimer_Base timer;
	protected float flyTime;
	public int damage;

	// Use this for initialization
	public virtual void  Start () {
		//flyTime = 5f;
		//projectileVelocity = 200;
		//timer = new EventTimer_Base(flyTime);
		//rigidbody.velocity = transform.forward * projectileVelocity;
	}
	void Update(){
		if(timer.timerTick()){
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision col){
	
		Destroy(gameObject);
	}

}
