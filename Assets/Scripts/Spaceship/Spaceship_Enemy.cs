using UnityEngine;
using System.Collections;

public class Spaceship_Enemy : Spaceship_Base {

	//private float objectVelocity;

	private float lifeSpan;
	protected EventTimer_Base timer;
	protected Transform cameraPos;
	protected bool isDead = false;
	
	// 
	public float timeToShoot;

	// bool determining when the first shot is fired:
	public bool firstShot = false;

	// The rate of fire is set in the child classes of the class and sets the rate of fire for the enemy's weapons
	public float fireRate;
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

		timeToShoot = Random.Range (0.0F, 5.0F);
		StartCoroutine (Shoot (timeToShoot));

	
	}
	public virtual void shipInitialization(){ }
	public virtual void forceStart(){ }
	public void modifyEnemy(int level){
		/*int value = level;
		if(level < 9){
			value = level;
		}else if(level > 8 && level < 17){
			value = level - 8;
		}else{
			value = level - 16;
		}*/
//		Debug.Log(value + "  " + level);

		health += level * 60;
		shield += level * 40;
		maneuverSpeed += level * 18;
		fireRate -= level * 0.1f;
		damage += (int)(damage/50 * level);

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
		//print ("dead enemies: "+Parent.deadEnemy);

		if(isDead){
			Parent.deadEnemy++;
			Destroy(gameObject);
		}
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
		if (firstShot) {
			for (int i = 0; i < canonMountCapacity; i++) {
					EnemyWeapon_Base script = canonMounted [i].GetComponent<EnemyWeapon_Base> ();
					script.fireWeapon ();
			}
		}

		/*RaycastHit hit1;
		Vector3 forward1 = this.transform.FindChild("mountT0").transform.TransformDirection(Vector3.forward) * 20000; //Det sidste tal ændrer længen af søgefeltet (tror jeg)
	//	Debug.DrawRay(this.transform.FindChild("mountT0").transform.position, forward1, Color.green);
		RaycastHit hit2;
		Vector3 forward2 = this.transform.FindChild("mountT1").transform.TransformDirection(Vector3.forward) * 20000; //Det sidste tal ændrer længen af søgefeltet (tror jeg)
	//	Debug.DrawRay(this.transform.FindChild("mountT1").transform.position, forward2, Color.green);

		if (Physics.Raycast(this.transform.FindChild("mountT0").transform.position, forward1, out hit1))
		{
			if (hit1.collider.tag == "Player")
			{
				for (int i = 0; i < canonMountCapacity; i++) {
					EnemyWeapon_Base script = canonMounted[i].GetComponent<EnemyWeapon_Base> ();
					script.fireWeapon ();
				}
			}
		}
		if (Physics.Raycast(this.transform.FindChild("mountT1").transform.position, forward2, out hit2))
		{
			if (hit2.collider.tag == "Player")
			{
				for (int i = 0; i < canonMountCapacity; i++) {
					EnemyWeapon_Base script = canonMounted[i].GetComponent<EnemyWeapon_Base> ();
					script.fireWeapon ();
				}
			}
		}*/
	
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
			renderer.material.SetFloat("_Shield_Blend" , 1f);
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
			isDead = true;
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
			hitTimer.resetTimer();
			//Run a function to subtract damage from the enemy's health, according to the damage of the projectile
			takeDamage(other.collider.GetComponent<Projectile_Base>().damage);
			if(shipInGameShield > 0){
				renderer.material.SetFloat("_Shield_Blend" ,1f);
			}
		}
		if(other.collider.tag =="Player")
		{
			Destroy(gameObject);
			Instantiate(explosion,transform.position, new Quaternion());
			hitTimer.resetTimer();
			//Run a function to subtract damage from the enemy's health, according to the damage of the projectile
			Parent.deadEnemy++;
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

	IEnumerator Shoot(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		for (int i = 0; i < canonMountCapacity; i++) {
			EnemyWeapon_Base script = canonMounted[i].GetComponent<EnemyWeapon_Base> ();
			script.fireWeapon ();
		}
		firstShot = true;
	}




}
