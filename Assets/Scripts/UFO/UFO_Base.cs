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
		cameraPos = GameObject.Find("ImageTarget").transform;
		lifeSpan = life;
		objectVelocity = speed;
		timer = new EventTimer_Base(lifeSpan);
	}

	public void  resetTimer(){
		timer.resetTimer();
	}

	public virtual void Start(){}

	public virtual void Update(){
		if(!gameObject.activeSelf)
			return;
	
		if(transform.position.y > cameraPos.transform.position.y){
			Despawn();
		}

	}
	public void Spawn(){
		if(this.gameObject != null)
		

		gameObject.SetActive(true);
		rigidbody.velocity = transform.forward * objectVelocity;
	}

	public void Despawn(){

		if(Parent != null)
			Parent.meteorStack.Push (this);
		gameObject.SetActive(false);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "projectile"){
			Despawn();
		}
	}
}
