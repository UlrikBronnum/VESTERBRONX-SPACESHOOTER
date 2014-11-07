using UnityEngine;
using System.Collections;

public class Planet_Two_Level : Mission_Level {
	
	public override void loadLevel()
	{
		levelNames = new string[8];
		levelNames[0] = "Level 1";
		levelNames[1] = "Level 2";
		levelNames[2] = "Level 3";
		levelNames[3] = "Level 4";
		levelNames[4] = "Level 5";
		levelNames[5] = "Level 6";
		levelNames[6] = "Level 7";
		levelNames[7] = "Level 8";
		
		if(levels.Count == 0){
			setLevels();
		}
		
		levelLoaded = true;
		completed = false;
		
		planetState = "Home";
		player = GameObject.Find("ARCamera");
		script = player.GetComponent<Player_Charactor>();
		GameObject image = GameObject.Find ("ImageTarget");
		
		string newProp;
		Vector3 newScale;
		Vector3 newPosition;
		Vector3 newRotation;
		
		newProp = "PlanetChain";
		newScale = new Vector3(150,150,150);
		newPosition = new Vector3(0,0,0);
		newRotation = new Vector3(0,0,0);
		createSceneObject(newProp,newScale,newPosition,newRotation,image.transform);
		transform.parent = transform;
		
		
		swipeScript = props[0].GetComponent<CameraSwipe>();
		
	}
	
	
	public override void setLevels()
	{
		Level_9 newLevel0 = gameObject.AddComponent("Level_9") as Level_9;
		levels.Add(newLevel0);
		Level_10 newLevel1 = gameObject.AddComponent("Level_10") as Level_10;
		levels.Add(newLevel1);
		Level_11 newLevel2 = gameObject.AddComponent("Level_11") as Level_11;
		levels.Add(newLevel2);
		Level_12 newLevel3 = gameObject.AddComponent("Level_12") as Level_12;
		levels.Add(newLevel3);
		Level_13 newLevel4 = gameObject.AddComponent("Level_13") as Level_13;
		levels.Add(newLevel4);
		Level_14 newLevel5 = gameObject.AddComponent("Level_14") as Level_14;
		levels.Add(newLevel5);
		Level_15 newLevel6 = gameObject.AddComponent("Level_15") as Level_15;
		levels.Add(newLevel6);
		Level_16 newLevel7 = gameObject.AddComponent("Level_16") as Level_16;
		levels.Add(newLevel7);
		
	}
}
