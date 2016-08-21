using UnityEngine;
using System.Collections;

public class EnemyShooter : Enemy {

	public GameObject bullet;
	public float timerPerShoot = 1f;
	public Vector3 speed = Vector3.zero;
	private float currentTime=0f;
	private float angle = 0;
	public float frequency = .07f;
	public float amplitude = 1f;
	public float startingAngle = 0f;
	private float middleYPosition = 0;
	private bool doingIntro = true;
	public int formationType = 0;

	public const int FORMATION_DIVE_DOWNWARDS = 0;
	public const int FORMATION_ZIGZAG_MIDDLE = 1;
	public const int FORMATION_SPIRAL= 2;

	//public GameObject explosionParticle;

	void removeSelf() {
		Destroy (this.gameObject);
	}

	/*void OnTriggerEnter2D(Collider2D col) {
		if (col.Equals (null)) {

		} else if (col.tag.Equals ("Player")) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<avatarManager> ().GetHit (5);
		}
		Instantiate (explosionParticle, transform.position, explosionParticle.transform.rotation);
		Destroy (this.gameObject);

	}*/

	// Use this for initialization
	void Start () {
		middleYPosition = transform.position.y;
		Invoke ("removeSelf", 9f);
		angle = startingAngle;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		angle += frequency;

		Vector3 newPosition = Vector3.zero;
		switch (formationType) {
		case FORMATION_ZIGZAG_MIDDLE: 
			newPosition = new Vector3 (transform.position.x, middleYPosition + amplitude * Mathf.Sin (angle), transform.position.z);
			if (doingIntro) {
				if ((transform.position.x < -7 && speed.x <0) || (transform.position.x > 7 && speed.x >0)) {
					doingIntro = false;
					speed = -speed;
				}
			} else {
				if (currentTime > timerPerShoot+Random.Range(0,.6f)) {
					currentTime = 0;
					Instantiate (bullet, transform.position, bullet.transform.rotation);
				}
			}
			break;
		case FORMATION_DIVE_DOWNWARDS:
			newPosition = new Vector3 (amplitude * Mathf.Sin (angle), transform.position.y , transform.position.z);
			if (currentTime > timerPerShoot+Random.Range(0,.6f)) {
				currentTime = 0;
				Instantiate (bullet, transform.position, bullet.transform.rotation);
			}
			break;

		}

		transform.position = newPosition;
		transform.position += speed;

		currentTime += Time.fixedDeltaTime;


	}
}
