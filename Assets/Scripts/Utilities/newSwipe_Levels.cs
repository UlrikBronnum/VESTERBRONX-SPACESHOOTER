using UnityEngine;
using System.Collections;

public class newSwipe_Levels  : MonoBehaviour {
	

	
	// Use this for initialization
	/*
	public int numberOfSwipes = 0;
	public int maxNumberOfSwipes = 7;
	public int targetPos = 0;
	public float speed = 2000F;
*/
	private bool isActive;
	private bool swipe = false;
	private float swipeCompleted;
	private int upperLimit;
	private int swipeCounter = 0;

	private Vector2 StartPos = new Vector2(0f,0f);
	private Vector2 LastPos = new Vector2(0f,0f);
	private int swipeID = -1;
	private int minMovement = 150;

	public void setUpSwipeLimits(int upL, bool iA){
		upperLimit = upL - 1;
		isActive = iA;
	}
	void Update() 
	{
		if(isActive){
			// registers the direction of the swipe:
			foreach (var T in Input.touches) {
				var P = T.position;
				if (T.phase == TouchPhase.Began && swipeID == -1) {
					swipeID = T.fingerId;
					StartPos = P;
				} else if (T.fingerId == swipeID) {
					var delta = P - StartPos;
					if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement) 
					{
						swipeID = -1;
						
						if (delta.x > 0) {
							if(swipeCounter>=1){
								swipeCounter--;
							}
							else{
								swipeCounter = upperLimit;
							}
						} else if (delta.x < 0){
							if(swipeCounter<upperLimit){
								swipeCounter++;
							}else{
								swipeCounter = 0;
							}
						}

						
					} else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended){
						swipeID = -1;
						swipeCompleted = 0f;
					}
				} 
			}
		}
	}
	public bool IsActive{
		get{ return isActive;}
		set{ isActive = value;}
	}
	public int SwipeCounter{
		get{ return swipeCounter;}
	}
	public float SwipeCompleted{
		get{ return swipeCompleted;}
	}
	public bool Swipe{
		get{ return swipe;}
	}
}
