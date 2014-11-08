using UnityEngine;
using System.Collections;

public class EnemyProjectile_Base : MonoBehaviour {
	
	protected float projectileVelocity;
	protected EventTimer_Base timer;
	protected float flyTime;
	public int damage;
	
	// Use this for initialization
	public virtual void  Start () {	}
	
	void Update(){
		if(timer.timerTick()){
			Destroy(gameObject);
		}
		Debug.Log(damage);
	}
	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Player"){
			Destroy(gameObject);
		}
	}
	public void setProjectileDamage(int newDamage){
		damage = newDamage;
	}
	
}
