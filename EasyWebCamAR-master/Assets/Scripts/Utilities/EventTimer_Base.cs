﻿using UnityEngine;
using System.Collections;
using System.IO;

public class EventTimer_Base {

	private float timerValue;
	private float _timer;

	public EventTimer_Base(float timeInterval) //starting a new time
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
	public float TimerValue{
		get { return timerValue;}
		set { timerValue = value;}
	}
}