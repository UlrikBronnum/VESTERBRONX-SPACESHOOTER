using UnityEngine;
using System.Collections;

public class ProjectileCanon_Script : Weapons_Base {

	public override void Start () {
		rateOfFire = 2/10.0f;
		fireTimer = new EventTimer_Base(rateOfFire);
	}
}
