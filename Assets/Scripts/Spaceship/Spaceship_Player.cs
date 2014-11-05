using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// imports a characterController
[RequireComponent(typeof(CharacterController))] 

public class Spaceship_Player : Spaceship_Base {
	/// <summary>
	/// This is the base class of Spaceships. All spaceship
	/// will inherits from this base class. The morphology will
	/// split in a player specific and a enemy specific class
	/// 
	/// Don't write new code here
	/// </summary>

	public string cameraName = "ARCamera";

	// the price of the ship
	public int shipValue;

	// ship upgrade states
	public int[] upgradeStates = new int[3];

	// ship magasins, comes from the weapon mounted
	public int mountMagasinCapacity;

	public int shipInGameHealth;
	public int shipInGameShield;
	// Controls, gui ship buttons
	private bool fire = false;
	private float dir = 0f;


	// CharacterController used to move player spaceship
	protected CharacterController cc;
	// Find the player so it is easy to update
	// player variables
	protected GameObject player;



	/// <summary>
	/// Only use this on new gameobjects
	/// Use copyInitialization() for copies of
	/// existing gameobjects
	/// </summary>
	public virtual void shipInitialization(){}
	/// <summary>
	/// Only use this when copying existing gameobjects
	/// Use copyInitialization() for copies of
	/// existing gameobjects
	/// </summary>
	public virtual void copyInitialization(){
		mountMagasinCapacity = canonMounted[0].GetComponent<Weapons_Base>().weaponCapacity() * 2;
		shipInGameHealth = shipHealth();
		shipInGameShield = shipShield();

	}
	public override void takeDamage(int damage){
		if(shipInGameShield > 0){
			hitTimer.timerActive = true;
			renderer.material.SetFloat("_ShieldBlend" , 1);
			hitTimer.resetTimer();
			if(shipInGameShield - damage > 0){
				shipInGameShield -= damage;
			}else{
				shipInGameShield -= damage;
				shipInGameHealth -= -1 * (shipInGameShield - damage);
			}
		}else{
			shipInGameHealth -= damage;
		}
	}
	// if the player is out of health, it will die. 
	public void die(){

	}
	// Controls, sends input from gui ship buttons
	public void getButtonInput(bool fb, float js){
		fire = fb;
		dir = js;
	}
	
	void OnCollisionEnter(Collision other)
	{
	
		//If the enemy collides with the player and has the tag " enemy", following will run:
		if(other.collider.tag =="Enemy")
		{
			//Run a function to subtract damage from the player's health, according to the damage of the enemy
			takeDamage(other.collider.GetComponent<Spaceship_Enemy>().collisionDamage);
			Destroy (other.collider.gameObject);
			other.collider.GetComponent<Spaceship_Enemy>().Parent.deadEnemy++;
		}
		if(other.collider.tag =="EnemyProjectile")
		{
			//Run a function to subtract damage from the player's health, according to the damage of the enemy
			takeDamage(other.collider.GetComponent<Projectile_Base>().damage);
			Destroy (other.collider.gameObject);
		}
	}

	public override void initializeCanon(Transform scale, int i){
		canonMounted[i] = (GameObject)Object.Instantiate(Resources.Load(canonTypes[i]));
		Transform thisTrans = canonMounted[i].transform;
		canonMounted[i].transform.localScale = new Vector3(thisTrans.localScale.x * scale.localScale.x ,thisTrans.localScale.y * scale.localScale.y , thisTrans.localScale.z * scale.localScale.z);
		canonMounted[i].transform.position = canonMount[i].position;
		canonMounted[i].transform.rotation = canonMount[i].rotation;
		canonMounted[i].transform.parent = canonMount[i].transform;
		
		Weapons_Base wScript = canonMounted[i].GetComponent<Weapons_Base>();
		Player_Charactor hScript = GameObject.Find("ARCamera").GetComponent<Player_Charactor>();
		wScript.forceStart ();
		
		for(int j = 0; j < hScript.hangar.canonTypes.Count ; j++){
			if (hScript.hangar.canonTypes[j] == canonTypes[i]){
				wScript.setUpStates(hScript.hangar.canonUpgrade1[j], hScript.hangar.canonUpgrade2[j] , hScript.hangar.canonUpgrade3[j]);				
			}
		}
	}

	public int shipHealth(){
		return health +  (int)(health * (upgradeStates[0] / 10.0f));
	}
	
	public int shipShield(){
		return health +  (int)(health * (upgradeStates[1] / 10.0f));
	}
	
	public float shipManeuverSpeed(){
		return maneuverSpeed +  (maneuverSpeed * (upgradeStates[2] / 10.0f));
	}


	/// <summary>
	/// All of the players spaceship inherits 
	/// this updatefunction, moves the spaceship
	/// when isActive is true
	/// </summary>
	public override void Update () 
	{
		if(hitTimer.timerActive){
			manageShader();
		}
		/*
		if(Application.platform == RuntimePlatform.WindowsEditor ||
		   Application.platform == RuntimePlatform.OSXPlayer)
		{
			if(IsActive)
				pcControls(canonMountCapacity);

		}
		else if (Application.platform == RuntimePlatform.Android ||
		         Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if(IsActive)
				androidControls(canonMountCapacity);
		}*/

		if(IsActive)
			androidControls(canonMountCapacity);

	}
	private void pcControls(int shipCapacity){
		float sideSpeed;

		if(Input.GetKey("left")){
			sideSpeed = -1;
		}else if(Input.GetKey("right")){
			sideSpeed = 1;
		}else{
			sideSpeed = 0;
		}
		// moves the player 
		if(sideSpeed > 0){
			if(transform.position.x < 250.0f){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.y > -0.3){
					transform.Rotate(new Vector3(0,0,1) * -maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}
		if(sideSpeed < 0){
			if(transform.position.x > -250.0f ){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z < 0.3){
					transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}
		if(sideSpeed == 0){
			if (transform.rotation.z < -0.02f){
				transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
			}else if(transform.rotation.z > 0.02f){
				transform.Rotate(new Vector3(0,0,1) * -maneuverSpeed * 2 * Time.deltaTime);
			}
		}

		// fire weapons
		if(Input.GetButton("Fire1")){
			for(int i = 0; i < shipCapacity; i++){
				Weapons_Base script = canonMounted[i].GetComponent<Weapons_Base>();
				mountMagasinCapacity -= script.fireWeapon();
			}
		}
		

	}
	
	private void androidControls(int shipCapacity){	
		if(dir < 0.01 && dir > -0.01)
		{
			dir = 0;
		}
		if(dir != 0){

		}
		if(transform.position.x < 250.0f  && dir > 0)
		{
			moveShip (shipManeuverSpeed()*dir);
			if(transform.rotation.z > -0.3){
				transform.Rotate(new Vector3(0,0,1) * -dir * shipManeuverSpeed() * 2 * Time.deltaTime);
			}
		}else if(transform.position.x > -250.0f  && dir < 0)
		{
			moveShip (shipManeuverSpeed()*dir);
			if(transform.rotation.z < 0.3){
				transform.Rotate(new Vector3(0,0,1) * -dir * shipManeuverSpeed() * 2 * Time.deltaTime);
			}
		}else
		{
			if (transform.rotation.z < - 0.02f){
				transform.Rotate(new Vector3(0,0,1)  * shipManeuverSpeed() * 2 * Time.deltaTime);
			}else if(transform.rotation.z > 0.02f){
				transform.Rotate(new Vector3(0,0,1)  * -shipManeuverSpeed() * 2 * Time.deltaTime);
			}
		}
		
		if(fire){
			for(int i = 0; i < shipCapacity; i++){
				Weapons_Base script = canonMounted[i].GetComponent<Weapons_Base>();
				mountMagasinCapacity -= script.fireWeapon();

			}
		}
		
	}

	public void setUpStates(int up1, int up2, int up3){
		upgradeStates[0] = up1;
		upgradeStates[1] = up2;
		upgradeStates[2] = up3;
	}

	private void moveShip(float xAxis){

		Vector3 speed = new Vector3(xAxis,0,0);
		cc.Move (speed * Time.deltaTime);
	}






}
