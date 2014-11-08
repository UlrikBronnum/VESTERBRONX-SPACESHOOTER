using UnityEngine;
using System.Collections;

public class EnemyFirstClass : Spaceship_Enemy {


	public override void forceStart()
	{
		cameraPos = GameObject.Find ("ARCamera").transform;
		
		renderer.material.shader = Shader.Find("Game/Spaceship_Shader");
		renderer.material.SetColor("_Texture_Blend_Color" , Color.white);
		renderer.material.SetColor("_Texture_Override_Color" , Color.red);
		renderer.material.SetColor("_Shield_Blend_Color" ,new Color(0.0f,0.2f,1f,0.0f));
		renderer.material.SetFloat("_Shield_Blend" , 0f);

		canonScale = transform;
		
		health = 300;
		shield = 100;
		// Ship speed
		maneuverSpeed = 200f;
		// sets the rate of fire for the guns of this Enemy:
		fireRate = 4f;
		damage = 50;
		
		// damage inflicted if the player collides with this enemy
		collisionDamage = (int)(health/4f);
		// Amount of gun attachments 
		canonMountCapacity = 2;
		
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonTypes = new string[canonMountCapacity];
		
		// the guns of this enemy:
		canonTypes[0] = "Enemies/enemyCanon";
		canonTypes[1] = "Enemies/enemyCanon";

		
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
		initTimer(10f);
	}


}
