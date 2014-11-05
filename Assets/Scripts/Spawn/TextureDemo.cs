using UnityEngine;
using System.Collections;

public class TextureDemo : MonoBehaviour {
	
		void Start() {
		//renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		//renderer.material.SetColor("_RedColor", new Color(Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f));


		//1st!
		//renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		//renderer.material.SetColor("_RedColor",new Color(0.525f, 0.027f, 0.788f, 1.0f) );

		//renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		//renderer.material.SetColor("_GreenColor",new Color(0.17f, 0.188f, 0.494f, 1.0f) );

		//renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		//renderer.material.SetColor ("_BlueColor", new Color(0.153f, 0.278f, 0.102f, 1.0f));

		//2nd
		renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		renderer.material.SetColor("_RedColor",new Color(0.117f, 0.533f, 0.552f, 1.0f) );
		
		renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		renderer.material.SetColor("_GreenColor",new Color(1f, 1f, 1f, 1.0f) );
		
		renderer.material.shader = Shader.Find("My Shaders/CHANGE DA SHIZZLE");
		renderer.material.SetColor ("_BlueColor",new Color(0f, 0f, 0.529f, 1.0f) );


		//3rd




	}
		void Update() {

		}
	}
