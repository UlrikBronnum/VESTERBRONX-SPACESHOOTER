using UnityEngine;
using System.Collections;

public class Spaceship_Enemy : Spaceship_Base {

	private float objectVelocity;
	private float lifeSpan;
	private EventTimer_Base timer;

	[System.NonSerialized]
	public Enemy_Spawn Parent;
	// Use this for initialization
	public virtual void Start () {

	}
	public override void shipInitialization(){

		
		canonScale = transform;
		
		// Ship speed
		maneuverSpeed = 30f;
		// Amount of gun attachments 
		canonMountCapacity = 2;

		// Give an intitial value to canon types
		canonTypes = new string[canonMountCapacity];
		canonTypes[0] = "projectileCanon";
		canonTypes[1] = "projectileCanon";
		
		// Find the canon mounts on model
		canonMount = new Transform[canonMountCapacity];
		canonMount[0] = transform.FindChild("gunMountLeft1");
		canonMount[1] = transform.FindChild("gunMountRight1");
		// Set array for canons
		canonMounted = new GameObject[canonMountCapacity];
		// Save the initial rotation of ship for reference
		spaceshipRotation = transform.rotation.z;
		
		mountCanon(0);
	}
	// Update is called once per frame
	public virtual void Update () {
		Transform tmp = transform;
		Vector3 tmpPos = tmp.position;
		tmpPos.z += -10 * Time.deltaTime;
		transform.position = tmpPos;
	}
	public void initTimer(float life,float speed){
		lifeSpan = life;
		objectVelocity = speed;
		timer = new EventTimer_Base(lifeSpan);
	}

	public void Spawn(){
		if(this.gameObject != null)
			timer.TimerValue = lifeSpan;
		
		gameObject.SetActive(true);

	}
	
	public void Despawn(){
		gameObject.SetActive(false);
		if(Parent != null)
			Parent.enemyStack.Push (this);
	}
}
