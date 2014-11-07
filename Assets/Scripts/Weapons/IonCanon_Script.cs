﻿using UnityEngine;
using System.Collections;

public class IonCanon_Script : Weapons_Base {


	public override void forceStart () 
	{
		ammoType = "Projectile1";
		// upgradeStates = { rate of fire , damage , capacity }
		// will range from 0 to topLimit?
		// the purchase value of the weapon
		weaponValue = 3000;
		// Damage of projetile
		projectileDamage = 20;
		// the rate of fire value
		rateOfFire = 10f;
		// magasin capacity
		magCapacity = 5000;
		
		fireTimer = new Weapon_Timer(weaponRateOfFire());
	}



}