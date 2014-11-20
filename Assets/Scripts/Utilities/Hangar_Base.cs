using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hangar_Base : MonoBehaviour {
	
	public List<GameObject> hangarslots = new List<GameObject>();
	public List<string> shipTypes = new List<string>();
	public List<int> shipUpgrade1 = new List<int>();
	public List<int> shipUpgrade2 = new List<int>();
	public List<int> shipUpgrade3 = new List<int>();

	public List<string> canonTypes = new List<string>();
	public List<int> canonUpgrade1 = new List<int>();
	public List<int> canonUpgrade2 = new List<int>();
	public List<int> canonUpgrade3 = new List<int>();

	public int mount1Set = 0;
	public int mount2Set = 0;

	public void run(){
		Debug.Log(mount1Set + " " + mount2Set);
	}
	public void setHangar(){
		Debug.Log(mount1Set + " " + mount2Set);
		int hangarCapacity = shipTypes.Count;
		if(hangarslots.Count != hangarCapacity){
			for (int i = hangarslots.Count; i < hangarCapacity; i++){
				GameObject newObj = (GameObject) Object.Instantiate(Resources.Load(shipTypes[i]));
				newObj.SetActive(false);
				Spaceship_Player script = newObj.GetComponent<Spaceship_Player>();
				script.IsActive = false;
				if(shipUpgrade1.Count != shipTypes.Count){
					addToShipUpgrades();
				}
				script.setUpStates(shipUpgrade1[i] , shipUpgrade2[i] , shipUpgrade3[i]);
				script.shipInitialization();
				hangarslots.Add(newObj);

				gunMountManagement(canonTypes[mount1Set], i, 0);
				if (script.CanonMountCapacity > 2){
					gunMountManagement(canonTypes[mount2Set], i, 1);
				}
			}
		}
	}

	public void setNewUpgradesHangar(int slotPos)
	{
		Spaceship_Player script = hangarslots[slotPos].GetComponent<Spaceship_Player>();
		script.setUpStates(shipUpgrade1[slotPos] , shipUpgrade2[slotPos] , shipUpgrade3[slotPos]);
		gunMountManagement(script.canonTypes[mount1Set], slotPos,0);
		if(script.CanonMountCapacity > 2){
			gunMountManagement(script.canonTypes[mount2Set], slotPos,1);
		}
	}

	public void setSpaceshipGuns(int ps){
		int shipC = GameObject.Find("ARCamera").GetComponent<Player_Charactor>().shipChoise;
		Spaceship_Player script = hangarslots[shipC].GetComponent<Spaceship_Player>();
		script.removeCanon(0);
		string _cannon = canonTypes[ps];
		script.gunSetting(_cannon,0);
		script.mountCanon(0);
		mount1Set = ps;
		if(script.CanonMountCapacity > 2){
			script.removeCanon(1);
			script.gunSetting(_cannon,1);
			script.mountCanon(1);
			mount2Set = ps;
		}
	}


	public void addSpaceshipToHangar(string shipType){
		shipTypes.Add(shipType);
	}
	public void addGunToHangar(string gunType){
		canonTypes.Add(gunType);
	}
	public void gunMountManagement(string gunType, int ship, int m){
		Spaceship_Player script = hangarslots[ship].GetComponent<Spaceship_Player>();
		script.removeCanon(m);
		script.gunSetting(gunType,m);
		script.mountCanon(m);
	}
	public void addToCanonUpgrades(){
		canonUpgrade1.Add(0);
		canonUpgrade2.Add(0);
		canonUpgrade3.Add(0);
	}
	public void addToShipUpgrades(){
		shipUpgrade1.Add(0);
		shipUpgrade2.Add(0);
		shipUpgrade3.Add(0);
	}

	public string returnContentString(){
		string reportString = "";

		for(int i = 0; i < canonTypes.Count ; i++){
			reportString +=  "CanonType=" + canonTypes[i] + "\n";
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

		for(int i = 0; i < shipTypes.Count ; i++){
			reportString +=  "ShipType=" + shipTypes[i] + "\n";
		}
		for(int i = 0; i < shipUpgrade1.Count ; i++){
			reportString +=  "ShipUpgrade1=" + shipUpgrade1[i] + "\n";
		}
		for(int i = 0; i < shipUpgrade2.Count ; i++){
			reportString +=  "ShipUpgrade2=" + shipUpgrade2[i] + "\n";
		}
		for(int i = 0; i < shipUpgrade3.Count ; i++){
			reportString +=  "ShipUpgrade3=" + shipUpgrade3[i] + "\n";
		}
		reportString +=  "CannonChoise1=" + mount1Set + "\n";
		reportString +=  "CannonChoise2=" + mount2Set + "\n";

		return reportString;
	}

}
