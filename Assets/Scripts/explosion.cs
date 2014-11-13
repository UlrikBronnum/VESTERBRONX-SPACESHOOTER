using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	// audio for the explosion
	public AudioClip bang;

	// Use this for initialization
	void Start () {
		bang = Resources.Load("Audio/MissileS") as AudioClip;
		Destroy (this.gameObject, 2f);
		audio.PlayOneShot(bang);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
