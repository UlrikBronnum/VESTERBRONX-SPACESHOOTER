using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet_Base : MonoBehaviour {

	protected Vector3 rotationSpeed;
	protected float orbitSpeed;
	public virtual void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               List<string> moonNames, List<Vector3> scale, 
	                               List<Vector3> pos , List<Vector3> turnRotation){}

	public virtual void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                                string moonNames, Vector3 scale, 
	                                Vector3 pos , Vector3 turnRotation)	{}

	public virtual void loadPlanet(Vector3 rotSpeed, float orbitSpeed){}
	protected virtual void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation){}
	public virtual void Update(){}



}
