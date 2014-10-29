using UnityEngine;
using System.Collections;

public class ProjectileCanon_Script : Weapons_Base {

	public override void Start () 
	{
		ammoType = "Projectile1";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 1500;
		// Damage of projetile
		projectileDamage = 1;
		// the rate of fire value
		rateOfFire = 1/2f;
		// magasin capacity
		magCapacity = 5000;

		fireTimer = new EventTimer_Base(rateOfFire);
	}

	public void update(){
		
		print ("Rate OF FIRE: " + rateOfFire);
	}
}
