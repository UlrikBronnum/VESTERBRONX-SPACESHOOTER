using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	bool movingForward = true;
	bool movingBackward = false;
	bool movingLeft = false;
	bool movingRight = false;

	//public GameObject child;


	// Use this for initialization
	void Start () {
		StartCoroutine(DiagonalSquare());
	}
	
	// Update is called once per frame
	void Update () {
		if (movingForward){
			rigidbody.velocity = Vector3.forward;//new Vector3(0,0,1);
		}
		if (movingBackward){
			rigidbody.velocity = Vector3.back;//new Vector3(0,0,-1);
		}
		if (movingLeft){
			rigidbody.velocity = Vector3.left;//new Vector3(-1,0,0);
		}
		if (movingRight){
			rigidbody.velocity = Vector3.right;//new Vector3(1,0,0);
		}
		if (Input.GetButtonDown("Fire1")) Death();
	}

	void Death () {
		//Rigidbody clone1 = Instantiate(child, transform.position + new Vector3(1,0,0), transform.rotation) as Rigidbody;
		//Rigidbody clone2 = Instantiate(child, transform.position + new Vector3(-1,0,0), transform.rotation) as Rigidbody;
		Destroy(this.gameObject);

	}

	IEnumerator Square(){
		yield return new WaitForSeconds(5);
		movingForward = false;
		movingRight = true;
		yield return new WaitForSeconds(5);
		movingRight = false;
		movingBackward = true;
		yield return new WaitForSeconds(5);
		movingBackward = false;
		movingLeft = true;
		yield return new WaitForSeconds(5);
		movingLeft = false;
		movingForward = true;

	}
	IEnumerator DiagonalSquare(){
		yield return new WaitForSeconds(5);
		movingForward = false;
		rigidbody.velocity = new Vector3(1,0,1);
		yield return new WaitForSeconds(5);
		rigidbody.velocity = new Vector3(1,0,-1);
		yield return new WaitForSeconds(5);
		rigidbody.velocity = new Vector3(-1,0,-1);
		yield return new WaitForSeconds(5);
		rigidbody.velocity = new Vector3(-1,0,1);
		yield return new WaitForSeconds(5);
		movingForward = true;
	}
}
