using UnityEngine;
using System.Collections;

public class EnemyCanon : EnemyWeapon_Base {

	public override void forceStart (float newFire, int damage) 
	{
		barrelEnd = transform.FindChild("barrelEnd").transform;
		fireExplosion = Resources.Load("Audio/shotgunSound") as AudioClip;

		ammoType = "Projectile1";
		// Damage of projetile
		projectileDamage = damage;
		// the rate of fire value
		rateOfFire = newFire;
		// magasin capacity
		
		fireTimer = new Weapon_Timer(rateOfFire);
	}
}
