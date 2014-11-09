using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {
	public bool isUser;
	string id2 = "";
	string user2 = "", email2 = "", score2 = "", bopael2 = "", alder2 = "", timeStarted = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), timePlayed2;
	string message = "";
	public int gameVersion;
	// Use this for initialization
	void Start () {
	
	

	}
	void CreateUser(string user, string bopael, string alder){

		message = "";
		
		if (user != "")
		{
			WWWForm form = new WWWForm();
			form.AddField("user", user);
			form.AddField("bopael", bopael);
			form.AddField("alder", alder);
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

	void AddScore(string id, string score, string timeStarted) {

		message = "";
		WWWForm form = new WWWForm();
		form.AddField("user_id", id);
		form.AddField("score", score);
		form.AddField("time_started", timeStarted);
		WWW w = new WWW("http://www.carmoe.dk/AAU/AddScore.php", form);
		StartCoroutine(addScoreFunc(w));

	}
	
	// Update is called once per frame
	void Update () {
	


	}

	private void OnGUI()
	{
		if (!isUser){

			if (message != "")
				GUILayout.Box(message);

			GUILayout.Label("Username");
			user2 = GUILayout.TextField(user2);
			GUILayout.Label("Bopæl");
			bopael2 = GUILayout.TextField(bopael2);
			GUILayout.Label("Alder");
			alder2 = GUILayout.TextField(alder2);
			GUILayout.Label("Email");
			email2 = GUILayout.TextField(email2);
			GUILayout.Label("ID");
			id2 = GUILayout.TextField(id2);
			GUILayout.Label("Score");
			score2 = GUILayout.TextField(score2);
			//GUILayout.Label("TimePlayed");
			//timePlayed2 = GUILayout.TextField(timePlayed2);

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Register"))
			{
				CreateUser(user2, bopael2, alder2);
			}
			if (GUILayout.Button("Email"))
			{
				RegisterEmail(id2, email2);
			}
			if (GUILayout.Button("Score"))
			{
				AddScore(id2, score2, timeStarted);
			}

			GUILayout.EndHorizontal();
		}

	}

	IEnumerator registerUserFunc(WWW w)
	{

		yield return w;
		if (w.error == null && w.text != "User allready exists!")
		{
			message += w.text;
			//int tal = int.Parse(w.text);
			print("User Created with ID : " + w.text);
		}
		else
		{
			message += "ERROR: " + w.error+ "\n";
			if (w.text == "User allready exists!")
				message += w.text;
		}
	}
	IEnumerator registerEmailFunc(WWW w)
	{
		
		yield return w;
		if (w.error == null)
		{
			message += w.text;
			print("Email setup");
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
		}
		else
		{
			message += "ERROR: " + w.error + "\n";
		}
	}
}
