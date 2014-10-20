using UnityEngine;
using System.Collections;

public class EnemyFirstClass : Spaceship_Enemy {

	public virtual void Start() { 
		cameraPos = GameObject.Find ("ARCamera").transform;
		
		canonScale = transform;
		
		// Ship speed
		maneuverSpeed = 200f;
		// Amount of gun attachments 
		canonMountCapacity = transform.childCount;
		
		
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];
		
		for (int i = 0 ; i < transform.childCount ; i ++){
			canonMount[i] = transform.FindChild("mountT" + i);
			canonTypes[i] = "projectileCanon";
		}
		// Give an intitial value to canon types
		
		// Set array for canons
		canonMounted = new GameObject[canonMountCapacity];
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;
		
		for(int i = 0 ; i < canonMountCapacity/2 ; i++){
			mountCanon(i);
		}
	}
	public virtual void shipInitialization(){


		
	}

}
