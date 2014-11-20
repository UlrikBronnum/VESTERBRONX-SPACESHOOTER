using UnityEngine;
using System.Collections;

public class Missile_Launcher_Ammo : Projectile_Base {
	
	protected Transform enemyPos;
	protected Vector3 pos;


	// Use this for initialization
	public override void Start () {

		flyTime = 7f;
		projectileVelocity = 30;
		timer = new EventTimer_Base(flyTime);

		
	}
	
	// Update is called once per frame
	public override void Update () {

		if (timer.timerTick ()) {
						Destroy (gameObject);
				}
		pos = GameObject.Find("EnemySpawn(Clone)").transform.GetChild(0).position;
		transform.position = Vector3.MoveTowards(transform.position, pos, projectileVelocity);
		
		
	}
	
}
