using UnityEngine;
using System.Collections;

public class AButton : TouchLogic {

	public bool touch = false;

	void OnTouchBegan () 
	{
	//	print ("The touch has begun " + this.name);
		touch = true;
		//Debug.Log ("the boolean is " + touch);
	}

	void OnTouchEnded () 
	{
	//	print ("The touch has ended " + this.name);
		touch = false;
		//Debug.Log ("the boolean is " + touch);
		
	}
	
}
