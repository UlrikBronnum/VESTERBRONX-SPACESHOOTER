using UnityEngine;
using System.Collections;

public class SkydebaneCanon_Weapon : Weapons_Base {
		
		public override void forceStart () 
		{
			barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/CanonS") as AudioClip;

		ammoType = "VesterBro/Projectile_canon";
			// upgradeStates = { rate of fire , damage , capacity }
			// will range from 0 to topLimit?
			// the purchase value of the weapon
			weaponValue = 8000;
			// Damage of projetile
			projectileDamage = 100;
			// the rate of fire value
			rateOfFire = 1/6f;
			// magasin capacity
			magCapacity = 100;
			
	//		fireTimer = new Weapon_Timer(weaponRateOfFire());
		}
}
