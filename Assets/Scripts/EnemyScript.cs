using UnityEngine;
using System.Collections;

public abstract class EnemyScript : MonoBehaviour {

	public int Health;
	protected GameObject CameraPos;
	public GameObject projectile;
	public int ShootSpeed;
	protected float time;
	protected float time2;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {







		/*if (time2 > spawnSpeed) {
			time2 = 0f;
			enemyCount+=1;
			Spawn();
			
		}
		
		if (enemyCount > 10) {
			
			spawnSpeed -=1;
			enemyCount = 0;
		}*/


	
	}

	public void takeDamage(int damage){
		Health -= damage;
		
		if (Health <= 0) {
			die();
		}
		
	}
	
	protected void die()
	{

		Destroy (gameObject);
		
	}

	protected void Shoot(){

		Rigidbody clone;
		
		//Instantiate((GameObject)Resources.Load("Sphere"), transform.position + transform.forward*3.5f, transform.rotation);
		clone = Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) , this.transform.rotation) as Rigidbody;


		}

	/*protected void Spawn(){
		
		Rigidbody clone;
		
		

		clone = Instantiate(Enemy, new Vector3(Random.Range(-120.0F, 120.0F),-31.0f , Random.Range(-60.0F, 50.0F)) , Enemy.transform.rotation) as Rigidbody;
		clone.velocity =Enemy.transform.TransformDirection(Vector3.forward * 60);
		
		
	}*/

	protected void CheckPosition(){
		// deletes the enemy if it flies past the camera: 
		if (this.transform.position.y > CameraPos.transform.position.y) {
			print("YEAH");
			die ();
		}

		}



}
