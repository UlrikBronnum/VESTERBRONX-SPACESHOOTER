using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {


	protected AButton aButtonAction;
	protected bool buttonWaitA = true;
	protected float AttackSpeed;
	protected GameObject CameraPos;
	public Rigidbody projectile;

	// Use this for initialization
	void Start () {
		AttackSpeed = 0f;
		aButtonAction = GameObject.Find ("AButton").GetComponent<AButton> ();
		CameraPos = GameObject.Find ("ARCamera");
	}
	
	// Update is called once per frame
	void Update () {



		if((aButtonAction.touch && buttonWaitA)){
			//animBool = true;
			StartCoroutine("buttonwaita");
			Shoot();
			
		}



	
	}

	IEnumerator buttonwaita(){
		buttonWaitA = false;
		yield return new WaitForSeconds(AttackSpeed);
		buttonWaitA = true;
	}

	void Shoot(){

	
	Rigidbody clone;
	
	
	//Instantiate((GameObject)Resources.Load("Sphere"), transform.position + transform.forward*3.5f, transform.rotation);
	clone = Instantiate(projectile, new Vector3(CameraPos.transform.position.x, CameraPos.transform.position.y, CameraPos.transform.position.z) , CameraPos.transform.rotation) as Rigidbody;
	clone.velocity = CameraPos.transform.TransformDirection(Vector3.forward * 400);


}

}
