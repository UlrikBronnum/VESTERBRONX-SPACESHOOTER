using UnityEngine;
using System.Collections;

public class Spaceship_Enemy : Spaceship_Base {

	private float objectVelocity;
	private float lifeSpan;
	private EventTimer_Base timer;
	protected Transform cameraPos;

	// The rate of fire is set in the child classes of the class and sets the rate of fire for the enemy's weapons
	protected float enemyFireRate;

	// collisionDamage of the ENEMY
	public int collisionDamage;

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

		if(!gameObject.activeSelf)
			return;
		
		// deletes the enemy if it flies past the camera: 
		if(transform.position.y > cameraPos.transform.position.y){	
			Destroy(gameObject);
		}
		//	RaycastHit hit;
		//	Ray weaponSight = new Ray(transform.position,Vector3.forward);
		//	Debug.DrawRay(transform.position,Vector3.forward, Color.blue);

		//if(Physics.Raycast(weaponSight,out hit, 5000f)){
			//if(hit.collider.tag == "Player"){

				
			//}
		//}
		for (int i = 0; i < canonMountCapacity; i++) {
				Weapons_Base script = canonMounted [i].GetComponent<Weapons_Base> ();
						script.EnemyFireWeapon ();
				}



	}

	protected void setRateofFire(){
		for (int i = 0; i < canonMountCapacity; i++) {
				Weapons_Base script = canonMounted [i].GetComponent<Weapons_Base> ();
				script.rateOfFire = enemyFireRate; 
		//	script.fireTimer.TimerValue =enemyFireRate;
		//	script.fireTimer.resetTimer();
			}
	}


	public void initTimer(float life){
		lifeSpan = life;
		timer = new EventTimer_Base(lifeSpan);
	}


	public override void takeDamage(int damage){
		health -= damage;
		if(health<=0)
		{
			Parent.deadEnemy++;
			Destroy(gameObject);
		}
	}
	// if the enemy os out of health, it will die. 
	public void die()
	{


	}

	void OnCollisionEnter(Collision other)
	{
		//If the object has the tag projectile run this
		if(other.collider.tag =="PlayerProjectile")
		{
			//Run a function to subtract damage from the enemy's health, according to the damage of the projectile
			takeDamage(other.collider.GetComponent<Projectile_Base>().damage);
		}
	}




}
