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

	private bool canShoot = true;
	
	
	// Use this for initialization
	public virtual void forceStart (float newFire, int damage) {}

	public void fireWeapon(){
		if(canShoot){
			StartCoroutine("hasShot");
			audio.PlayOneShot(fireExplosion);
			GameObject newShot = (GameObject) Object.Instantiate(Resources.Load(ammoType));
			newShot.tag = "EnemyProjectile";
			newShot.layer = LayerMask.NameToLayer("EnemyProjectile");
			EnemyProjectile_Base script = newShot.GetComponent<EnemyProjectile_Base>();
			script.setProjectileDamage(projectileDamage);
			newShot.transform.position = barrelEnd.position;
			//newShot.transform.rotation = Quaternion.Euler(180,0,0);
			//newShot.transform.rotation = barrelEnd.rotation;
			//newShot.transform.rotation = new Quaternion(90,0,0);
		}
	}
	IEnumerator hasShot() {
		canShoot = false;
		yield return new WaitForSeconds(rateOfFire);
		canShoot = true;
	}
	

}
