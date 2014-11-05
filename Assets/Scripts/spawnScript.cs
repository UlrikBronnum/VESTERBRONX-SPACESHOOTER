using UnityEngine;
using System.Collections;

public class spawnScript : MonoBehaviour {

	public Rigidbody Enemy;
	public float time = 0;
	public int spawnSpeed = 5;
	public int enemyCount= 0; 
	private GameObject go;


	// Use this for initialization
	void Start () {
		go = GameObject.Find ("SAPCE2");
		this.gameObject.transform.parent = go.transform;

	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time > spawnSpeed) {
			time = 0f;
			enemyCount+=1;
			Spawn();

				}

		if (enemyCount > 10) {

			spawnSpeed -=1;
			enemyCount = 0;
				}


	
	}

	void Spawn(){

		Rigidbody clone;


		//Instantiate((GameObject)Resources.Load("Sphere"), transform.position + transform.forward*3.5f, transform.rotation);
		clone = Instantiate(Enemy, new Vector3(Random.Range(-120.0F, 120.0F),-31.0f , Random.Range(-60.0F, 50.0F)) , Enemy.transform.rotation) as Rigidbody;
		//clone.velocity =Enemy.transform.TransformDirection(Vector3.forward * 60);


		}

}
