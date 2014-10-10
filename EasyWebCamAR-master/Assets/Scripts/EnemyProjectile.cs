using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	protected Vector3 CameraPos;
	public GameObject explosion;


	// Use this for initialization
	void Start () {
		CameraPos = GameObject.Find ("SAPCE2").transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards(transform.position, CameraPos, 5);


	}

	void OnCollisionEnter(Collision other)
	{
		//If the enemy have the tag posh run this
		if(other.collider.tag =="Player")
		{
			
			
			Instantiate(explosion,transform.position, new Quaternion());
			//Run a function to subtract damage from the enemy's health, and destroy the projectile afterwards
			other.collider.GetComponent<PlayerScript>().takeDamage(100);
			Destroy(gameObject);
		}
	}
}
