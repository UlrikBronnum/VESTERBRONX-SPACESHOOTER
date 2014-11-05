using UnityEngine;
using System.Collections;

public class Hatchet_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/shotgunSound") as AudioClip;

		ammoType = "Weapons/ButcherCleaver";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 3000;
		// Damage of projetile
		projectileDamage = 110;
		// the rate of fire value
		rateOfFire = 5f;
		// magasin capacity
		magCapacity = 100;
		
		fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
