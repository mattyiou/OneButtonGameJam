using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	public GameObject bullet;
	public float timerPerShoot = 1f;
	public Vector3 speed = Vector3.zero;
	private float currentTime=0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime += Time.fixedDeltaTime;
		transform.position += speed;
		if (currentTime>timerPerShoot) {
			currentTime = 0;
			Instantiate(bullet,transform.position,bullet.transform.rotation);
		}

	}
}
