using UnityEngine;
using System.Collections;

public class Enemy_UFO : UFO_Base {
	

	
	public override void Start(){
		lifeSpan = 30f;
		objectVelocity = -400f;
		timer = new EventTimer_Base(lifeSpan);
		cameraPos = GameObject.Find ("ARCamera").transform;
	}
	
	public void Update(){
		
		
		
		if(!gameObject.activeSelf)
			return;

		// deletes the enemy if it flies past the camera: 
		if(transform.position.y > cameraPos.transform.position.y){	
			Despawn();
		}
	}
	public void Spawn(){
		gameObject.SetActive(true);
		rigidbody.velocity = transform.forward * objectVelocity;
	}
	
	public void Despawn(){
		gameObject.SetActive(false);
		if(Parent != null)
			Parent.meteorStack.Push (this);
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "projectile"){
			Despawn();
			timer.TimerValue = lifeSpan;
		}
	}
}