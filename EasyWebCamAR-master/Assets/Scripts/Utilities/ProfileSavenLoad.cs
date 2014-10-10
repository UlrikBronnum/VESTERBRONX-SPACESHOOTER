using UnityEngine;
using System.Collections;
using System.IO;
using System.Globalization;

public class ProfileSavenLoad : MonoBehaviour {

	public GameObject profile;
	private Player_Charactor profileScript;

	// Use this for initialization
	void Start () {
		profileScript = profile.GetComponent<Player_Charactor>();
		gameLoad();
	}
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			gameSave();
		}
	}

	public void gameSave(){
		// Find path of game folder and save it
		string path = Application.dataPath + "/SaveGame";
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
		// Find path of folder 
		string path = Application.dataPath + "/SaveGame/";
		// If the file exists 
		if(File.Exists(path + "/profileSave.fzf")){
			// Load file
			StreamReader fileLoaded = File.OpenText(path + "/profileSave.fzf");
			string s = "";
			// while there is a new line read it
			while((s = fileLoaded.ReadLine()) != null){
				// Split the line at the '='
				string[] getLine = s.ToString().Split('=');
				// Match title to variable and update game
				if(getLine[0] == "Player Position X"){ 
					Vector3 temp = profile.transform.position;
					temp.x = float.Parse(getLine[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
					profile.transform.position = temp;
				}
				if(getLine[0] == "Player Position Y"){
					Vector3 temp = profile.transform.position;
					temp.y = float.Parse(getLine[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
					profile.transform.position = temp;
				}
				if(getLine[0] == "Player Position Z"){
					Vector3 temp = profile.transform.position;
					temp.z = float.Parse(getLine[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
					profile.transform.position = temp;
				}
				if(getLine[0] == "Player Lives"){

				}
				if(getLine[0] == "Player Defence"){

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
		data += "Player Position X=" + profile.transform.position.x + "\n";
		data += "Player Position Y=" + profile.transform.position.y + "\n";
		data += "Player Position Z=" + profile.transform.position.z + "\n";

		// returns the string when done
		return data;
	}
}
