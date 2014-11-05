using UnityEngine;
using System.Collections;

public class JoyMove : MonoBehaviour {

	// Use this for initialization
	public Joystick joystick;
	public float speed = 10;
	public bool useAxisInput = true;
	public float h = 0;
	

	void Start()
	{
	
		joystick = GameObject.Find("joystick").GetComponent<Joystick>();


	}

	void Update () {


		if(!useAxisInput) {
			// assigns the position of the joystick to h and v
			h = joystick.position.x;
		}
		else {
			h = Input.GetAxis("Horizontal");
		}

		// uses the position of the joystick to move the player:  
		if(h < 0) {
			rigidbody.velocity = new Vector3(h * speed, 0, rigidbody.velocity.y);
		}

		if(h > 0) {
			rigidbody.velocity = new Vector3(h * speed, 0, -rigidbody.velocity.y);
		}

		if (joystick.position.x == 0){

			rigidbody.velocity = new Vector3(0, 0, 0);
				}

	}
}
