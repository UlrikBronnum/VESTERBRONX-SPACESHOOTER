
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_ShowEnemy : MonoBehaviour {
	
	protected Vector3 newScale;
	protected Vector3 newPosition;
	protected Vector3 newRotation;
	protected string newProp;
	
	protected List<GameObject> props = new List<GameObject>();
	
	protected string[] enemiesInWorld;
	
	
	public void showEnemy(string[] en, int size){
		enemiesInWorld =  en;
		loadEnemies(size);
	}
	public void Update(){
		foreach (GameObject element in props){
			element.rigidbody.AddTorque(transform.forward  * 5 * Time.deltaTime,ForceMode.Acceleration);
			//element.transform.Rotate(Vector3.up,45 * Time.deltaTime);
		}
	}
	public void loadEnemies(int howManyEnemyType )
	{

		newScale = new Vector3(5,5,5);
		newPosition = new Vector3(-100,0,0);
		newRotation = new Vector3(115,0,0);
		createSceneObject(enemiesInWorld[0],newScale,newPosition,newRotation,transform);
		props[0].transform.parent = this.transform.parent;
		newScale = new Vector3(5,5,5);
		newPosition = new Vector3(100,0,0);
		newRotation = new Vector3(115,0,0);
		createSceneObject(enemiesInWorld[1],newScale,newPosition,newRotation,transform);
		props[1].transform.parent = this.transform.parent;

		if(howManyEnemyType > 2){
			newScale = new Vector3(5,5,5);
			newPosition = new Vector3(0,-75,15);
			newRotation = new Vector3(115,0,0);
			createSceneObject(enemiesInWorld[2],newScale,newPosition,newRotation,transform);
			props[2].transform.parent = this.transform.parent;

		}
		if(howManyEnemyType > 3){
			newScale = new Vector3(5,5,5);
			newPosition = new Vector3(0,75,-15);
			newRotation = new Vector3(115,0,0);
			createSceneObject(enemiesInWorld[3],newScale,newPosition,newRotation,transform);
			props[3].transform.parent = this.transform.parent;

		}
	}
	
	protected void createSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * scale.x , tmp.transform.localScale.y * scale.y , tmp.transform.localScale.z * scale.z);
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		props.Add(tmp);
	}
	protected void createSceneObject(GameObject gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(gameProp);
		tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * scale.x , tmp.transform.localScale.y * scale.y , tmp.transform.localScale.z * scale.z);
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
		props.Add(tmp);
		
	}
}
