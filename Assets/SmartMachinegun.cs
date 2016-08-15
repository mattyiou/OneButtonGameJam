using UnityEngine;
using System.Collections;

public class SmartMachinegun : MonoBehaviour {

	public GameObject bullet;
	public float rechargeTime=3f;
	public float firingTime=5f;
	public float timeBetweenShoots = .3f;
	private float currentTime = 0f;
	private float shotTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime += Time.fixedDeltaTime;
		shotTime += Time.fixedDeltaTime;
		if (currentTime < firingTime && shotTime > timeBetweenShoots) {
			shotTime = 0f;
			Vector3 target = GameObject.FindGameObjectWithTag ("Player").transform.position;
			Vector3 speed = new Vector3 ((target.x - transform.position.x) / 100 * .5f, (target.y - transform.position.y) / 100 * .5f, 0);
			Vector3 vectorToTarget = target - transform.position;
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			GameObject goBullet = (GameObject)Instantiate (bullet, this.transform.position, q);
			goBullet.GetComponent<Bullet> ().speed = speed;
		} else {
			if (currentTime > firingTime + rechargeTime) {
				currentTime = 0f;
			}
		}
	}
}
