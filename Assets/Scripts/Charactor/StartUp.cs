/*

	Sådan virker det:
	1. For at registrer brugere skal vi tjekke efter om de er registrerede allerede (kig Start() )
	Herefter kan funktionen ShowCreateUser() bruges som vil vise gui'en til at oprette en bruger.

	2. For at registrer en mail bruges funktionen ShowRegisterEmail() som viser GUI'en

	3. For at adde en score bruges funktionen AddScore(id2, score2, timeStarted)
	Her fyldes de 3 strings ud som de er angivet den første er til brugerens id, den næste
	er til scoren og den sidste er hvonår spillet startede.

*/
using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {
	public int newID;
	public bool isUser;
	public string id2 = ""; // 
	string registerText = "Write your info";
	string defaultUserName = ">Username", userName = ""; 
	string defaultEmail = ">Email", userEmail = "";  
	string defaultBopael = ">City", userBopael = "";
	string defaultAlder = ">Age", userAlder = "";
	string score2 = ""; //timeStarted = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
	string timeEnded = "";
	string message = "";
	public bool userCreatedBool = false;
	bool addEmailBool = false, addScoreBool = false;
	GUIStyle myStyle = new GUIStyle();
	GUIStyle myHeadStyle = new GUIStyle();
	GUIStyle mySecondStyle = new GUIStyle();
	public Font myFont;
	public int tal;
	



	// Use this for initialization
	void Start () {




//		Tjek om brugeren eksisterer i databasen

//		if (idIsExisting){ // Det her skal bygges da jeg ikke ved noget om den lokale gemte data
//			id2 = *gemt data id*
//		} else {
//			ShowCreateUser();
//		}
	
	
	}
	public void ShowCreateUser(){
		userCreatedBool = false;
	}
	public void ShowRegisterEmail(){
		addEmailBool = true;
	}
	


	void CreateUser(string user, string bopael, string alder, string email){

		message = "";
		
		if (user != "")
		{
			WWWForm form = new WWWForm();
			form.AddField("user", user);
			form.AddField("bopael", bopael);
			form.AddField("alder", alder);
			form.AddField("email", email);
			print(email);
			WWW w = new WWW("http://www.carmoe.dk/AAU/RegisterUser.php", form);
			StartCoroutine(registerUserFunc(w));
		}
		else {
			message += "Please enter a Username \n";
		}

	}
	void RegisterEmail(string id, string email){

		message = "";

		WWWForm form = new WWWForm();
		form.AddField("email", email);
		form.AddField("id", id);
		WWW w = new WWW("http://www.carmoe.dk/AAU/RegisterEmail.php", form);
		StartCoroutine(registerEmailFunc(w));
	}

	public void AddScore(string id, string score, string timeStarted, string credits) {

		timeEnded = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		message = "";
		WWWForm form = new WWWForm();
		form.AddField("user_id", id);
		form.AddField("score", score);
		form.AddField ("credits", credits);
		form.AddField("time_started", timeStarted);
		form.AddField("time_ended", timeEnded);
		WWW w = new WWW("http://www.carmoe.dk/AAU/AddScore.php", form);
		StartCoroutine(addScoreFunc(w));

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	public void levelGUI()
	{

//		myHeadStyle.fontSize = 95;
		myHeadStyle.fontSize = Screen.width/13;
		myHeadStyle.normal.textColor = Color.white;
//		myStyle.fontSize = 95;
		myStyle.fontSize = Screen.width/13;
		myStyle.normal.textColor = Color.white;
//		mySecondStyle.fontSize = 80;
		mySecondStyle.fontSize = Screen.width/23;
		mySecondStyle.normal.textColor = Color.grey;

		//KUN TIL TEST *** SKAL SLÆTTES EFTER !!! BRUGES KUN TIL AT FREMTVINGE MENUERNE NÅR DE SKAL TESTES 
		if (GUI.Button(new Rect(0,0,70,20) ,"User"))
		{
			ShowCreateUser();
			addEmailBool = false;
			addScoreBool = false;
			Handheld.Vibrate();
		}
		if (GUI.Button(new Rect(75,0,70,20) ,"Email"))
		{
			ShowRegisterEmail();
			userCreatedBool = true;
			addScoreBool = false;
		}
		if (GUI.Button(new Rect(150,0,70,20) ,"Score"))
		{
			//ShowAddScore();
			userCreatedBool = true;
			addEmailBool = false;
		}
		//KUN TIL TEST *** SKAL SLÆTTES EFTER !!! BRUGES KUN TIL AT FREMTVINGE MENUERNE NÅR DE SKAL TESTES

		GUI.BeginGroup(new Rect(Screen.width/2-(Screen.width/4)-(Screen.width/30),Screen.height/2-(Screen.height/3)-(Screen.height/8),Screen.width,Screen.height));
		if (message != "")
			GUILayout.Box(message);
		if (!userCreatedBool){
			GUI.DrawTexture(new Rect(new Rect(0,0,Screen.width/2+Screen.width/11,Screen.width/2+Screen.width/170)),Resources.Load("Interface/texture14") as Texture);
			GUI.Label(new Rect(Screen.width/24+Screen.width/600,Screen.width/150,500,500) , registerText, myHeadStyle);
			GUI.SetNextControlName ("player_name");
			userName = GUI.TextField(new Rect(Screen.width/24+Screen.width/200,Screen.width/11+ Screen.width/150,Screen.width/2, Screen.height/10),userName, mySecondStyle);
			if (UnityEngine.Event.current.type == EventType.Repaint)
			{
				if (GUI.GetNameOfFocusedControl () == "player_name")
				{
					if (userName == defaultUserName) userName = "";
				}
				else
				{
					if (userName == "") userName = defaultUserName;
				}
			}
			GUI.SetNextControlName ("player_bopael");
			userBopael = GUI.TextField(new Rect(Screen.width/24+Screen.width/200,Screen.width/7+Screen.width/60,Screen.width/2, Screen.height/10),userBopael, mySecondStyle);
			if (UnityEngine.Event.current.type == EventType.Repaint)
			{
				if (GUI.GetNameOfFocusedControl () == "player_bopael")
				{
					if (userBopael == defaultBopael) userBopael = "";
				}
				else
				{
					if (userBopael == "") userBopael = defaultBopael;
				}
			}
			GUI.SetNextControlName ("player_alder");
			userAlder = GUI.TextField(new Rect(Screen.width/24+Screen.width/200,Screen.width/5+Screen.width/40,Screen.width/2, Screen.height/10),userAlder, mySecondStyle);
			if (UnityEngine.Event.current.type == EventType.Repaint)
			{
				if (GUI.GetNameOfFocusedControl () == "player_alder")
				{
					if (userAlder == defaultAlder) userAlder = "";
				}
				else
				{
					if (userAlder == "") userAlder = defaultAlder;
				}
			}

			GUI.SetNextControlName ("pEmail");
			userEmail = GUI.TextField(new Rect(Screen.width/23,Screen.width/4+Screen.width/25,Screen.width/2, Screen.height/10) ,userEmail, mySecondStyle);
			if (UnityEngine.Event.current.type == EventType.Repaint)
			{
				if (GUI.GetNameOfFocusedControl () == "pEmail")
				{
					if (userEmail == defaultEmail) userEmail = "";
				}
				else
				{
					if (userEmail == "") userEmail = defaultEmail;
				}
			}

			if (GUI.Button(new Rect(Screen.width/7+Screen.width/600,Screen.width/3+Screen.width/60,400,100),"Register", myStyle))
			{
				CreateUser(userName, userBopael, userAlder, userEmail);
				userCreatedBool = true;
				message = "Creating User";
			}


		}



		GUI.EndGroup();



	}

	IEnumerator registerUserFunc(WWW w)
	{

		yield return w;
		if (w.error == null && w.text != "User allready exists!")
		{
			message = w.text;
			id2 = w.text;


			newID = int.Parse(w.text);
			Debug.Log(id2 + " + " + newID);
			tal = int.Parse(w.text);
			tal = tal % 2;
			print (tal % 2);
			userCreatedBool = true;
		}
		else
		{
			message += "ERROR: " + w.error+ "\n";
			if (w.text == "User allready exists!"){
				message += w.text;
				registerText = "Name not valid";

			}
			userCreatedBool = false;
		}
	}
	IEnumerator registerEmailFunc(WWW w)
	{
		
		yield return w;
		if (w.error == null)
		{
			message += w.text;
			print("Email setup");
			addEmailBool = false;
		}
		else
		{
			message += "ERROR: " + w.error + "\n";
		}
	}
	IEnumerator addScoreFunc(WWW w)
	{
		
		yield return w;
		if (w.error == null)
		{
			message += w.text;
			print("Score Added");
			addScoreBool = false;
		}
		else
		{
			message += "ERROR: " + w.error + "\n";
		}
	}
}
