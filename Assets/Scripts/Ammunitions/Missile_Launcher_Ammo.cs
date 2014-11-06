using UnityEngine;
using System.Collections;

public class Missile_Launcher_Ammo : Projectile_Base {
	
	protected Transform enemyPos;
	protected Vector3 pos;


	// Use this for initialization
	public override void Start () {

		flyTime = 5f;
		projectileVelocity = 10;
		timer = new EventTimer_Base(flyTime);

		
	}
	
	// Update is called once per frame
	void Update () {

		pos = GameObject.Find("EnemySpawn(Clone)").transform.GetChild(0).position;
		transform.position = Vector3.MoveTowards(transform.position, pos, projectileVelocity);
		
		
	}
	
}
