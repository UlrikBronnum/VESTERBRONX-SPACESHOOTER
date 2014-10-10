using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Proto_Planet : Planet_Base {


	public virtual void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               List<string> moonNames, List<Vector3> scale, 
	                               List<Vector3> pos , List<Vector3> turnRotation){}

	public virtual void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               string moonNames, Vector3 scale, 
	                               Vector3 pos , Vector3 turnRotation)	{}

	protected virtual void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation){}

	public override void loadPlanet(Vector3 rotSpeed, float orbSpeed){
		rotationSpeed = rotSpeed;
		orbitSpeed = orbSpeed;
	}
	public override void Update(){
		transform.Rotate(new Vector3(1,0,0) * rotationSpeed.x * Time.deltaTime);
		transform.Rotate(new Vector3(0,1,0) * rotationSpeed.y * Time.deltaTime);
		transform.Rotate(new Vector3(0,0,1) * rotationSpeed.z * Time.deltaTime);
		transform.RotateAround(transform.parent.position,Vector3.up, orbitSpeed * Time.deltaTime);
	}
}
