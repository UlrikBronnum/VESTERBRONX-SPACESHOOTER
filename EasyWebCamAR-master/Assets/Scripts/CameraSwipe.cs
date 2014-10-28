using UnityEngine;
using System.Collections;

public class CameraSwipe : MonoBehaviour {

	Vector2 StartPos;
	int swipeID = -1;
	float minMovement = 150.0f;

	// Use this for initialization

	public int numberOfSwipes = 0;
	public int maxNumberOfSwipes = 7;
	public int targetPos = 0;
	public float speed = 200F;
	public bool swipe = false;

	void Update() {


		// registers the direction of the swipe:
		foreach (var T in Input.touches) {
			var P = T.position;
			if (T.phase == TouchPhase.Began && swipeID == -1) {
				swipeID = T.fingerId;
				StartPos = P;
			} else if (T.fingerId == swipeID) {
				var delta = P - StartPos;
				if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement) {

					swipeID = -1;
					if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
						if (delta.x > 0) {
							if(numberOfSwipes>=1){
							Debug.Log ("Swipe Right Found");
							swipe = true;
							targetPos +=300;
							numberOfSwipes-=1;
							}
						} else {
							if(numberOfSwipes<maxNumberOfSwipes){
							Debug.Log ("Swipe Left Found");
							swipe = true;
							targetPos -=300;
							numberOfSwipes+=1;
							}
						}
					} 
					else {
						if (delta.y > 0) {
							
							Debug.Log ("Swipe Up Found");
						} else {
							
							Debug.Log ("Swipe Down Found");
						}
					}
				
				} else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
					swipeID = -1;
			} 
		}


		// if a swipe is registered in left or the right direction, the object is moved to the target position and swipe is false when it is at the target position. 
		if (swipe) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos,transform.position.y,transform.position.z), speed);
			if (transform.position == new Vector3(targetPos,transform.position.y,transform.position.z)){
				swipe = false;
			}
		}
	}

	public int NumberOfSwipes{
		get{ return numberOfSwipes;}
	}
}
