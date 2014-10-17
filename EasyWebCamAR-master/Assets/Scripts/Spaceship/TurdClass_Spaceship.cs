using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurdClass_Spaceship : Spaceship_Player {


	// Run function to set class specific lists 
	public override void shipInitialization(){

		cameraName = "ARCamera";
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
		maneuverSpeed = 100f;
		// Health of this ship 
		health = 200;

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

	
		for(int i = 0 ; i < canonMountCapacity/2 ; i++){
			mountCanon(i);
		}
	}
	// 

}
