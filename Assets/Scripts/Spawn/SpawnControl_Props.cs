using UnityEngine;
using System.Collections;

public class SpawnControl_Props : SpawnControl_Base 
{
	int versionControl;

	public SpawnClass_Base spawnBase;

	protected virtual void Start () 
	{
		versionControl = GameObject.Find("ARCamera").GetComponent<Player_Charactor>().gameSetting;
		spawnRate = 2f;
		timer = new EventTimer_Base(spawnRate);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(timer.timerTick()){
			timer.TimerValue = Random.Range(5f,10f);
			spawnBase.Spawn((int)(Random.Range(0.5f,2.5f)) , versionControl);
		}
	}
}
