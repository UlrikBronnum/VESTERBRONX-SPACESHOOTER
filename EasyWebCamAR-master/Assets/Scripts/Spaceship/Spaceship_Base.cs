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


	// The names of the canon and shield prefabs that is mounted on spaceship.
	// Needs to be public for ease access
	public string[] canonTypes;
	public List<string> shieldTypes = new List<string>();
	// The transform where canons spawn on prefab
	protected Transform[] canonMount;
	// The canons that are mounted on spaceship
	protected GameObject[] canonMounted;
	protected GameObject[] shieldMounted;

	// How many canon mounts does the ship have
	protected int canonMountCapacity;
	// Determines if the ship are active in the scene
	// and should move
	protected bool isActive = false;
	protected bool gotShield = false;
	// Scale the canons to the spaceships scale
	protected Transform canonScale;
	// How fast are maneuvers
	protected float maneuverSpeed;
	// Use for stabilizing ship turn animation
	protected float spaceshipRotation;


	public virtual void shipInitialization(){}
	public virtual void Update () {}

	// Instantiates canons on 
	public void initializeCanon(Transform scale, int i){
		canonMounted[i] = (GameObject)Object.Instantiate(Resources.Load(canonTypes[i]));
		Transform thisTrans = canonMounted[i].transform;
		canonMounted[i].transform.localScale = new Vector3(thisTrans.localScale.x * scale.localScale.x ,thisTrans.localScale.y * scale.localScale.y , thisTrans.localScale.z * scale.localScale.z);
		canonMounted[i].transform.position = canonMount[i].position;
		canonMounted[i].transform.rotation = canonMount[i].rotation;
		canonMounted[i].transform.parent = canonMount[i].transform;

	}
	public void mountCanon(int mount){
		
		removeCanon(mount);
		if(mount == 0){
			initializeCanon(canonScale,0);
			initializeCanon(canonScale,1);
		}else if(mount == 1){
			initializeCanon(canonScale,2);
			initializeCanon(canonScale,3);
		}
	}

	// Destroys unwanted canon prefabs
	public void removeCanon(int gun){
		if(gun == 0){
			Destroy(canonMounted[0]);
			Destroy(canonMounted[1]);
		}else if(gun == 1){
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
	public void gunSetting(string[] newSetting){ 
		for(int i = 0; i < canonMountCapacity ; i++){
			canonTypes[i] = newSetting[i];	
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
	public bool GotShield(){
		if(shieldTypes.Count != 0){
			return true;
		}else{
			return false;
		}
	}


}
