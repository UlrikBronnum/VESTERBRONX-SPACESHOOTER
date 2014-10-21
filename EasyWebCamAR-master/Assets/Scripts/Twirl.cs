using UnityEngine;
using System.Collections;

public class Twirl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime*1000);
		transform.localScale -= new Vector3(0.20F, 0.00F, 0.20F);
		transform.position += new Vector3 (0, 0.02F, 0);

		if (transform.localScale.x < 0.05) {
			Destroy (gameObject);
		}
	}
}
