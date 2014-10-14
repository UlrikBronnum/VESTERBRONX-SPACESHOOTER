using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hangar_Base : MonoBehaviour {
	
	public List<GameObject> hangarslots = new List<GameObject>();
	public List<string> shipTypes = new List<string>();

	public List<string> canonTypes = new List<string>();
	public List<int> canonAmmunitionStorage = new List<int>();


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
		if(canonAmmunitionStorage.Count != canonTypes.Count){
			for (int i = canonAmmunitionStorage.Count; i < canonTypes.Count; i++){
				canonAmmunitionStorage.Add(1000);
			}
		}
	}
	public void addAmmunitionToHangar(List<string> playerGuns ,string ammunitionType, int sumToAdd){
		foreach (int element in canonAmmunitionStorage){
			if(ammunitionType == playerGuns[element]){
				canonAmmunitionStorage[element] += sumToAdd;
			}
		}
	}
	public void addSpaceshipToHangar(string shipType){
		shipTypes.Add(shipType);
	}
	public void addGunToHangar(string gunType){
		canonTypes.Add(gunType);
	}
	public void gunMountManagement(string gunType, int ship){
		Spaceship_Player script = hangarslots[ship].GetComponent<Spaceship_Player>();
		script.gunSetting(gunType,ship);
	}


}
