using UnityEngine;
using System.Collections;

public class SpawnControl_Props : SpawnControl_Base {

	public SpawnClass_Base spawnBase;
	protected virtual void Start () 
	{
		spawnRate = 3f;
		timer = new EventTimer_Base(spawnRate);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(timer.timerTick()){
			timer.TimerValue = spawnRate;
			spawnBase.Spawn(0);
		}
	}
}
