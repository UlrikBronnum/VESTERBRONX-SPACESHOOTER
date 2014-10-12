using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moon_Planet : Planet_Base {

	public List<GameObject> moonObjects = new List<GameObject>();
	public float moonOrbit;




	public override void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               List<string> moonNames, List<Vector3> scale, 
	                               List<Vector3> pos , List<Vector3> turnRotation)
	{
		rotationSpeed = rotSpeed;
		orbitSpeed = orbSpeed;
		for(int i = 0; i < moonNames.Count ; i++){
			createSceneObject(moonNames[i], scale[i],pos[i] ,turnRotation[i]);
		}
	}
	public override void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               string moonNames, Vector3 scale, 
	                               Vector3 pos , Vector3 turnRotation)
	{
		rotationSpeed = rotSpeed;
		orbitSpeed = orbSpeed;
		createSceneObject(moonNames, scale, pos ,turnRotation);
		Planet_Base script = moonObjects[0].GetComponent<Planet_Base>();
		script.loadPlanet(new Vector3 (0f,0f,0f),90f);

	}
	public override void Update(){

		transform.Rotate(new Vector3(1,0,0) * rotationSpeed.x * Time.deltaTime);
		transform.Rotate(new Vector3(0,1,0) * rotationSpeed.y * Time.deltaTime);
		transform.Rotate(new Vector3(0,0,1) * rotationSpeed.z * Time.deltaTime);
		transform.RotateAround(transform.position,Vector3.up, orbitSpeed * Time.deltaTime);
	}
	protected void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation)
	{
	
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		tmp.transform.localScale = scale;
		Vector3 newPos = transform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		tmp.transform.rotation = transform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		tmp.transform.parent = transform;
		moonObjects.Add(tmp);
	}
}
