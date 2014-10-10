using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AstroidBelt_Planet : Planet_Base {

	private int listSize;
	public List<Planet_Base>  astroids = new List<Planet_Base>();

	public void loadAstroidBelt(Transform newParent){

		listSize = 200;
		
		for (int i = 0; i < listSize; i++){
			GameObject go = (GameObject)Object.Instantiate(Resources.Load("Astroid"));
			Planet_Base createdObject = go.GetComponent<Planet_Base>();
			createdObject.transform.localScale = new Vector3 (Random.Range(1f,3f),Random.Range(1f,3f),Random.Range(1f,3f));
			createdObject.transform.parent = newParent;
			Vector3 newPos = createdObject.transform.position;
			newPos.x += Random.Range(170f,210f);
			createdObject.transform.position = newPos;
			createdObject.transform.RotateAround(createdObject.transform.parent.position, Vector3.up, Random.Range(0.1f,360f));
			Planet_Base script = createdObject.GetComponent<Planet_Base>();
			script.loadPlanet(new Vector3(0f,30f,0),Random.Range(15f,30f));
			astroids.Add(createdObject);
			
		}



	}
	
	public override void loadPlanet(Vector3 rotSpeed, float orbSpeed,
	                               string moonNames, Vector3 scale, 
	                               Vector3 pos , Vector3 turnRotation)	{}
	
	public override void loadPlanet(Vector3 rotSpeed, float orbitSpeed){}
	protected override void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation){}

}
