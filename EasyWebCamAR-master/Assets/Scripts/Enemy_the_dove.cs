using UnityEngine;
using System.Collections;

public class Enemy_the_dove : Spaceship_Enemy {
	
	public override void Start() { 
		cameraPos = GameObject.Find ("ARCamera").transform;
		
		
		renderer.material.shader = Shader.Find("Specular");
		renderer.material.SetColor("_Color" , Color.blue);
		
		
		canonScale = transform;
		health = 100;
		// Ship speed
		maneuverSpeed = 100f;
		// Amount of gun attachments 
		canonMountCapacity = 2;
		
		
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];
		
		canonTypes[0] = "projectileCanon";
		canonTypes[1] = "projectileCanon";
		
		
		for (int i = 0 ; i < canonMountCapacity ; i ++){
			canonMount[i] = transform.FindChild("mountT" + i);
			canonTypes[i] = canonTypes[i];
			
		}
		// Give an intitial value to canon types
		
		// Set array for canons
		canonMounted = new GameObject[canonMountCapacity];
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;
		
		for(int i = 0 ; i < canonMountCapacity/2 ; i++){
			mountCanon(i);
		}
		/*for (int i = 0 ; i < canonMountCapacity ; i ++){
			Weapons_Base weaponScript = canonMount[i].GetComponent<Weapons_Base>();
			weaponScript.setUpStates(0,0,0);
			
		}*/
		
	}
}
