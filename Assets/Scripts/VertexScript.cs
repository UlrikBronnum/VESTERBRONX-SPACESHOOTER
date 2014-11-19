using UnityEngine;
using System.Collections;

public class VertexScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(1F, 5f, 1f);
		grow ();
		StartCoroutine("grow");

	}
	
	// Update is called once per frame
	void Update () {

	
	}

	

	IEnumerator grow(){
		while (transform.localScale.x<66601){
			this.transform.localScale += new Vector3 (666.01f, 594.25f, 972.0006f);
			yield return new WaitForSeconds(0.00000001f);		
		}


	}
}
