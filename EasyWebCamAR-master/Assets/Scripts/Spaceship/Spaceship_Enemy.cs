﻿using UnityEngine;
using System.Collections;

public class Spaceship_Enemy : Spaceship_Base {

	private float objectVelocity;
	private float lifeSpan;
	private EventTimer_Base timer;


	[System.NonSerialized]
	public Enemy_Spawn Parent;
	// Use this for initialization
	public virtual void Start() { }
	public virtual void shipInitialization(){ }

	public void  resetTimer(){
		timer.resetTimer();
	}
	// Update is called once per frame
	public override void Update () {
		Transform tmp = transform;
		Vector3 tmpPos = tmp.position;
		tmpPos.y += maneuverSpeed * Time.deltaTime;
		transform.position = tmpPos;
	}
	public void initTimer(float life){
		lifeSpan = life;
		timer = new EventTimer_Base(lifeSpan);
	}

	public void Spawn(){
		if(this.gameObject != null)
			timer.TimerValue = lifeSpan;
		
		gameObject.SetActive(true);

	}
	
	public void Despawn(){
		gameObject.SetActive(false);
		if(Parent != null)
			Parent.enemyStack.Push (this);
	}

	public override void takeDamage(int damage){
		health -= damage;
		if(health>=0){
			die();}
	}
	// if the enemy os out of health, it will die. 
	public void die(){
		Despawn ();
	}



	void OnCollisionEnter(Collision other)
	{
		//If the object has the tag projectile run this
		if(other.collider.tag =="projectile")
		{
			//Run a function to subtract damage from the enemy's health, according to the damage of the projectile
			takeDamage(other.collider.GetComponent<Projectile_Base>().damage);
		}
	}




}
