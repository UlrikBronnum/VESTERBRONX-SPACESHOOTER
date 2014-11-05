using UnityEngine;
using System.Collections;

public class PortalFog : MonoBehaviour {

	private float dieTime = 7f;
	// Update is called once per frame
	void Update () {
		dieTime -= Time.deltaTime;
		if(dieTime < 0){
			Destroy(gameObject);
		}
	}
}
