using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Solar_System : Planet_Base {

	protected GameObject player; 
	protected Player_Charactor script;

	public List<GameObject> moonObjects = new List<GameObject>();
	public float moonOrbit;
	public int numberOfChildren;

	private float sizes = 150;
	private float[] spacing = {300,600,900,1200,1500,1800,2100,2400};

	private string[] propName = {"Sun","Sun","Sun","Sun","Sun","Sun","Sun","Sun" };

	public void Start()
	{
	

		player = GameObject.Find("ImageTarget");
		
		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;

		for (int i = 0; i < numberOfChildren; i++) {
			newProp = propName[i];
			newScale = new Vector3(sizes,sizes,sizes);
			newPosition = new Vector3(spacing[i],0,0);
			newRotation = new Vector3(0,0,0);
			createSceneObject(newProp,newScale,newPosition,newRotation,player.transform);
			moonObjects[i].transform.parent = transform;
	
		}
	}
	public override void Update(){
		
	
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

}