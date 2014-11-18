using UnityEngine;
using System.Collections;

public class Missile_Ammo : Projectile_Base {
	
	protected Transform enemyPos;
	protected Vector3 pos;


	// Use this for initialization
	public override void Start () {

		flyTime = 7f;
		projectileVelocity = 30;
		timer = new EventTimer_Base(flyTime);

		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("EnemySpawn(Clone)") != null ){
			if(GameObject.Find("EnemySpawn(Clone)").transform.childCount != 0){
				pos = GameObject.Find("EnemySpawn(Clone)").transform.GetChild(0).position;
				transform.position = Vector3.MoveTowards(transform.position, pos, projectileVelocity);
			}
		}
		if(timer.timerTick()){
			Destroy(gameObject);
		}
	}
	
}
