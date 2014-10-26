using UnityEngine;
using System.Collections;

public class SpawnControl_Enemy : SpawnControl_Base {

	public int numberOfEnemies;
	public Enemy_Spawn spawnBase;

	public bool spawnEmpty;

	private float spwnTim = 0.5f;
	private int spawnCount = 0;
	private bool spwnWing = false;
	private Vector3 tmpPos;

	private string[] shaderNames;

	public int EnemyDead = 0;

	public void setSpawnBase () {
		spawnBase.enemiesToSpawn = numberOfEnemies;
		shaderNames = new string[2];
		shaderNames[0] = "Mars";
		shaderNames[1] = "Fart";
	}

	protected override void Start () {
		spawnEmpty = false;
		timer = new EventTimer_Base(spawnRate);
	}
	
	// Update is called once per frame
	protected override void Update () {

		if(spawnBase.enemiesToSpawn > 0){
			if(timer.timerTick()){
				spwnWing = true;
				tmpPos = new Vector3 (transform.position.x + Random.Range(-50f,50f),transform.position.y,transform.position.z);
			}
		}else if (transform.childCount == 0){
			EnemyDead = spawnBase.deadEnemy;
			spawnEmpty = true;
		}



		if (spwnWing){
			spwnTim -= Time.deltaTime * 2f;
			if (spwnTim < 0){
				spawnBase.SpawnWing(tmpPos , spawnCount);
				spawnCount++;
				spwnTim = 1.5f;
			}
			if (spawnCount > 2){
				timer.TimerValue = spawnRate;
				spwnWing = false;
				spawnCount = 0;
				spwnTim = 1.5f;
			}
		}
	}
}
