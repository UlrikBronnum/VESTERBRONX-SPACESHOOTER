using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Proto_Planet : Planet_Base {

	public override void Start(){
		axisRotationSpeed = 5f;
		lightOrbitSpeed = 24f;
		transform.Rotate(Vector3.forward * (-15));
	}
	public override void Update(){
		transform.Rotate(Vector3.up * axisRotationSpeed * Time.deltaTime);
		//planetLight.transform.RotateAround(transform.position,Vector3.up, lightOrbitSpeed * Time.deltaTime);
		//planetLight.transform.LookAt(transform.position);
	}
}
