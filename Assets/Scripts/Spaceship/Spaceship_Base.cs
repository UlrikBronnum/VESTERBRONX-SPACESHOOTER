using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Spaceship_Base : MonoBehaviour {


	/// <summary>
	/// This is the base class of Spaceships. All spaceship
	/// will inherits from this base class. The morphology will
	/// split in a player specific and a enemy specific class
	/// 
	/// Don't write new code here
	/// </summary>

	protected string standartCanon = "projectileCanon";

	// The names of the canon and shield prefabs that is mounted on spaceship.
	// Needs to be public for ease access
	public string[] canonTypes;
	// The transform where canons spawn on prefab
	protected Transform[] canonMount;
	// The canons that are mounted on spaceship
	protected GameObject[] canonMounted;

	// How many canon mounts does the ship have
	protected int canonMountCapacity;
	// Determines if the ship are active in the scene
	// and should move
	protected bool isActive = false;
	// Scale the canons to the spaceships scale
	protected Transform canonScale;
	// How fast are maneuvers
	protected float maneuverSpeed;
	// Use for stabilizing ship turn animation
	protected float spaceshipRotation;
	// health of the player/enemy
	protected int health;
	protected int shield;

	
	public int shipInGameHealth;
	public int shipInGameShield;

	protected Shield_Timer hitTimer = new Shield_Timer(0.25f);

	public virtual void initializeCanon(Transform scale, int i){}
	public virtual void shipInitialization(){}
	public virtual void Update () {}

	public void manageShader(){
	
		renderer.material.shader = Shader.Find("Game/Spaceship_Shader");
		if(shipInGameShield > 0 && shipShield() > 0){
			float shieldLeft = (float)shipInGameShield/shipShield();
			renderer.material.SetFloat("_Shield_Left" , shieldLeft);
		}else{
			renderer.material.SetFloat("_Shield_left" , 0f);
		}
		if(hitTimer.timerTick())
			renderer.material.SetFloat("_Shield_Blend" , 0f);


	}

	// Instantiates canons on 

	public void mountCanon(int mount){
		if(mount == 0){
			initializeCanon(canonScale,0);
			initializeCanon(canonScale,1);
		}else if(mount == 1){
			initializeCanon(canonScale,2);
			initializeCanon(canonScale,3);
		}
	}


	// Destroys unwanted canon prefabs
	public void removeCanon(int mount){
		if(mount == 0){
			Destroy(canonMounted[0]);
			Destroy(canonMounted[1]);
		}else if(mount == 1){
			Destroy(canonMounted[2]);
			Destroy(canonMounted[3]);
		}
	}
	// Overloaded func changes canon types
	// by canon racks 
	public void gunSetting(string newSetting, int mount){ 
		if(mount == 0){
			canonTypes[0] = newSetting;
			canonTypes[1] = newSetting;
		}else if(mount == 1){
			canonTypes[2] = newSetting;
			canonTypes[3] = newSetting;
		}
	}
	// Overloaded func changes canon types
	// of all racks
	public void gunSetting(string newSetting){ 
		for(int i = 0; i < canonMountCapacity ; i++){
			canonTypes[i] = newSetting;	
		}

	}
	// get func for shipCapacity
	public int CanonMountCapacity{
		get {return canonMountCapacity;}
	}
	// get func for isActive
	// set func for isActive
	public bool IsActive{
		get {return isActive;}
		set {isActive = value;}
	}
	public int Health{
		get {return health;}
		set {health = value;}
	}
	public int Shield{
		get {return shield;}
		set {shield = value;}
	}

	// function that makes sure the enemies or player takes damage 
	public virtual void takeDamage(int damage){
		
	}
	public virtual int shipHealth(){
		return 0;
	}
	
	public virtual int shipShield(){
		return 0;

	}
	
	public virtual float shipManeuverSpeed(){
		return 0f;

	}

}
