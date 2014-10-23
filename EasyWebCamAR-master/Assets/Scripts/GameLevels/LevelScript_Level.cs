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
	
	public virtual void loadLevel(){}
	public virtual void updateLevel(){}
	public virtual void levelGUI(){	}

	protected void setClassTargets(){
		player = GameObject.Find(cameraName);
		script = player.GetComponent<Player_Charactor>();
		shipScr = script.hangar.hangarslots[script.shipChoise].GetComponent<Spaceship_Player>();
		
		completed = false;
		
		image = GameObject.Find("ImageTarget");
	}

	protected override void loadButtons(){
		button  = new GameObject[2];
		button[0] = (GameObject)Object.Instantiate(Resources.Load ("AButton"));
		button[0].SetActive(true);
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
}
