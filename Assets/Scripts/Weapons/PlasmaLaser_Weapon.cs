using UnityEngine;
using System.Collections;

public class PlasmaLaser_Weapon: Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/plasmaGunS") as AudioClip;
		
		ammoType = "Space/Projectile_laser";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 16000;
		// Damage of projetile
		projectileDamage = 260;
		// the rate of fire value
		rateOfFire = 1/3f;
		// magasin capacity
		magCapacity = 200;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
