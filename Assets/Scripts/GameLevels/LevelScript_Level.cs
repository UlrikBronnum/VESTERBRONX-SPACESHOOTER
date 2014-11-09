using UnityEngine;
using System.Collections;

public class LevelScript_Level : LevelScript_Base {

	public GameObject[] button ;
	public AButton[] buttonScript;


	protected Spaceship_Player shipScr ;
	protected SpawnControl_Enemy spwnScr;
	
	protected string cameraName = "ARCamera";

	public Joystick joystick;
	public bool useAxisInput = true;
	public float joystickInput = 0f;
	
	public int levelNumber;
	protected int enemiesDestroyed = 0;
	protected bool levelCompleted = false;

	protected int howManyEnemies;

	protected Vector3 newScale;
	protected Vector3 newPosition;
	protected Vector3 newRotation;
	protected string newProp;


	//Make these nice
	


	protected Texture lifeRemainingTexture;
	protected Texture lifeRemainingBehindTexture;


	protected int shipHealth;
	protected int shipShield;

	protected int numberOfFireButtons = 1;
	
	protected string endGame;
	protected float _unLoadTimer = 5f;
	protected int gain;

	protected int shipDamageHealth;
	protected int shipDamageShield;

	protected int shipAmmunition;
	protected int shipAmmunitionLoss;

	protected Texture[] fireButton = new Texture[2];
	protected Texture[] fireButtonDown = new Texture[2];

	protected string[] enemyTypes;

	public virtual int getLevelNumber(){
		return 0;
	}

	public virtual void loadLevel(){}

	public bool canLoad(int playerProgress)
	{
		gain = -1;
		levelCompleted = false;
		levelNumber = getLevelNumber();
		if(playerProgress >= levelNumber - 1){
			return true;
		}else{
			return false;
		}
	}

	public void resetLevel(){
		setClassTargets();
	}


	protected void setClassTargets()
	{
		fireButton[0] = Resources.Load("Interface/fireButtom_vesterbro") as Texture;
		fireButton[1] = Resources.Load("Interface/Button_Space_Fire") as Texture;

		fireButtonDown[0] = Resources.Load("Interface/Button_Vesterbro_Fire") as Texture;
		fireButtonDown[1] = Resources.Load("Interface/Button_Space_Fire") as Texture;

		setMainVars();

		player = GameObject.Find(cameraName);
		script = player.GetComponent<Player_Charactor>();
		shipScr = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
		shipScr.copyInitialization();
		shipHealth = shipScr.shipHealth();
		shipShield = shipScr.shipShield();
		shipAmmunition = shipScr.mountMagasinCapacity;
		shipAmmunitionLoss = shipScr.mountMagasinCapacity;

		lifeRemainingTexture = Resources.Load("Interface/ammobar") as Texture;
		lifeRemainingBehindTexture = Resources.Load("healthb") as Texture;
		_unLoadTimer = 5f;

		completed = false;

		background = GameObject.Find("ImageTarget");
	}

	public override void updateLevel(){
		
		shipDamageHealth = shipScr.shipInGameHealth;
		shipDamageShield = shipScr.shipInGameShield;
		shipAmmunitionLoss = shipScr.mountMagasinCapacity;

		Debug.Log(levelCompleted);
		
		if(useAxisInput) {
			// assigns the position of the joystick to h and v
			joystickInput = joystick.position.x;
		}
		else {
			joystickInput = Input.GetAxis("Horizontal");
		}
		

		
		
		if (spwnScr.spawnEmpty){
			SpawnControl_Enemy tmpscr =  props[0].GetComponent<SpawnControl_Enemy>();
			enemiesDestroyed = tmpscr.EnemyDead;

			if( (float)enemiesDestroyed/howManyEnemies > 0.6f){
				endGame = "Complete";
				gain = priceCreditsTotal();
			}else {
				endGame = "Fail";
				gain = 0;
			}

			levelCompleted = true;
			_unLoadTimer -= Time.deltaTime * 1f;
			
			if(_unLoadTimer < 0){
				if( (float)enemiesDestroyed/howManyEnemies > 0.6f){
					script.credits += priceCreditsTotal();
					if(script.levelsCompleted < levelNumber){
						script.levelsCompleted = levelNumber;
					}
				}

				unloadButtons();
				closeLevel();
				Spaceship_Player shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
				shipScript.gameObject.SetActive(false);
				shipScript.IsActive = false;
				completed = true;

			}
			
		}else if (shipDamageHealth < 1){
			levelCompleted = true;
			_unLoadTimer -= Time.deltaTime * 1f;
			Spaceship_Player shipScript = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
			shipScript.gameObject.SetActive(false);
			SpawnControl_Enemy tmpscr =  props[0].GetComponent<SpawnControl_Enemy>();
			enemiesDestroyed = tmpscr.EnemyDead;

			endGame = "Fail";
			gain = 0;

			if(_unLoadTimer < 0){
				unloadButtons();
				closeLevel();
				resetLevel();
				shipScript.IsActive = false;
				completed = true;	
			}
		}else {
			sentButtonInput();
		}
	}

	protected override void loadButtons()
	{

		int buttonSize = Screen.height/5, placementX = Screen.width - buttonSize*2, placementY = Screen.height/20 , scaleFont = buttonSize/4;

		if(numberOfFireButtons == 2){
			buttonScript = new AButton[1];
			button  = new GameObject[2];
		}else if(numberOfFireButtons == 4){
			buttonScript = new AButton[2];
			button  = new GameObject[3];
		}



		placementX = Screen.width / 12;
		placementY =  Screen.height / 9;
		
		button[0] = (GameObject)Object.Instantiate(Resources.Load ("joystick"));
		button[0].SetActive(true);
		button[0].guiTexture.pixelInset = new Rect (placementX , placementY, buttonSize,buttonSize);
		joystick = button[0].GetComponent<Joystick>();

		placementX = Screen.width - buttonSize*2;
		placementY = placementY = Screen.height/20;

		button[1] = (GameObject)Object.Instantiate(Resources.Load ("Interface/FireButton"));
		button[1].SetActive(true);
		button[1].guiTexture.texture = fireButton[script.gameSetting];
		button[1].guiTexture.pixelInset = new Rect(placementX,placementY,buttonSize,buttonSize);
		buttonScript[0] = button[1].GetComponent<AButton>();

		button[1].guiText.pixelOffset = new Vector2(placementX + buttonSize/2,placementY + buttonSize/2) ;
		button[1].guiText.text = "Fire1";
		button[1].guiText.font = newFont;
		button[1].guiText.fontSize = scaleFont;
		button[1].guiText.color = script.textColor;

		if(numberOfFireButtons == 4)
		{
			placementX = Screen.width - buttonSize - Screen.height/20;
			placementY = buttonSize + Screen.height/20;

			button[2] = (GameObject)Object.Instantiate(Resources.Load ("Interface/FireButton"));
			button[2].SetActive(true);
			button[2].guiTexture.texture = fireButton[script.gameSetting];
			button[2].guiTexture.pixelInset = new Rect(placementX,placementY,buttonSize,buttonSize);
			buttonScript[1] = button[2].GetComponent<AButton>();
			
			button[2].guiText.pixelOffset = new Vector2(placementX + buttonSize/2,placementY + buttonSize/2) ;
			button[2].guiText.text = "Fire2";
			button[2].guiText.font = newFont;
			button[2].guiText.fontSize = scaleFont;
			button[2].guiText.color = script.textColor;

		}


		
	}
	protected override void unloadButtons(){
		int limit = 0;
		if(numberOfFireButtons == 4){
			limit = 3;
		}
		else
		{
			limit = 2;
		}

		for(int i = 0; i < limit ; i++){
			Destroy(button[i]);
		}
	}

	protected void sentButtonInput(){
		if(numberOfFireButtons == 2)
		{
			if(buttonScript[0].touch)
			{
				button[1].guiTexture.texture = fireButtonDown[script.gameSetting];
			}
			else
			{
				button[1].guiTexture.texture = fireButton[script.gameSetting];
			}
			shipScr.getButtonInput(buttonScript[0].touch,false, joystickInput);
		}
		else if(numberOfFireButtons == 4)
		{
			if(buttonScript[1].touch)
			{
				button[2].guiTexture.texture = fireButtonDown[script.gameSetting];
			}
			else
			{
				button[2].guiTexture.texture = fireButton[script.gameSetting];
			}
			shipScr.getButtonInput(buttonScript[0].touch, buttonScript[1].touch,joystickInput);
		}

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
	public int fullPriceCreditsTotal(){
		int priceValue = priceCreditsValue();
		priceValue += (int) ((priceValue * 0.1f));
		return priceValue;
	}
	public override void levelGUI(){
		int buttonHeight = Screen.height/10 * 9 , buttonWidth = Screen.width/10 * 9, placementX = 0, placementY = 0, scaleFont = buttonHeight/3;



		placementX = (int)(Screen.width/10 * 0.5f); 
		placementY = (int)(Screen.height/10 * 0.5f);
		buttonHeight = (int)Screen.height/10 * 9; 
		buttonWidth = (int)Screen.width/10 * 9;
		

		if(levelCompleted){
			if(gain == 0)
			{
				myGUIStyle.alignment = TextAnchor.MiddleCenter;
				placementX = (int)(Screen.width/10 * 0.5f); 
				placementY = (int)(Screen.height/10 * 0.5f);
				buttonHeight = (int)Screen.height/10 * 9; 
				buttonWidth = (int)Screen.width/10 * 9;
				
				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				
				GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),script.buttonTexture);
				scaleFont = 35;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect (0, 0, buttonWidth, buttonHeight), endGame +  "\nEnemy Kills: " + enemiesDestroyed.ToString() + " / " + howManyEnemies.ToString() + "\nCredits Won: " + gain + " out of " + fullPriceCreditsTotal(),myGUIStyle);
				GUI.EndGroup();
			}else{
				myGUIStyle.alignment = TextAnchor.MiddleCenter;
				placementX = (int)(Screen.width/10 * 0.5f); 
				placementY = (int)(Screen.height/10 * 0.5f);
				buttonHeight = (int)Screen.height/10 * 9; 
				buttonWidth = (int)Screen.width/10 * 9;

				GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
				
				float backGroundWidthCredits;
				float creditWidth;
				float creditPercent;
				creditPercent = priceCreditsTotal() / fullPriceCreditsTotal();
				backGroundWidthCredits = buttonWidth;
				creditWidth = creditPercent * backGroundWidthCredits;		

				GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),script.buttonTexture);
				scaleFont = 35;
				myGUIStyle.fontSize = scaleFont;
				GUI.Box (new Rect (0, 0, buttonWidth, buttonHeight), endGame +  "\nEnemy Kills: " + enemiesDestroyed.ToString() + " / " + howManyEnemies.ToString() + "\nCredits Won: " + gain + " out of " + fullPriceCreditsTotal(),myGUIStyle);
				GUI.EndGroup();
			}
		}


		buttonHeight = Screen.height/16;
		buttonWidth = Screen.width/10;
		placementX = 0;
		placementY = 0;
		scaleFont = buttonHeight/3 * 2;

		myGUIStyle.alignment = TextAnchor.MiddleLeft;

		float life_Remain = (float)shipDamageHealth / shipHealth ;
		float life_Width = life_Remain * buttonWidth;
		if(life_Width < 0){
			life_Width = 0;
		}
		int hN = (shipDamageHealth > 0)? shipDamageHealth: 0;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		myGUIStyle.fontSize = scaleFont;
		GUI.DrawTexture(new Rect(0, 0,buttonWidth ,buttonHeight),script.buttonTexture);
		GUI.Box (new Rect (scaleFont/2,0, buttonWidth/3,buttonHeight), "Health",myGUIStyle);
		GUI.EndGroup();

		placementX = buttonWidth;
		placementY = 0;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth * 2,buttonHeight - 4));
		Texture2D healthBar = new Texture2D(1,1);
		Color healthBarColor;
		if(life_Remain > 0.5f){
			healthBarColor = Color.Lerp(new Color(0.0f,0.6f,0.0f,1.0f),new Color(0.6f,0.6f,0.0f,1.0f), (1 - life_Remain)*2 );
		}else if(life_Remain > 0) {
			healthBarColor = Color.Lerp(new Color(0.6f,0.6f,0.0f,1.0f),new Color(0.6f,0.0f,0.0f,1.0f),(0.5f -life_Remain) * 2);
		}else {
			healthBarColor = Color.black;
		}
		healthBar.SetPixel(0,0, healthBarColor);
		healthBar.Apply();
		GUI.skin.box.normal.background = healthBar;
		GUI.Box(new Rect(0,2,(int)(buttonWidth * 2 * life_Remain) ,buttonHeight),GUIContent.none);
		GUI.EndGroup();


		myGUIStyle.alignment = TextAnchor.MiddleLeft;
		placementX = 0;
		placementY = buttonHeight;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));

		float shield_Remain = (float)shipDamageShield / shipShield ;
		float shield_Width = shield_Remain * buttonWidth;
		if(shield_Width < 0){
			shield_Width = 0;
		}
		int sN = (shipDamageShield > 0)? shipDamageShield: 0;
		myGUIStyle.fontSize = scaleFont;
		GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),script.buttonTexture);
		GUI.Box (new Rect (scaleFont/2,0, buttonWidth,buttonHeight ), "Shield" ,myGUIStyle);
		GUI.EndGroup();


		myGUIStyle.alignment = TextAnchor.MiddleRight;
		placementX = buttonWidth;
		placementY = buttonHeight;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth * 2,buttonHeight - 4));
		Texture2D shieldBar = new Texture2D(1,1);
		Color shieldBarColor;
		if(shield_Remain > 0.5f){
			shieldBarColor = Color.Lerp(new Color(0.0f,0.0f,0.6f),new Color(0.6f,0.0f,0.6f), (1 - shield_Remain)*2 );
		}else if(shield_Remain > 0) {
			shieldBarColor = Color.Lerp(new Color(0.6f,0.0f,0.6f),new Color(0.6f,0.0f,0.0f),(0.5f -shield_Remain) * 2);
		}else {
			shieldBarColor = Color.black;
		}
		shieldBar.SetPixel(0,2, shieldBarColor);
		shieldBar.Apply();
		GUI.skin.box.normal.background = shieldBar;
		GUI.Box(new Rect(0,2,(int)(buttonWidth * 2 * shield_Remain) ,buttonHeight),GUIContent.none);
		GUI.EndGroup();




		buttonHeight = Screen.height/16;
		buttonWidth = Screen.width/10;
		placementX = Screen.width - buttonWidth;
		placementY = 0;
		scaleFont = buttonHeight/3 * 2;
		
		myGUIStyle.alignment = TextAnchor.MiddleCenter;

		float ammunition_Remain = (float) shipAmmunitionLoss / shipAmmunition;
		float ammunition_Width = ammunition_Remain * buttonWidth * 2;
		
		if(ammunition_Width < 0){
			ammunition_Width = 0;
		}
		
		int a1N = (shipAmmunitionLoss > 0)? shipAmmunitionLoss: 0;
		
		myGUIStyle.fontSize = scaleFont;
		
		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth,buttonHeight));
		GUI.DrawTexture(new Rect(0,0,buttonWidth ,buttonHeight),script.buttonTexture);
		GUI.Box (new Rect (scaleFont/2,- buttonHeight/4, buttonWidth,buttonHeight),  "Ammo",myGUIStyle);
		GUI.EndGroup();

		placementX = Screen.width - buttonWidth * 3;
		placementY = 0;

		GUI.BeginGroup(new Rect(placementX,placementY,buttonWidth * 2,buttonHeight - 4));
		GUI.DrawTexture (new Rect (buttonWidth * 2 - ammunition_Width, 2 ,buttonWidth * 2,buttonHeight - 2) , lifeRemainingTexture, ScaleMode.StretchToFill, true, 1.0F);
		GUI.EndGroup();

	}
}
