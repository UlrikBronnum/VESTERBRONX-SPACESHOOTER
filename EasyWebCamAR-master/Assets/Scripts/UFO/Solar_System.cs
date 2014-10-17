using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Solar_System : Planet_Base {

	protected GameObject player; 
	protected Player_Charactor script;
	/*
	public List<GameObject> moonObjects = new List<GameObject>();
	public float moonOrbit;

	private float[] sizes = {5,12,13,7,20,9,6,6};
	private float[] spacing = {35,75,120,170,8,6,4,2};
	private float[] speed = {30,10,20,5,8,6,4,2};
	
	private string[] propName = {"Mercury","Venus","Earth","Mars","AstroidBelt" };

	public void loadSolarSystem()
	{
	


		
		string newProp = "Sun";
		Vector3 newScale = new Vector3(20,20,20);
		Vector3 newPosition = new Vector3(0,-50,200);
		Vector3 newRotation = new Vector3(125,-30,-75);
		createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);
		for (int i = 0; i < 5; i++) {
			newProp = propName[i];
			newScale = new Vector3(sizes[i],sizes[i],sizes[i]);
			newPosition = new Vector3(spacing[i],0,0);
			newRotation = new Vector3(-145,20,75);
			createSceneObject(newProp,newScale,newPosition,newRotation,moonObjects[0].transform);
			moonObjects[i + 1].transform.parent = moonObjects[0].transform;
			if(propName[i] == "Earth"){
				Planet_Base planet = moonObjects[i + 1].GetComponent<Planet_Base>();
				planet.loadPlanet(new Vector3 (0f,sizes[i],0) , speed[i] , "Moon" , new Vector3(sizes[i]/4,sizes[i]/4,sizes[i]/4) , new Vector3(18f,0,0) , new Vector3(0,sizes[i] * 5,0));
			}else if(propName[i] == "AstroidBelt"){
				AstroidBelt_Planet planet = moonObjects[i + 1].GetComponent<AstroidBelt_Planet>();
				planet.loadAstroidBelt(moonObjects[0].transform);
			}else{
				Planet_Base planet = moonObjects[i + 1].GetComponent<Planet_Base>();
				planet.loadPlanet(new Vector3 (0f,sizes[i],0),speed[i]);
			}
		}
	}
	public override void Update(){
		
		transform.Rotate(new Vector3(1,0,0) * rotationSpeed.x * Time.deltaTime);
		transform.Rotate(new Vector3(0,1,0) * rotationSpeed.y * Time.deltaTime);
		transform.Rotate(new Vector3(0,0,1) * rotationSpeed.z * Time.deltaTime);
		transform.RotateAround(transform.parent.position,Vector3.up, orbitSpeed * Time.deltaTime);
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

	protected void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		tmp.transform.localScale = scale;
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		moonObjects.Add(tmp);
	}
	protected void createSceneObject(GameObject gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(gameProp);
		tmp.transform.localScale = scale;
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		tmp.SetActive(true);
		moonObjects.Add(tmp);
		
	}

	protected void createDirectionalLightInScene(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,
	                                             Transform cameraTransform, Color lightColor)
	{
		GameObject tmp = new GameObject (gameProp);
		tmp.AddComponent<Light>();
		tmp.light.type = LightType.Directional;
		tmp.light.color = Color.white;
		
		tmp.transform.localScale = scale;
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		moonObjects.Add(tmp);
		
		
	}
	*/
}