using UnityEngine;
using System.Collections;
using System.IO;
using System.Globalization;

public class ProfileSavenLoad : MonoBehaviour {

	private GameObject profile;
	private Player_Charactor profileScript;

	// Use this for initialization
	public void Start() {
		profile = GameObject.Find("ARCamera");
		profileScript = profile.GetComponent<Player_Charactor>();
	}
	public void onClose () 
	{
		gameSave();
	}
	public bool filePresent(){
		profile = GameObject.Find("ARCamera");
		profileScript = profile.GetComponent<Player_Charactor>();
		//string path = Application.dataPath + "/SaveGame";
		string path = Application.persistentDataPath + "/SaveGame";
		if(Directory.Exists(path)){
			if(!File.Exists(path + "/profileSave.fzf")){
				return false;
			}else{
				return true;
			}
		}else{
			return false;
		}
	}

	public void gameSave(){
		// Find path of game folder and save it
		//string path = Application.dataPath + "/SaveGame";
		string path = Application.persistentDataPath + "/SaveGame";
		// Check if the folder does not exists, if not 
		// then create it at the path!
		if(!Directory.Exists(path)){
			DirectoryInfo dir = Directory.CreateDirectory(path);
		}
		// Check if the file does not exists, if not 
		// then create it!
		if(!File.Exists(path + "/profileSave.fzf")){
			StreamWriter file = File.CreateText(path + "/profileSave.fzf");
			file.Close();
		}
		// Load the savegame file and write the data to it
		StreamWriter loadFile = new StreamWriter(path + "/profileSave.fzf");
		// Data comes from the custom function saveFileFormat
		loadFile.WriteLine(saveFileFormat());
		// Close the file
		loadFile.Close();

	}
	public void gameLoad(){
		profile = GameObject.Find("ARCamera");
		profileScript = profile.GetComponent<Player_Charactor>();
		Debug.Log("x");
		// Find path of folder 
		//string path = Application.dataPath + "/SaveGame/";
		string path = Application.persistentDataPath + "/SaveGame";
		Debug.Log(path);
		// If the file exists 
		if(File.Exists(path + "/profileSave.fzf")){
			Debug.Log("xxx");
			// Load file
			StreamReader fileLoaded = File.OpenText(path + "/profileSave.fzf");
			string s = "";
			// while there is a new line read it
			while((s = fileLoaded.ReadLine()) != null){
				// Split the line at the '='
				string[] getLine = s.ToString().Split('=');
				// Match title to variable and update game

				if(getLine[0] == "CanonType"){
					profileScript.hangar.addGunToHangar(getLine[1]);
				}
				if(getLine[0] == "CanonUpgrade1"){
					profileScript.hangar.canonUpgrade1.Add(int.Parse(getLine[1]));
				}
				if(getLine[0] == "CanonUpgrade2"){
					profileScript.hangar.canonUpgrade2.Add(int.Parse(getLine[1]));
				}
				if(getLine[0] == "CanonUpgrade3"){
					profileScript.hangar.canonUpgrade3.Add(int.Parse(getLine[1]));
				}

				if(getLine[0] == "ShipType"){
					profileScript.hangar.addSpaceshipToHangar(getLine[1]);
				}
				if(getLine[0] == "ShipUpgrade1"){
					profileScript.hangar.shipUpgrade1.Add(int.Parse(getLine[1]));
				}
				if(getLine[0] == "ShipUpgrade2"){
					profileScript.hangar.shipUpgrade2.Add(int.Parse(getLine[1]));
				}
				if(getLine[0] == "ShipUpgrade3"){
					profileScript.hangar.shipUpgrade3.Add(int.Parse(getLine[1]));
				}
				if(getLine[0] == "GameVersion"){
					profileScript.gameSetting =  int.Parse(getLine[1]);
				}
				if(getLine[0] == "LevelCompleted"){
					profileScript.levelsCompleted =  int.Parse(getLine[1]);
				}
				if(getLine[0] == "Credit"){
					profileScript.credits =  int.Parse(getLine[1]);
				}
				if(getLine[0] == "DatabaseID"){
					profileScript.userDatabaseID =  int.Parse(getLine[1]);
				}
			}
			// Close the file
			fileLoaded.Close();
		}
	}
	public string saveFileFormat(){
		// The file is a long string 
		string data = "";
		// adds lines to it
		data += profileScript.hangar.returnContentString();
		data += profileScript.returnContentString();
		// returns the string when done
		return data;
	}
}
