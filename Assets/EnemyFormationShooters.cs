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
	// Use this for initialization
	void Start () {
		if (speed == 0) {
			speed = .1f;
		}

		if (Random.Range (0, 10) > 5) {
			movementSpeed.x = speed;
		} else {
			movementSpeed.x = -speed;			
		}
		if (Random.Range (0, 10) > 5) {
			movementSpeed.y = speed/2;
			startingPoint.y = 4.5f;
		} else {
			movementSpeed.y = -speed/2;
			startingPoint.y = 1.5f;			
		}

		startingPoint.x = 128 * -movementSpeed.x;
		//startingPoint.y = 128 * -movementSpeed.y;


		for (int i=0; i<shooterAmount;i++) {
			lShooters.Add((GameObject) Instantiate(baseEnemyShooter,baseEnemyShooter.transform.position,baseEnemyShooter.transform.rotation));
			lShooters[i].GetComponent<EnemyShooter>().speed = movementSpeed;
			lShooters[i].transform.position = startingPoint - new Vector3(i*1.7f,0,0);
			//yield return new WaitForSeconds(.1f);

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
