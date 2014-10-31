using UnityEngine;
using System.Collections;

public class LevelScript_Level : LevelScript_Base {

	public GameObject[] button ;
	public AButton buttonScript;


	protected Spaceship_Player shipScr ;
	protected SpawnControl_Enemy spwnScr;
	
	protected string cameraName = "ARCamera";
	protected GameObject image;

	public Joystick joystick;
	public bool useAxisInput = true;
	public float joystickInput = 0f;
	
	public int levelNumber;
	protected int enemiesDestroyed = 0;

	protected int howManyEnemies;

	protected Vector3 newScale;
	protected Vector3 newPosition;
	protected Vector3 newRotation;
	protected string newProp;


	//Make these nice
	
	protected float backGroundWidthLife;
	protected float lifePercent;
	protected float lifeWidth;
	protected float left;
	protected float top;
	protected float height;

	protected Texture lifeRemainingTexture;
	protected Texture lifeRemainingBehindTexture;

	protected int shipHealth;
	protected int shipShield;

	
	protected string endGame;
	protected float _unLoadTimer = 5f;
	protected int gain;

	
	public virtual void loadLevel(){}
	public virtual void updateLevel(){}




	protected void setClassTargets(){
		player = GameObject.Find(cameraName);
		script = player.GetComponent<Player_Charactor>();
		shipScr = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();



		lifeRemainingTexture = Resources.Load("io") as Texture;
		lifeRemainingBehindTexture = Resources.Load("io") as Texture;


		completed = false;
		
		image = GameObject.Find("ImageTarget");
	}

	protected override void loadButtons(){
		button  = new GameObject[2];
		button[0] = (GameObject)Object.Instantiate(Resources.Load ("AButton"));
		button[0].SetActive(true);
		button[0].guiTexture.pixelInset = new Rect(182,-175,100,100);
		buttonScript = button[0].GetComponent<AButton>();


		button[1] = (GameObject)Object.Instantiate(Resources.Load ("joystick"));
		button[1].SetActive(true);
		joystick = button[1].GetComponent<Joystick>();
		
	}
	protected override void unloadButtons(){
		for(int i = 0; i < 2 ; i++){
			Destroy(button[i]);
		}
	}

	protected void sentButtonInput(){
		shipScr.getButtonInput(buttonScript.touch, joystickInput);
	}

	public int priceCreditsValue(){
		int priceValue = (int) (75 * Mathf.Pow(levelNumber, 1.06f));
		return priceValue;
	}


	public int priceCreditsTotal(){
		int priceValue = priceCreditsValue();
		priceValue += (int) ((priceValue * 0.1f) * ( (float)enemiesDestroyed / howManyEnemies));
		return priceValue;
	}

	public override void levelGUI(){
		if(spwnScr.spawnEmpty)
		{
			GUI.TextField(new Rect(Screen.width/2 -Screen.width/8,Screen.height - Screen.height/4,Screen.width/4,Screen.height/4),endGame +  "\nEnemy Kills: " + enemiesDestroyed.ToString() + " / " + howManyEnemies.ToString() + "\nCredits: " + gain);
		}

		left = Screen.width / 2;
		top = 8F;
		backGroundWidthLife = Screen.width / 4;
		lifeWidth = lifePercent * backGroundWidthLife;
		height = 12F;
		
		GUI.Box (new Rect ((Screen.width / 2) + (Screen.width / 4), top, Screen.width / 10, Screen.height / 40), "Health: " + shipHealth.ToString() + "/" + shipShield.ToString());
		GUI.DrawTexture (new Rect (left, top, backGroundWidthLife, height), lifeRemainingBehindTexture, ScaleMode.StretchToFill, true, 1.0F);
		GUI.DrawTexture (new Rect (left, top, lifeWidth, height), lifeRemainingTexture, ScaleMode.StretchToFill, true, 1.0F);

	}
}
