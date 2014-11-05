using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.position.y < 0) {
			Destroy(this.gameObject);
			Instantiate(explosion,transform.position, new Quaternion());
			}


	
	}


	void OnCollisionEnter(Collision other)
	{
		//If the enemy have the tag posh run this
		if(other.collider.tag =="Enemy")
		{


			Instantiate(explosion,transform.position, new Quaternion());
			//Run a function to subtract damage from the enemy's health, and destroy the projectile afterwards
			other.collider.GetComponent<EnemyScript>().takeDamage(75);
			Destroy(gameObject);
		}
	}
}
