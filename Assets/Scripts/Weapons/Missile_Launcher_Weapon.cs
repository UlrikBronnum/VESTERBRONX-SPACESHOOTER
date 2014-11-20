using UnityEngine;
using System.Collections;

public class Missile_Launcher_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/plasmaLazerS") as AudioClip;
		
		ammoType = "Space/Projectile_launcher";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 4000;
		// Damage of projetile
		projectileDamage = 290;
		// the rate of fire value
		rateOfFire = 1f;
		// magasin capacity
		magCapacity = 75;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
