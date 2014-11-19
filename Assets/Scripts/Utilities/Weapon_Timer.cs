using UnityEngine;
using System.Collections;
using System.IO;


public class Weapon_Timer {
	
	private float timerValue;
	private float _timer;
	private bool hasShot = false;
	
	public Weapon_Timer(float timeInterval) //starting a new time
	{
		timerValue = timeInterval;
		_timer = TimerValue;
	}
	public bool timerTick(){
		_timer -= Time.deltaTime;
		if(_timer < 0){
			_timer = timerValue;
			return true;
		}else{
			return false;
		}
	}

	public void resetTimer(){
		_timer = TimerValue;
	}
	public float TimerValue{
		get { return timerValue;}
		set { timerValue = value;}
	}
}
