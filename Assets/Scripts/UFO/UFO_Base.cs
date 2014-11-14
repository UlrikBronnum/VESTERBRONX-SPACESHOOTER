using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))] 

public class UFO_Base : MonoBehaviour {
	
	protected float objectVelocity;
	protected float lifeSpan;
	protected EventTimer_Base timer;
	protected Transform cameraPos; 

	public void initTimer(float life,float speed){
		cameraPos = GameObject.Find("ImageTarget").transform;
		lifeSpan = life;
		objectVelocity = speed;
		timer = new EventTimer_Base(lifeSpan);
	}

	public void  resetTimer(){
		timer.resetTimer();
	}



	public virtual void Update(){
		if(!gameObject.activeSelf)
			return;
	
		if(transform.position.y > cameraPos.transform.position.y){
			Destroy(gameObject);
		}

	}
	public void Start(){
		rigidbody.velocity = transform.forward * objectVelocity;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "projectile"){
			Destroy(gameObject);
		}
	}
}
