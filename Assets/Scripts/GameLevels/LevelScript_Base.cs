using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript_Base : MonoBehaviour {

	protected GameObject player;
	protected GameObject background; 
	protected Player_Charactor script;

	protected int numberOfEnemies;
	protected List<GameObject> props = new List<GameObject>();
	
	protected bool completed;

	protected int versionNum;

	protected Texture buttonTexture;
	protected GUIStyle myGUIStyle = new GUIStyle();
	protected Font newFont;

	protected virtual void loadButtons(){}
	protected virtual void unloadButtons(){}
	public virtual void loadLevel(){}
	public virtual void updateLevel(){}
	public virtual void levelGUI(){	}

	public void setMainVars(){
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		background = GameObject.Find("ImageTarget");
		// finds the texture for the buttons
		versionNum = script.gameSetting;

		newFont = script.newFont;
		buttonTexture = script.buttonTexture;
		myGUIStyle.font = newFont ;
		myGUIStyle.normal.textColor = script.textColor;
		myGUIStyle.alignment = TextAnchor.MiddleCenter;
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
	protected void createPlayerSpaceship(GameObject gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform, bool active, bool onStage)
	{
		gameProp.transform.localScale = scale;
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		gameProp.transform.position = newPos;
		gameProp.transform.rotation = cameraTransform.rotation;
		gameProp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		gameProp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		gameProp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		gameProp.SetActive(onStage);
		Spaceship_Player shipScript = gameProp.GetComponent<Spaceship_Player>();
		shipScript.IsActive = active;
	}
	protected void createShopSpaceship(GameObject gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform, bool active)
	{
		gameProp.transform.localScale = scale;
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		gameProp.transform.position = newPos;
		gameProp.transform.rotation = cameraTransform.rotation;
		gameProp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		gameProp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		gameProp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		Spaceship_Player shipScript = gameProp.GetComponent<Spaceship_Player>();
		shipScript.IsActive = active;
		shipScript.shipInitialization();
		props.Add(gameProp);
	}
	protected void createDirectionalLightInScene(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,
	                                  			Transform cameraTransform, Color lightColor)
	{
		GameObject tmp = new GameObject (gameProp);
		tmp.AddComponent<Light>();
		tmp.light.type = LightType.Directional;
		tmp.light.color = lightColor;

		//tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * scale.x , tmp.transform.localScale.y * scale.y , tmp.transform.localScale.z * scale.z);
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

	protected void createScaleSceneObject(string gameProp,Vector3 scale,Vector3 pos,Vector3 turnRotation,Transform cameraTransform)
	{
		GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameProp));
		tmp.transform.localScale = new Vector3(scale.x , scale.y , scale.z);
		Vector3 newPos = cameraTransform.position;
		newPos.x += pos.x;
		newPos.y += pos.y;
		newPos.z += pos.z;
		tmp.transform.position = newPos;
		//tmp.transform.rotation = cameraTransform.rotation;
		tmp.transform.Rotate(new Vector3(1,0,0) * turnRotation.x );
		tmp.transform.Rotate(new Vector3(0,1,0) * turnRotation.y );
		tmp.transform.Rotate(new Vector3(0,0,1) * turnRotation.z );
		props.Add(tmp);
	}

	public bool Completed{
		get{return completed;}
		set{completed = value;}
	}

	protected void closeLevel(){
		foreach(GameObject element in props){
			Destroy(element);
		}
		props.Clear();

	}

}
