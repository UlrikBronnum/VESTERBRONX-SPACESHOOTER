using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecondClass_Spaceship : Spaceship_Player {

	// Run function to set class specific lists
	public override void shipInitialization()
	{	
		player = GameObject.Find(cameraName);
	
		// Character Controller used to move the ship
		// and resets it's size, so it will not block
		// shots from canons
		cc = GetComponent<CharacterController>();
		cc.radius = 1;
		cc.height = 1;

		canonScale = transform;
		// Ship speed
		maneuverSpeed = 40f;
		// Amount of gun attachments 
		canonMountCapacity = 4;
		// Give an intitial value to canon types
		canonTypes = new string[canonMountCapacity];
		canonTypes[0] = "projectileCanon";
		canonTypes[1] = "projectileCanon";
		canonTypes[2] = "projectileCanon";
		canonTypes[3] = "projectileCanon";

		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonMount[0] = transform.FindChild("gunMountLeft1");
		canonMount[1] = transform.FindChild("gunMountRight1");
		canonMount[2] = transform.FindChild("gunMountLeft2");
		canonMount[3] = transform.FindChild("gunMountRight2");
		// Set array for canons
		canonMounted = new GameObject[canonMountCapacity];
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;
		
		// Mount canons on new spaceship
		mountCanon(0);
		mountCanon(1);

		aButton = (GameObject)Resources.Load ("AButton");
		aButton.SetActive (false);
		leftArrow = (GameObject)Resources.Load ("Red_Arrow_Left");
		leftArrow.SetActive (false);
		rightArrow = (GameObject)Resources.Load ("Red_Arrow_Right");
		rightArrow.SetActive (false);
		
		Instantiate(aButton, new Vector3(0.6f, 0.4f,0) , aButton.transform.rotation);
		Instantiate(leftArrow, new Vector3(0.3f, 0.3f,0), leftArrow.transform.rotation);
		Instantiate(rightArrow, new Vector3(0.3f, 0.3f,0), rightArrow.transform.rotation);
		
	}

	public override void copyInitialization()
	{

		player = GameObject.Find(cameraName);
		
		Player_Charactor script = player.GetComponent<Player_Charactor>();
		Spaceship_Base ship = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Base>();
		
		// Character Controller used to move the ship
		// and resets it's size, so it will not block
		// shots from canons
		cc = GetComponent<CharacterController>();
		cc.radius = 1;
		cc.height = 1;
		// Ship speed
		maneuverSpeed = 40f;
		// Amount of gun attachments 
		canonMountCapacity = 4;
		// Give an intitial value to canon types
	
		canonScale = transform;

		canonTypes = new string[canonMountCapacity];
		canonTypes[0] = ship.canonTypes[0];
		canonTypes[1] = ship.canonTypes[0];
		canonTypes[2] = ship.canonTypes[1];
		canonTypes[3] = ship.canonTypes[1];
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonMount[0] = transform.FindChild("gunMountLeft1");
		canonMount[1] = transform.FindChild("gunMountRight1");
		canonMount[2] = transform.FindChild("gunMountLeft2");
		canonMount[3] = transform.FindChild("gunMountRight2");
		// Set array for canons
		canonMounted = new GameObject[canonMountCapacity];
		// Find the canons already mounted on prefab
		canonMounted[0] = canonMount[0].GetChild(canonMount[0].childCount - 1).gameObject;
		canonMounted[1] = canonMount[1].GetChild(canonMount[1].childCount - 1).gameObject;
		canonMounted[2] = canonMount[2].GetChild(canonMount[2].childCount - 1).gameObject;
		canonMounted[3] = canonMount[3].GetChild(canonMount[3].childCount - 1).gameObject;
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;

		aButton = (GameObject)Resources.Load ("AButton");
		aButton.SetActive (false);
		leftArrow = (GameObject)Resources.Load ("Red_Arrow_Left");
		leftArrow.SetActive (false);
		rightArrow = (GameObject)Resources.Load ("Red_Arrow_Right");
		rightArrow.SetActive (false);

		Instantiate(aButton, new Vector3(0.6f, 0.4f,0) , aButton.transform.rotation);
		Instantiate(leftArrow, new Vector3(0.3f, 0.3f,0), leftArrow.transform.rotation);
		Instantiate(rightArrow, new Vector3(0.3f, 0.3f,0), rightArrow.transform.rotation);
		

	}

}