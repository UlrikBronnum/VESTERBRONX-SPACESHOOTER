using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// imports a characterController
[RequireComponent(typeof(CharacterController))] 

public class Spaceship_Player : Spaceship_Base {

	public string cameraName = "ARCamera";
	public int shipValue;
	public int[] upgradeStates = new int[3];
	// Use this for initialization


	private bool fire = false;
	private float dir = 0f;

	/// <summary>
	/// This is the base class of Spaceships. All spaceship
	/// will inherits from this base class. The morphology will
	/// split in a player specific and a enemy specific class
	/// 
	/// Don't write new code here
	/// </summary>

	// CharacterController used to move player spaceship
	protected CharacterController cc;
	// Find the player so it is easy to update
	// player variables
	protected GameObject player;

	// for the buttons:

	public void getButtonInput(bool fb, float js){
		fire = fb;
		dir = js;
	}

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
	public virtual void copyInitialization(){}


	public override void initializeCanon(Transform scale, int i){
		canonMounted[i] = (GameObject)Object.Instantiate(Resources.Load(canonTypes[i]));
		Transform thisTrans = canonMounted[i].transform;
		canonMounted[i].transform.localScale = new Vector3(thisTrans.localScale.x * scale.localScale.x ,thisTrans.localScale.y * scale.localScale.y , thisTrans.localScale.z * scale.localScale.z);
		canonMounted[i].transform.position = canonMount[i].position;
		canonMounted[i].transform.rotation = canonMount[i].rotation;
		canonMounted[i].transform.parent = canonMount[i].transform;
		
		Weapons_Base wScript = canonMounted[i].GetComponent<Weapons_Base>();
		Player_Charactor hScript = GameObject.Find("ARCamera").GetComponent<Player_Charactor>();
		wScript.setFireRatePl (10f);

		for(int j = 0; j < hScript.hangar.canonTypes.Count ; j++){
			if (hScript.hangar.canonTypes[j] == canonTypes[i]){
				wScript.setUpStates(hScript.hangar.canonUpgrade1[j], hScript.hangar.canonUpgrade2[j] , hScript.hangar.canonUpgrade3[j]);
				
			}
		}
		
	}


	/// <summary>
	/// All of the players spaceship inherits 
	/// this updatefunction, moves the spaceship
	/// when isActive is true
	/// </summary>
	public override void Update () 
	{
	
	/*	if(Application.platform == RuntimePlatform.WindowsEditor ||
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
		}if(sideSpeed < 0){
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
				script.fireWeapon();
				Player_Charactor playerScr = GameObject.Find(cameraName).GetComponent<Player_Charactor>();
				for(int j = 0 ; j < playerScr.hangar.canonTypes.Count;j++){
					if(canonMounted[i].name == playerScr.hangar.canonTypes[j]){

					}
				}
				
			}
		}
		

	}
	
	private void androidControls(int shipCapacity){
		
		
		
		
		if(dir < 0.01 && dir > -0.01)
		{
			dir = 0;
		}
		if(dir != 0){
			if(transform.position.x < 250.0f  ){
				moveShip (shipManeuverSpeed()*dir);
				if(transform.rotation.z < 0.3){
					transform.Rotate(new Vector3(0,0,1) * -dir * shipManeuverSpeed() * 2 * Time.deltaTime);
				}
			}else if(transform.position.x > -250.0f){
				moveShip (shipManeuverSpeed()*dir);
				if(transform.rotation.z > -0.3){
					transform.Rotate(new Vector3(0,0,1) * dir * shipManeuverSpeed() * 2 * Time.deltaTime);
				}
			}
		}
		else{
			if (transform.rotation.z < - 0.1f){
				transform.Rotate(new Vector3(0,0,1)  * shipManeuverSpeed() * 2 * Time.deltaTime);
			}else if(transform.rotation.z > 0.1f){
				transform.Rotate(new Vector3(0,0,1)  * -shipManeuverSpeed() * 2 * Time.deltaTime);
			}
		}
		
		if(fire){
			for(int i = 0; i < shipCapacity; i++){
				Weapons_Base script = canonMounted[i].GetComponent<Weapons_Base>();
				script.fireWeapon();
				Player_Charactor playerScr = GameObject.Find(cameraName).GetComponent<Player_Charactor>();
				if(canonMounted[i].name == playerScr.hangar.canonTypes[0]){
					
				}else if(canonMounted[i].name == playerScr.hangar.canonTypes[1]){
					
				}
			}
		}
		
	}

	public void setUpStates(int up1, int up2, int up3){
		upgradeStates[0] = up1;
		upgradeStates[1] = up2;
		upgradeStates[2] = up3;
	}
	public int shipHealth(){
		int sH = health +  (int)(health * (upgradeStates[0] / 10.0f));
		return sH;
	}
	
	public int shipShild(){
		int sS = health +  (int)(health * (upgradeStates[1] / 10.0f));
		return sS;
	}
	
	public float shipManeuverSpeed(){
		float sMS = maneuverSpeed +  (maneuverSpeed * (upgradeStates[2] / 10.0f));
		return sMS;
	}


	private void moveShip(float xAxis){

		Vector3 speed = new Vector3(xAxis,0,0);
		cc.Move (speed * Time.deltaTime);
	}


	public override void takeDamage(int damage){
		health -= damage;
		if(health<=0){
			die();}
	}
	// if the player is out of health, it will die. 
	public void die(){
		Destroy (gameObject);

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



}
