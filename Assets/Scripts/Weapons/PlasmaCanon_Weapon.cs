using UnityEngine;
using System.Collections;

public class PlasmaCanon_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/plasmaGunS") as AudioClip;

		ammoType = "Space/Projectile_plasma";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 3000;
		// Damage of projetile
		projectileDamage = 110;
		// the rate of fire value
		rateOfFire = 1f;
		// magasin capacity
		magCapacity = 100;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
