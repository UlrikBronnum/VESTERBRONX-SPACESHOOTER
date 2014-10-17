using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// imports a characterController
[RequireComponent(typeof(CharacterController))] 

public class Spaceship_Player : Spaceship_Base {

	public string cameraName = "ARCamera";



	private bool fire = false;
	private bool left = false;
	private bool right = false;

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

	public void getButtonInput(bool fb, bool lb, bool rb){
		fire = fb;
		left = lb;
		right = rb;
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

	/// <summary>
	/// All of the players spaceship inherits 
	/// this updatefunction, moves the spaceship
	/// when isActive is true
	/// </summary>
	public override void Update () 
	{

		if(Application.platform == RuntimePlatform.WindowsEditor ||
		   Application.platform == RuntimePlatform.OSXPlayer)
		{
			if(IsActive)
				pcControls(canonMountCapacity);

		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			if(IsActive)
				androidControls(canonMountCapacity);
		}

		
	
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
			if(transform.position.x < 150.0f){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z > -0.3){
					transform.Rotate(new Vector3(0,0,1) * -maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}else if(sideSpeed < 0){
			if(transform.position.x > -150.0f ){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z < 0.3){
					transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}else{
			if (transform.rotation.z < spaceshipRotation){
				transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
			}else if(transform.rotation.z > spaceshipRotation){
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
						playerScr.hangar.canonAmmunitionStorage[j] -= 1;
					}
				}
				
			}
		}
		

	}
	
	private void androidControls(int shipCapacity){

		float sideSpeed;


		
		if(left){// gyo1.attitude.eulerAngles.z > 45){//Input.GetKey("left")){
			sideSpeed = -3;
		}else if(right){// gyo1.attitude.eulerAngles.z < -45){//Input.GetKey("right")){
			sideSpeed = 3;
		}else{
			sideSpeed = 0;
		}
		
		if(sideSpeed > 1){
			if(transform.position.x < 150.0f){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z > -0.3){
					transform.Rotate(new Vector3(0,0,1) * -maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}else if(sideSpeed < -1){
			if(transform.position.x > -150.0f ){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z < 0.3){
					transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
				}
			}
		}else{
			if (transform.rotation.z < spaceshipRotation){
				transform.Rotate(new Vector3(0,0,1) * maneuverSpeed * 2 * Time.deltaTime);
			}else if(transform.rotation.z > spaceshipRotation){
				transform.Rotate(new Vector3(0,0,1) * -maneuverSpeed * 2 * Time.deltaTime);
			}
		}

		if(fire){
			for(int i = 0; i < shipCapacity; i++){
				Weapons_Base script = canonMounted[i].GetComponent<Weapons_Base>();
				script.fireWeapon();
				Player_Charactor playerScr = GameObject.Find(cameraName).GetComponent<Player_Charactor>();
				if(canonMounted[i].name == playerScr.hangar.canonTypes[0]){
					playerScr.hangar.canonAmmunitionStorage[0] -= 1;
				}else if(canonMounted[i].name == playerScr.hangar.canonTypes[1]){
					playerScr.hangar.canonAmmunitionStorage[1] -= 1;
				}
			}
		}

	}



	private void moveShip(float xAxis){

		Vector3 speed = new Vector3(xAxis,0,0);
		cc.Move (speed * Time.deltaTime);
	}

	/*
	public override void takeDamage(int damage){
		health -= damage;
		if(health>=0){
			die();}
	}
	// if the player is out of health, it will die. 
	public void die(){
		// die stuff in here

	}


	void OnCollisionEnter(Collision other)
	{
		//If the enemy have the tag enemy run this
		if(other.collider.tag =="enemy")
		{
			//Run a function to subtract damage from the player's health, according to the damage of the enemy
			takeDamage(other.collider.GetComponent<Spaceship_Enemy>().damage);
		}
	}*/



}
