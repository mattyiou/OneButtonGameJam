using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject laserShootingShip;
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
			spawnLaserEnemies();
		}

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
