using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moon_Planet : Planet_Base {

	private GameObject moon;
	private float moonOrbitSpeed;
	private float moonSize;

	public override void Start(){
		axisRotationSpeed = 5f;
		lightOrbitSpeed = 24f;
		moonOrbitSpeed = 20f;
		transform.Rotate(Vector3.forward * (0));
		moon = (GameObject) Object.Instantiate(Resources.Load("Moon"));
		moon.transform.parent = transform;
		moon.transform.position = new Vector3(transform.position.x + transform.localScale.x , transform.position.y , transform.position.z);
		moon.transform.localScale = new Vector3(transform.localScale.x / 4 , transform.localScale.y / 4 , transform.localScale.z / 4);
		createLight(new Vector3(10,0,0));
	}
	public override void Update(){
		transform.Rotate(Vector3.left * axisRotationSpeed * Time.deltaTime);
		moon.transform.RotateAround(moon.transform.parent.position,moon.transform.parent.localPosition,moonOrbitSpeed * Time.deltaTime);
		planetLight.transform.RotateAround(transform.position,Vector3.up, lightOrbitSpeed * Time.deltaTime);
		planetLight.transform.LookAt(transform.position);
	}

}
