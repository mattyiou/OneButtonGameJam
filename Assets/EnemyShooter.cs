using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	public GameObject bullet;
	public float timerPerShoot = 1f;
	public Vector3 speed = Vector3.zero;
	private float currentTime=0f;
	private float angle = 0;
	public float frequency = .07f;
	private float middleYPosition = 0;
	// Use this for initialization

	void removeSelf() {
		Destroy (this.gameObject);
	}

	void Start () {
		middleYPosition = transform.position.y;
		Invoke ("removeSelf", 9f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		angle+=frequency;
		transform.position = new Vector3 (transform.position.x,middleYPosition+Mathf.Sin (angle),transform.position.z);
		currentTime += Time.fixedDeltaTime;
		transform.position += speed;
		if (currentTime>timerPerShoot) {
			currentTime = 0;
			Instantiate(bullet,transform.position,bullet.transform.rotation);
		}

	}
}
