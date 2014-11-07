using UnityEngine;
using System.Collections;

public class Spaceship_Enemy : Spaceship_Base {

	//private float objectVelocity;

	private float lifeSpan;
	protected EventTimer_Base timer;
	protected Transform cameraPos;

	// The rate of fire is set in the child classes of the class and sets the rate of fire for the enemy's weapons
	protected float fireRate;
	protected int damage;
	// collisionDamage of the ENEMY
	public int collisionDamage;
	// contains the exlosion when the enemy is destroyed:
	public GameObject explosion;

	[System.NonSerialized]
	public Enemy_Spawn Parent;
	// Use this for initialization
	public virtual void Start() {
		explosion = Resources.Load ("explosion") as GameObject;
	}
	public virtual void shipInitialization(){ }
	public virtual void forceStart(){ }
	public void modifyEnemy(int h, int s, float m, float f, int d){
		health += h;
		shield += s;
		// Ship speed
		maneuverSpeed += m;
		// sets the rate of fire for the guns of this Enemy:
		fireRate -= f;
		damage += d;

		shipInGameShield = shield;

		collisionDamage = (int)(health/4f);
		
		resetTimer();
		
		for(int i = 0 ; i < canonMountCapacity/2 ; i++){
			removeCanon(i);
			mountCanon(i);
		}
	}


	public void resetEnemyType(float m, int h, int s, float f){
		maneuverSpeed = m;
		health = h;
		shield = s;

		fireRate = f;
		timer.resetTimer();
	}

	public void  resetTimer(){
		timer.resetTimer();
	}

	// Update is called once per frame
	public override void Update () {
		Transform tmp = transform;
		Vector3 tmpPos = tmp.position;
		tmpPos.y += maneuverSpeed * Time.deltaTime;
		transform.position = tmpPos;
		manageShader();

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
			EnemyWeapon_Base script = canonMounted[i].GetComponent<EnemyWeapon_Base> ();
			script.fireWeapon ();
		}



	}
	public override void initializeCanon(Transform scale, int i)
	{
		canonMounted[i] = (GameObject)Object.Instantiate(Resources.Load(canonTypes[i]));
		Transform thisTrans = canonMounted[i].transform;
		canonMounted[i].transform.localScale = new Vector3(thisTrans.localScale.x * scale.localScale.x ,thisTrans.localScale.y * scale.localScale.y , thisTrans.localScale.z * scale.localScale.z);
		canonMounted[i].transform.position = canonMount[i].position;
		canonMounted[i].transform.rotation = canonMount[i].rotation;
		canonMounted[i].transform.parent = canonMount[i].transform;
		EnemyWeapon_Base wwscript = canonMounted [i].GetComponent<EnemyWeapon_Base> ();
		wwscript.forceStart (fireRate,damage);
	}



	public void initTimer(float life){
		lifeSpan = life;
		timer = new EventTimer_Base(lifeSpan);
	}


	public override void takeDamage(int damage){
		if(shipInGameShield > 0){
			hitTimer.timerActive = true;
			renderer.material.SetFloat("_Shield_State" , 1f);
			hitTimer.resetTimer();
			if(shipInGameShield - damage > 0){
				shipInGameShield -= damage;
			}else{
				shipInGameShield -= damage;
				health -= -1 * (shipInGameShield - damage);
			}
		}else{
			health -= damage;
		}
		if(health<=0)
		{
			Parent.deadEnemy++;
			Destroy(gameObject);
			Instantiate(explosion,transform.position, new Quaternion());
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
		if(other.collider.tag =="Player")
		{
			Instantiate(explosion,transform.position, new Quaternion());
			//Run a function to subtract damage from the enemy's health, according to the damage of the projectile
			takeDamage(0);
			Destroy(gameObject);
		}
	}
	public override int shipHealth(){
		return health;
	}
	
	public override int shipShield(){
		return shield ;
	}
	
	public override float shipManeuverSpeed(){
		return maneuverSpeed;
	}




}
