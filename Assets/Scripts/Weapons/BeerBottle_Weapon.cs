using UnityEngine;
using System.Collections;

public class BeerBottle_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/shotgunSound") as AudioClip;

		ammoType = "Weapons/BeerBottle";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 3000;
		// Damage of projetile
		projectileDamage = 250;
		// the rate of fire value
		rateOfFire = 2f;
		// magasin capacity
		magCapacity = 50;
		
		fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
