using UnityEngine;
using System.Collections;

public class SpawnControl_Props : SpawnControl_Base {

	protected virtual void Start () {
		timer = new EventTimer_Base(spawnRate);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(timer.timerTick()){
			timer.TimerValue = spawnRate;
			spawnBase.Spawn();
		}
	}
}
