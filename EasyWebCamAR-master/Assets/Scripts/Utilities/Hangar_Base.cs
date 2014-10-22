using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hangar_Base : MonoBehaviour {
	
	public List<GameObject> hangarslots = new List<GameObject>();
	public List<string> shipTypes = new List<string>();

	public List<string> canonTypes = new List<string>();
	public List<int> canonUpgrade1 = new List<int>();
	public List<int> canonUpgrade2 = new List<int>();
	public List<int> canonUpgrade3 = new List<int>();


	public void setHangar(){
		int hangarCapacity = shipTypes.Count;
		if(hangarslots.Count != hangarCapacity){
			for (int i = hangarslots.Count; i < hangarCapacity; i++){
				GameObject newObj = (GameObject) Object.Instantiate(Resources.Load(shipTypes[i]));
				newObj.SetActive(false);
				Spaceship_Player script = newObj.GetComponent<Spaceship_Player>();
				script.IsActive = false;
				script.shipInitialization();
				hangarslots.Add(newObj);
				gunMountManagement(script.canonTypes[0], i);
			}
		}
	}


	public void addSpaceshipToHangar(string shipType){
		shipTypes.Add(shipType);
		setHangar();
	}
	public void addGunToHangar(string gunType){
		canonTypes.Add(gunType);
	}
	public void gunMountManagement(string gunType, int ship){
		Spaceship_Player script = hangarslots[ship].GetComponent<Spaceship_Player>();
		script.gunSetting(gunType);
	}
	public void addToCanonUpgrades(){
		canonUpgrade1.Add(0);
		canonUpgrade2.Add(0);
		canonUpgrade3.Add(0);
	}


	public string returnContentString(){
		string reportString = "";

		for(int i = 0; i < canonTypes.Count ; i++){
			reportString +=  "CanonType=" + canonTypes[i] + "\n";
		}
		for(int i = 0; i < shipTypes.Count ; i++){
			reportString +=  "ShipType=" + shipTypes[i] + "\n";
		}
		for(int i = 0; i < canonUpgrade1.Count ; i++){
			reportString +=  "CanonUpgrade1=" + canonUpgrade1[i] + "\n";
		}
		for(int i = 0; i < canonUpgrade2.Count ; i++){
			reportString +=  "CanonUpgrade2=" + canonUpgrade2[i] + "\n";
		}
		for(int i = 0; i < canonUpgrade3.Count ; i++){
			reportString +=  "CanonUpgrade3=" + canonUpgrade3[i] + "\n";
		}

		return reportString;
	}

}
