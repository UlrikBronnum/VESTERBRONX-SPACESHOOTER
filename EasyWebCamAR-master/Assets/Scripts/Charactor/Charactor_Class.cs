using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charactor_Class : MonoBehaviour 
{
	// timer to control internal behavior
	public char gameSetting;
	
	public Hangar_Base hangar;
	protected int hangarCapacity;

	protected List<string> canonTypes = new List<string>();



	// expecting child class to have a override function start()
	public virtual void Start () 
	{
		
	}
	// expecting child class to have a override function Update()
	public virtual void Update () 
	{


	}

}
