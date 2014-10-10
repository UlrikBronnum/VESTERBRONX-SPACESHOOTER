using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// imports a characterController
[RequireComponent(typeof(CharacterController))] 

public class Spaceship_Player : Spaceship_Base {

	public string cameraName = "ARCamera" ;

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
	protected GameObject aButton;
	protected GameObject rightArrow;
	protected GameObject leftArrow;

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
	

		if(isActive)
			buttonsActive();
			moveSpaceship(canonMountCapacity);	

	}
	/// <summary>
	/// System for maneuvering player spaceship
	/// should be inside this function
	/// </summary>
	/// <param name="shipCapacity">Ship capacity.</param>
	protected void moveSpaceship (int shipCapacity) 
	{

		float sideSpeed;
		// fire weapons
		if(Input.GetButton("Fire1")){
			for(int i = 0; i < shipCapacity; i++){
				Weapons_Base script = canonMounted[i].GetComponent<Weapons_Base>();
				script.fireWeapon();
				Player_Charactor playerScript = GameObject.Find("ARCamera").GetComponent<Player_Charactor>();
				if(canonMounted[i].name == playerScript.hangar.canonTypes[0]){
					playerScript.hangar.canonAmmunitionStorage[0] -= 1;
				}else if(canonMounted[i].name == playerScript.hangar.canonTypes[1]){
					playerScript.hangar.canonAmmunitionStorage[1] -= 1;
				}

			}
		}
		if(Input.GetKey("left")){
			sideSpeed = -1;
		}else if(Input.GetKey("right")){
			sideSpeed = 1;
		}else{
			sideSpeed = 0;
		}
		// moves the player 
		if(sideSpeed > 0){
			if(transform.position.x < 15.0f){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z > -0.3){
					transform.Rotate(new Vector3(0,0,1) * -150 * Time.deltaTime);
				}
			}
		}else if(sideSpeed < 0){
			if(transform.position.x > -15.0f ){
				moveShip (sideSpeed * maneuverSpeed);
				if(transform.rotation.z < 0.3){
					transform.Rotate(new Vector3(0,0,1) * 150 * Time.deltaTime);
				}
			}
		}else{
			if (transform.rotation.z < spaceshipRotation){
				transform.Rotate(new Vector3(0,0,1) * 150 * Time.deltaTime);
			}else if(transform.rotation.z > spaceshipRotation){
				transform.Rotate(new Vector3(0,0,1) * -150 * Time.deltaTime);
			}
		}

	}
	private void moveShip(float xAxis){
		Vector3 speed = new Vector3(xAxis,0,0);
		cc.Move (speed * Time.deltaTime);
	}

	public void buttonsActive(){
		aButton.SetActive (isActive);
		leftArrow.SetActive (isActive);
		rightArrow.SetActive (isActive);
		
	}

}
