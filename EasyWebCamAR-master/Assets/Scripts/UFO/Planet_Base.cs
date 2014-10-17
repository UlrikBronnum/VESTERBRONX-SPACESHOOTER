using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet_Base : MonoBehaviour {

	protected float axisRotationSpeed;
	protected float	lightOrbitSpeed;
	protected GameObject planetLight;

	public virtual void Start(){}
	public virtual void Update(){}

	public void createLight(Vector3 pos)
	{
		planetLight = (GameObject) Object.Instantiate(new GameObject());
		planetLight.AddComponent<Light>();
		planetLight.light.type = LightType.Directional;
		planetLight.light.shadows = LightShadows.Soft;
		planetLight.light.range = 50f;
		planetLight.transform.parent = this.transform;
		planetLight.transform.position = new Vector3 (transform.position.x + pos.x , transform.position.y + pos.y , transform.position.z + pos.z) ;
		planetLight.transform.Rotate(new Vector3(0,1,0) * 90);


	}
}
