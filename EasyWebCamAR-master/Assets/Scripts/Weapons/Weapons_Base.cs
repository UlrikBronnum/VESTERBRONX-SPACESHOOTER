using UnityEngine;
using System.Collections;

public class Weapons_Base : MonoBehaviour {

	public Transform barrelEnd;
	public GameObject ammunition;
	public AudioClip fireExplosion;

	protected float rateOfFire;
	protected EventTimer_Base fireTimer;



	// Use this for initialization
	public virtual void Start () {}
	

	public void fireWeapon(){
		if(fireTimer.timerTick()){
			audio.PlayOneShot(fireExplosion);
			Instantiate(ammunition,barrelEnd.position,barrelEnd.rotation);
		}
	}
}
