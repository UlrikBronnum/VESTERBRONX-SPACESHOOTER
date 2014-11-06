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
		for (float i =0; i<100; i++) {
			this.transform.localScale += new Vector3 (350F-2*i, 230f-2*i, 540f-2*i);
			yield return new WaitForSeconds(0.014f);		
		}
		for (float j =0; j<40; j++) {
			this.transform.localScale += new Vector3 (-140, -92, -216);
			yield return new WaitForSeconds(0.00000001f);		
		}
		for (float i =0; i<100; i++) {
			this.transform.localScale += new Vector3 (471f, 500f, 617.39f);
			yield return new WaitForSeconds(0.014f);		
		}
		/*for (float i =0; i<100; i++) {
			this.transform.localScale += new Vector3 (320f,1000f, 519f);
			yield return new WaitForSeconds(0.014f);		
		}*/


	}
}
