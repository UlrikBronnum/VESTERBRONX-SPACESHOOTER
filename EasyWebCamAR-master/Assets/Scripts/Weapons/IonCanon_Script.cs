using UnityEngine;
using System.Collections;

public class IonCanon_Script : Weapons_Base {

	public override void Start () {
		rateOfFire = 3/10.0f;
		fireTimer = new EventTimer_Base(rateOfFire);
	}
}
