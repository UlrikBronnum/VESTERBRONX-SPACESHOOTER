using UnityEngine;
using System.Collections;

public class EnemySecondClass : Spaceship_Enemy {
	
	public override void Start() { 
		cameraPos = GameObject.Find ("ARCamera").transform;

		renderer.material.shader = Shader.Find("Specular");
		renderer.material.SetColor("_Color" , Color.red);

		canonScale = transform;
		health = 400;
		// Ship speed
		maneuverSpeed = 300f;
		// Amount of gun attachments 
		canonMountCapacity = transform.childCount;
		
		
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];

		canonTypes[0] = "projectileCanon";
		canonTypes[1] = "projectileCanon";
		canonTypes[2] = "projectileCanon";
		canonTypes[3] = "projectileCanon";


		
		for (int i = 0 ; i < transform.childCount ; i ++){
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
	}
	public virtual void shipInitialization(){
		
		
		
	}
}
