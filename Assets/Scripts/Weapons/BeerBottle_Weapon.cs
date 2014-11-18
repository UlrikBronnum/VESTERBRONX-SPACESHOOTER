using UnityEngine;
using System.Collections;

public class BeerBottle_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/BeerBottleGunS") as AudioClip;

		ammoType = "VesterBro/Projectile_bottle";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 4000;
		// Damage of projetile
		projectileDamage = 250;
		// the rate of fire value
		rateOfFire = 1f;
		// magasin capacity
		magCapacity = 50;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
