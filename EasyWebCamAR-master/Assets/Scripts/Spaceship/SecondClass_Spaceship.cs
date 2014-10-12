using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecondClass_Spaceship : Spaceship_Player {

	// Run function to set class specific lists 
	public override void shipInitialization(){
		
		player = GameObject.Find(cameraName);
		// Character Controller used to move the ship
		// and resets it's size, so it will not block
		// shots from canons
		cc = GetComponent<CharacterController>();
		cc.radius = 1;
		cc.height = 1;
		
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;
		
		canonScale = transform;
		
		// Ship speed
		maneuverSpeed = 40f;
		
		// Amount of gun attachments 
		canonMountCapacity = transform.childCount;
		
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];
		canonMounted = new GameObject[canonMountCapacity];
		
		Player_Charactor script = player.GetComponent<Player_Charactor>();
		
		for (int i = 0 ; i < transform.childCount ; i ++){
			canonMount[i] = transform.FindChild("mountT" + i);
			canonTypes[i] = script.hangar.canonTypes[0];
		}
		// Give an intitial value to canon types
		
		// Find the canon mounts on model
		// Set array for canons

		mountCanon(0);
		mountCanon(1);
	}
	// 
	public override void copyInitialization()
	{
		
		player = GameObject.Find(cameraName);
		// Character Controller used to move the ship
		// and resets it's size, so it will not block
		// shots from canons
		cc = GetComponent<CharacterController>();
		cc.radius = 1;
		cc.height = 1;
		
		spaceshipRotation = transform.rotation.z;
		// Ship speed
		maneuverSpeed = 40f;
		// Amount of gun attachments 
		canonMountCapacity = transform.childCount;
		canonScale = transform;
		
		canonMountCapacity = transform.childCount;
		
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];
		canonMounted = new GameObject[canonMountCapacity];
		
		Player_Charactor script = player.GetComponent<Player_Charactor>();
		Spaceship_Base ship = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Base>();
		
		for (int i = 0 ; i < transform.childCount ; i ++){
			canonMount[i] = transform.FindChild("mountT" + i);
			canonTypes[i] = ship.canonTypes[0];
			canonMounted[i] = canonMount[i].GetChild(canonMount[i].childCount - 1).gameObject;
		}
		
		// Save the initial rotation of ship for reference
		
	}

}