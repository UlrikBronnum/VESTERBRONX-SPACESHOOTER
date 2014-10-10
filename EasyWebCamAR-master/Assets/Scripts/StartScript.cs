using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	public Rigidbody Enemy;
	// Use this for initialization
	void Start () {

		Rigidbody clone;
		
		
		//Instantiate((GameObject)Resources.Load("Sphere"), transform.position + transform.forward*3.5f, transform.rotation);
		clone = Instantiate(Enemy, new Vector3(Random.Range(-120.0F, 120.0F),-31.0f , Random.Range(-60.0F, 50.0F)) , Enemy.transform.rotation) as Rigidbody;
		clone.velocity =Enemy.transform.TransformDirection(Vector3.forward * 60);

	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
