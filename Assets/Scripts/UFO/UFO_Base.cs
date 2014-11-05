using UnityEngine;
using System.Collections;

public class UFO_Base : MonoBehaviour {
	
	protected float objectVelocity;
	protected float lifeSpan;
	protected EventTimer_Base timer;
	protected Transform cameraPos; 

	[System.NonSerialized]
	public Meteor_Spawn Parent;

	public void initTimer(float life,float speed){
		cameraPos = GameObject.Find("ARCamera").transform;
		lifeSpan = life;
		objectVelocity = speed;
		timer = new EventTimer_Base(lifeSpan);
	}

	public void  resetTimer(){
		timer.resetTimer();
	}

	public virtual void Start(){
		timer = new EventTimer_Base(lifeSpan);
	}

	public void Update(){
		if(!gameObject.activeSelf)
			return;
	
		if(transform.position.y > cameraPos.transform.position.y){
			Despawn();
		}

	}
	public void Spawn(){
		if(this.gameObject != null)
			timer.TimerValue = lifeSpan;

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
		}
	}
}
