using UnityEngine;
using System.Collections;

public class MiniGun_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/shotgunSound") as AudioClip;

		ammoType = "Space/Projectile_Mini";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 2000;
		// Damage of projetile
		projectileDamage = 60;
		// the rate of fire value
		rateOfFire = 1f;
		// magasin capacity
		magCapacity = 1000;

	//	StartCoroutine (Shoot (weaponRateOfFire()));

		//fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
