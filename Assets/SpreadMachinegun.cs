using UnityEngine;
using System.Collections;

public class SpreadMachinegun : MonoBehaviour {

	public GameObject bullet;

	public float fireDelay = 2f;
	private float currentTime = 0f;
	public float shotsAmount = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime += Time.fixedDeltaTime;
		if (currentTime > fireDelay) {
			currentTime = 0f;
			float frequency = -Mathf.PI/shotsAmount;
			for (int i=0; i<=shotsAmount; i++) {
				GameObject goBullet = (GameObject)Instantiate(bullet,transform.position,transform.rotation);
				goBullet.GetComponent<Bullet>().speed = new Vector3(.05f * Mathf.Cos(frequency*i),.05f * Mathf.Sin(frequency*i),0);
			}
		}
	}
}
