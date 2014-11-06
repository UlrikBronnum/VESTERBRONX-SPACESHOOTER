using UnityEngine;
using System.Collections;

public class EnemyWeapon_Base : MonoBehaviour {

	public Transform barrelEnd;
	public AudioClip fireExplosion;
	
	public string ammoType;
	// upgradeStates = { rate of fire , damage , capacity }
	// will range from 0 to topLimit?
	// the purchase value of the weapon
	// Damage of projetile
	public int projectileDamage;
	// the rate of fire value
	public float rateOfFire;
	// magasin capacity
	
	protected Weapon_Timer fireTimer;
	
	
	
	// Use this for initialization
	public virtual void forceStart (float newFire, int damage) {}

	public void fireWeapon(){
		if(fireTimer.timerTick()){
			audio.PlayOneShot(fireExplosion);
			GameObject newShot = (GameObject) Object.Instantiate(Resources.Load(ammoType));
			newShot.tag = "EnemyProjectile";
			newShot.layer = LayerMask.NameToLayer("EnemyProjectile");
			Projectile_Base script = newShot.GetComponent<Projectile_Base>();
			script.setProjectileDamage(projectileDamage);
			newShot.transform.position = barrelEnd.position;
			newShot.transform.rotation = barrelEnd.rotation;
		}
	}
	

}
