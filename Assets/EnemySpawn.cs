using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject laserShootingShip;
	public GameObject suiciderShip;
	public GameObject formationShooterShip;
	public float currentTime;
	public float timePerEnemy = 5f;
	// Use this for initialization
	void Start () {
		spawnLaserEnemies ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime = currentTime + Time.fixedDeltaTime;
		if (timePerEnemy < currentTime) {
			currentTime = 0;
			switch (Mathf.RoundToInt(Random.Range(0,2.98f)-.49f)) {
			case 0: spawnShooters();
				break;
			case 1: spawnLaserEnemies();
				break;
			case 2: spawnSuiciders();
				break;
			}
			//spawnLaserEnemies();
		}

	}

	void spawnSuiciders() {
		for (int i=0; i<4; i++) { 
			Instantiate (suiciderShip, suiciderShip.transform.position, suiciderShip.transform.rotation);
		}
	}

	void spawnShooters() {
		Instantiate (formationShooterShip, formationShooterShip.transform.position, formationShooterShip.transform.rotation);
	}

	void spawnLaserEnemies() {

		int freeSpace = Random.Range (1, 5);
		for (int i=0; i<6; i++) {
			if (i!=freeSpace) {
				GameObject goTemporalLaserShip = (GameObject) Instantiate(laserShootingShip,laserShootingShip.transform.position,laserShootingShip.transform.rotation);
				goTemporalLaserShip.GetComponent<ApperShootAndLeave>().place = i+1;
			}
		}


	}
}
