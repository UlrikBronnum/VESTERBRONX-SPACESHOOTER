using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int Health = 200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDamage(int damage){
				Health -= damage;
		
				if (Health <= 0) {
						die ();
				}
		}

		public void die()
		{
			
			Destroy (gameObject);
			
		}

}
