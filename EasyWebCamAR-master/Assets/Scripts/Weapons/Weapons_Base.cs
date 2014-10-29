using UnityEngine;
using System.Collections;

public class Weapons_Base : MonoBehaviour {

	public Transform barrelEnd;
	public AudioClip fireExplosion;

	public string ammoType;
	// upgradeStates = { rate of fire , damage , capacity }
	// will range from 0 to topLimit?
	public int[] upgradeStates = new int[3];
	// the purchase value of the weapon
	public int weaponValue;
	// Damage of projetile
	public int projectileDamage;
	// the rate of fire value
	public float rateOfFire;
	// magasin capacity
	public int magCapacity;

	public EventTimer_Base fireTimer;



	// Use this for initialization
	public virtual void Start () {}
	

	public void fireWeapon(){
		if(fireTimer.timerTick()){
			audio.PlayOneShot(fireExplosion);
			GameObject newShot = (GameObject) Object.Instantiate(Resources.Load(ammoType));
			Projectile_Base script = newShot.GetComponent<Projectile_Base>();
			script.setProjectileDamage(weaponDamage());
			newShot.transform.position = barrelEnd.position;
			newShot.transform.rotation = barrelEnd.rotation;
			Debug.Log(weaponRateOfFire() + " " + weaponDamage() + " " + weaponCapacity());
		}
	}

	public void EnemyFireWeapon(){
		if(fireTimer.timerTick()){
			audio.PlayOneShot(fireExplosion);
			GameObject newShot = (GameObject) Object.Instantiate(Resources.Load(ammoType));
			newShot.tag = "EnemyProjectile";
			newShot.layer =LayerMask.NameToLayer("EnemyProjectile");
			Projectile_Base script = newShot.GetComponent<Projectile_Base>();
			script.setProjectileDamage(weaponDamage());
			newShot.transform.position = barrelEnd.position;
			newShot.transform.rotation = barrelEnd.rotation;
			Debug.Log(weaponRateOfFire() + " " + weaponDamage() + " " + weaponCapacity());
		}
	}

	public void setUpStates(int up1, int up2, int up3){
		upgradeStates[0] = up1;
		upgradeStates[1] = up2;
		upgradeStates[2] = up3;
	}
	public float weaponRateOfFire(){
		float wROF = rateOfFire + (rateOfFire * (upgradeStates[0] / 10.0f));
		return wROF;
	}

	public int weaponDamage(){
		int wDam = projectileDamage + (int) (projectileDamage * (upgradeStates[1] / 10.0f));
		return wDam;
	}

	public int weaponCapacity(){
		int wCap = magCapacity + (int) (magCapacity * (upgradeStates[2] / 10.0f));
		return wCap;
	}





}
