using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFormationShooters : MonoBehaviour {

	public GameObject baseEnemyShooter;
	List<GameObject> lShooters = new List<GameObject>();
	public int shooterAmount = 5;
	public float speed = 0;
	private Vector3 movementSpeed = Vector3.zero;
	private Vector3 startingPoint = Vector3.zero;

	void removeSelf() {
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {

		Invoke ("removeSelf", 3f);

		StartCoroutine (spawnShooters());

	}

	IEnumerator spawnShooters() {
		if (speed == 0) {
			speed = .1f;
		}

		if (Random.Range (0, 10) > 5) {
			movementSpeed.x = speed;
		} else {
			movementSpeed.x = -speed;			
		}
		if (Random.Range (0, 10) > 5) {
			//movementSpeed.y = speed/2;
			startingPoint.y = 2.5f;
		} else {
			//movementSpeed.y = -speed/2;
			startingPoint.y = 1.5f;			
		}

		startingPoint.x = 128 * -movementSpeed.x;
		//startingPoint.y = 128 * -movementSpeed.y;

		//float tempFrequency = Random.Range (.05f, .09f);
		float amplitude = Random.Range (1f, 1.8f);
		float tempFrequency = Random.Range (.05f, .09f);
		float startingAngle = 0;
		for (int i=0; i<shooterAmount;i++) {
			lShooters.Add((GameObject) Instantiate(baseEnemyShooter,baseEnemyShooter.transform.position,baseEnemyShooter.transform.rotation));
			lShooters[i].GetComponent<EnemyShooter>().speed = movementSpeed;
			lShooters[i].GetComponent<EnemyShooter>().frequency = tempFrequency;
			lShooters[i].GetComponent<EnemyShooter>().amplitude = amplitude;
			int whatFormation = 1;//Mathf.RoundToInt (Random.Range (0, 2.98f) - .49f);
			switch (whatFormation) {
			case 0:
				lShooters[i].GetComponent<EnemyShooter> ().speed = new Vector3 (0, -speed, 0);
				lShooters[i].transform.position = new Vector3(0,8,0);
				break;
			case 1:
				lShooters[i].transform.position = startingPoint - new Vector3(0,0,0);
				break;
			}
			lShooters[i].GetComponent<EnemyShooter>().formationType = whatFormation;
			startingAngle = 3.14f/2f * ((float)i/(float)shooterAmount);
			//lShooters[i].GetComponent<EnemyShooter>().startingAngle = startingAngle;
			//yield return new WaitForSeconds(.1f);
			yield return new WaitForSeconds(.3f);

		}

	}



	// Update is called once per frame
	void Update () {
	
	}
}
