using UnityEngine;
using System.Collections;

public class DurumCanon_Weapon : Weapons_Base {
	
	public override void forceStart () 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/durumGun") as AudioClip;

		ammoType = "VesterBro/Projectile_durum";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 500;
		// Damage of projetile
		projectileDamage = 200;
		// the rate of fire value
		rateOfFire = 2.5f;
		// magasin capacity
		magCapacity = 20;
		
	//	fireTimer = new Weapon_Timer(weaponRateOfFire());
	}
}
