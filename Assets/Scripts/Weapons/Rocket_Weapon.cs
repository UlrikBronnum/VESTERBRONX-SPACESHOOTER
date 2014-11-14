using UnityEngine;
using System.Collections;

public class Rocket_Weapon : Weapons_Base {


	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/RocketS") as AudioClip;
		
		ammoType = "Space/Projectile_missile";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 1000;
		// Damage of projetile
		projectileDamage = 200;
		// the rate of fire value
		rateOfFire = 1/2f;
		// magasin capacity
		magCapacity = 100;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
