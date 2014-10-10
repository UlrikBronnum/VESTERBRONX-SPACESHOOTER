using UnityEngine;
using System.Collections;

public class SpawnControl_Base : MonoBehaviour {

	
	protected float spawnRate;
	protected EventTimer_Base timer;
	public SpawnClass_Base spawnBase;

	// Use this for initialization
	void Start () {
		spawnRate = 1f;
		timer = new EventTimer_Base(spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer.timerTick()){
			timer.TimerValue = spawnRate;
			spawnBase.Spawn();
		}

	}

}
