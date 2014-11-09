using UnityEngine;
using System.Collections;

public class Shield_Timer {
	
	private float timerValue;
	private float _timer;
	public bool timerActive = false;
	
	public Shield_Timer(float timeInterval) //starting a new time
	{
		timerValue = timeInterval;
		_timer = TimerValue;
	}
	public bool timerTick(){
		if(timerActive)
		{
			_timer -= Time.deltaTime;
			if(_timer < 0){
				_timer = timerValue;
				timerActive = false;
				return true;
			}else{
				return false;
			}
		}else
		{
			return false;
		}
	}
	public void resetTimer(){
		timerActive = true;
		_timer = TimerValue;
	}
	public float TimerValue{
		get { return timerValue;}
		set { timerValue = value;}
	}
}
